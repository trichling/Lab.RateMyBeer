resource "azurerm_subnet" "cluster_subnet" {
  name                 = "${var.environment}-${var.application}-${var.version_number}"
  resource_group_name  = data.azurerm_resource_group.ratemybeer.name
  virtual_network_name = data.azurerm_virtual_network.ratemybber.name
  address_prefixes     = [var.cluster_subnet_address_space]
}