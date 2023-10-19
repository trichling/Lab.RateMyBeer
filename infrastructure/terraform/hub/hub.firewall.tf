resource "azurerm_public_ip" "hub_firewall_pip" {
    name                = "pip-firewall"
    location            = var.location
    resource_group_name = azurerm_resource_group.hub.name
    allocation_method   = "Static"
    sku                 = "Standard"
}

resource "azurerm_firewall_policy" "hub_fw_policy" {
    name                = "fw-policy-hub"
    location            = var.location
    resource_group_name = azurerm_resource_group.hub.name
    sku = "Basic"
}

resource "azurerm_firewall" "hub_firewall" {
    name                = "afw-hub"
    location            = var.location
    resource_group_name = azurerm_resource_group.hub.name
    sku_name            = "AZFW_VNet"
    sku_tier            = "Standard"

    firewall_policy_id = azurerm_firewall_policy.hub_fw_policy.id

    ip_configuration {
        name                 = "fw-pip"
        subnet_id            = azurerm_subnet.firewall_subnet.id
        public_ip_address_id = azurerm_public_ip.hub_firewall_pip.id
    }
}



resource "azurerm_firewall_network_rule_collection" "hub_basic_firewall_rules" {
  name = "basic-rules"
  azure_firewall_name = azurerm_firewall.hub_firewall.name
  resource_group_name = azurerm_resource_group.hub.name
  priority            = 100
  action              = "Allow"

  rule {
    name = "DNS"
    description = "Allow DNS"
    destination_addresses = [ "*" ]
    source_addresses = [ "*" ]
    destination_ports = [ "53" ]
    protocols = [ "UDP" ]
  }
}