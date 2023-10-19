locals {
  frontend_port_name                     = "${data.azurerm_virtual_network.meinapetito_vnet.name}-feport"
  frontend_port_name_https               = "${data.azurerm_virtual_network.meinapetito_vnet.name}-feport-https"
  frontend_public_ip_configuration_name  = "${data.azurerm_virtual_network.meinapetito_vnet.name}-feip"
  frontend_private_ip_configuration_name = "${data.azurerm_virtual_network.meinapetito_vnet.name}-feprivateip"

  private_ip_address = cidrhost(azurerm_subnet.applicationgatewaysubnet.address_prefixes[0], 5)
}

resource "azurerm_application_gateway" "spoke" {
  name                = "${var.environment}_${var.application}"
  resource_group_name = data.azurerm_resource_group.meinapetito_resourcegroup.name
  location            = data.azurerm_resource_group.meinapetito_resourcegroup.location

  sku {
    name     = "WAF_v2"
    tier     = "WAF_v2"
    capacity = 2
  }

  identity {
    type = "UserAssigned"
    identity_ids = [
      data.azurerm_user_assigned_identity.application_gateway_key_vault_reader.id
    ]
  }

    waf_configuration {
    enabled                  = true
    file_upload_limit_mb     = 100
    firewall_mode            = "Detection"
    max_request_body_size_kb = 128
    request_body_check       = true
    rule_set_type            = "OWASP"
    rule_set_version         = "3.0"
  }

  gateway_ip_configuration {
    name      = "frontend_ipconfiguration"
    subnet_id = azurerm_subnet.applicationgateway_subnet.id
  }

  frontend_port {
    name = local.frontend_port_name
    port = 80
  }

  frontend_port {
    name = local.frontend_port_name_https
    port = 443
  }

  frontend_ip_configuration {
    name                          = local.frontend_private_ip_configuration_name
    private_ip_address_allocation = "Static"
    private_ip_address            = local.private_ip_address
    subnet_id                     = azurerm_subnet.applicationgateway_subnet.id
  }

  http_listener {
    name                           = "http-listener"
    frontend_ip_configuration_name = local.frontend_private_ip_configuration_name
    frontend_port_name             = local.frontend_port_name
    protocol                       = "Http"
  }

  backend_address_pool {
    name = "backend-pool"
  }

  backend_http_settings {
    name                  = "backend-http-settings"
    cookie_based_affinity = "Disabled"
    port                  = 80
    protocol              = "Http"
    request_timeout       = 20
  }

  request_routing_rule {
    name                       = "http-routing-rule"
    rule_type                  = "Basic"
    http_listener_name         = "http-listener"
    backend_address_pool_name  = "backend-pool"
    backend_http_settings_name = "backend-http-settings"
  }
}