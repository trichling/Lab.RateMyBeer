resource "azurerm_subnet" "applicationgateway_subnet" {
  name                 = "subnet-applicationgateway"
  virtual_network_name = data.azurerm_virtual_network.spoke_vnet.name
  resource_group_name  = data.azurerm_resource_group.spoke.name
  address_prefixes     = [var.applicationgateway_subnet_address_space]
}

resource "azurerm_public_ip" "applicationGateway_public_ip" {
  name                = "pip-${var.environment}-${var.application}-applicationgateway"
  resource_group_name = data.azurerm_resource_group.spoke.name
  location            = data.azurerm_resource_group.spoke.location
  allocation_method   = "Static"
  domain_name_label   = "${var.environment}-${var.application}"
  sku                 = "Standard"
}

resource "azurerm_route_table" "routing_from_applicationgateway" {
  name                = "${var.environment}_${var.application}_routing_applicationgateway"
  location            = data.azurerm_resource_group.spoke.location
  resource_group_name = data.azurerm_resource_group.spoke.name
}

resource "azurerm_subnet_route_table_association" "subnetlink" {
  subnet_id      = azurerm_subnet.applicationgateway_subnet.id
  route_table_id = azurerm_route_table.routing_from_applicationgateway.id
}

resource "azurerm_route" "applicationgateway_To_hub" {
  name                   = "To_Hub"
  resource_group_name    = data.azurerm_resource_group.spoke.name
  route_table_name       = azurerm_route_table.routing_from_applicationgateway.name
  address_prefix         = var.hub_virtualnetwork_address_space
  next_hop_type          = "VirtualAppliance"
  next_hop_in_ip_address = data.azurerm_firewall.hub_firewall.ip_configuration.0.private_ip_address
}

resource "azurerm_private_dns_zone_virtual_network_link" "dns_zone_virtual_network_link" {
  name                  = "${var.environment}.thinkex.net_${var.application}"
  resource_group_name   = data.azurerm_private_dns_zone.dns_zone.resource_group_name
  private_dns_zone_name = data.azurerm_private_dns_zone.dns_zone.name
  virtual_network_id    = data.azurerm_virtual_network.spoke_vnet
  registration_enabled  = false
}