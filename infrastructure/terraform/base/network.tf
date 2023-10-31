## network
resource "azurerm_virtual_network" "spoke" {
  name                = "vnet-${var.application}-${var.environment}"
  resource_group_name = azurerm_resource_group.RateMyBeerRessourceGroup.name
  location            = azurerm_resource_group.RateMyBeerRessourceGroup.location
  address_space       = var.spoke_vnet_address_spaces
}

resource "azurerm_virtual_network_peering" "peer_spoke_to_hub" {
  name                      = "peer_spoke_to_hub"
  resource_group_name       = azurerm_resource_group.RateMyBeerRessourceGroup.name
  virtual_network_name      = azurerm_virtual_network.spoke.name
  remote_virtual_network_id = data.azurerm_virtual_network.hub.id
  allow_forwarded_traffic   = true
  allow_gateway_transit     = true
  use_remote_gateways       = true
}

resource "azurerm_virtual_network_peering" "peer_hub_to_spoke" {
  name                      = "peer_hub_to_spoke"
  resource_group_name       = data.azurerm_resource_group.hub.name
  virtual_network_name      = data.azurerm_virtual_network.hub.name
  remote_virtual_network_id = azurerm_virtual_network.spoke.id	
  allow_forwarded_traffic   = true
  allow_gateway_transit     = true
}

resource "azurerm_private_dns_zone_virtual_network_link" "thinkex" {
  name = "${var.environment}.thinkex.net"
  resource_group_name = data.azurerm_resource_group.hub.name
  virtual_network_id = azurerm_virtual_network.spoke.id
  private_dns_zone_name = "${var.environment}.thinkex.net"
}