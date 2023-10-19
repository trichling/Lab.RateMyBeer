resource "azurerm_route_table" "hub_routes" {
  name                = "rt-hub"
  resource_group_name = azurerm_resource_group.hub.name
  location            = var.location
}

resource "azurerm_route" "vpn_to_firewall" {
  name                   = "vpn-to-firewall"
  resource_group_name    = azurerm_resource_group.hub.name
  route_table_name       = azurerm_route_table.hub_routes.name
  address_prefix         = azurerm_subnet.gateway_subnet.address_prefixes[0]
  next_hop_type          = "VirtualAppliance"
  next_hop_in_ip_address = azurerm_firewall.hub_firewall.ip_configuration[0].private_ip_address
}
