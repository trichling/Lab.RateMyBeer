resource "azurerm_virtual_network" "hub_network" {
  name                = "vnet-hub"
  resource_group_name = azurerm_resource_group.hub.name
  location            = var.location
  address_space       = [var.hub_network_address_space]
}

resource "azurerm_subnet" "gateway_subnet" {
  name                 = "GatewaySubnet"
  resource_group_name  = azurerm_resource_group.hub.name
  virtual_network_name = azurerm_virtual_network.hub_network.name
  address_prefixes     = [var.vpn_gateway_subnet_address_space]
}

resource "azurerm_subnet" "firewall_subnet" {
  name                 = "AzureFirewallSubnet"
  resource_group_name  = azurerm_resource_group.hub.name
  virtual_network_name = azurerm_virtual_network.hub_network.name
  address_prefixes     = [var.firewall_subnet_address_space]
}

