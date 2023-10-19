resource "azurerm_private_dns_zone" "dev_thinkexception" {
  name = var.private_dns_zone_name
  resource_group_name = azurerm_resource_group.hub.name
}

resource "azurerm_private_dns_zone_virtual_network_link" "hub_vnet_dns" {
  name = "dns-link-hub"
  private_dns_zone_name = var.private_dns_zone_name
  resource_group_name = azurerm_resource_group.hub.name
  virtual_network_id = azurerm_virtual_network.hub_network.id
}