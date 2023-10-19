data "azurerm_resource_group" "spoke" {
  name = "${var.environment}-${var.application}"
}

data "azurerm_virtual_network" "spoke_vnet" {
    name = "${var.environment}-${var.application}"
    resource_group_name = data.azurerm_resource_group.spoke.name
}

data "azurerm_resource_gropu" "hub" {
    name = "rg-hub"
}

data "azurerm_firewall" "hub-firewall" {
  name                = "afw-hub"
  resource_group_name = data.azurerm_resource_group.hub.name
}

data "azurerm_private_dns_zone" "dns_zone" {
  name                = format("%s.thinkex.net", var.environment)
  resource_group_name = data.azurerm_resource_group.hub.name
}

data "azurerm_user_assigned_identity" "application_gateway_key_vault_reader" {
  name                = format("%s_%s_%s", var.environment, var.application, "application_gateway")
  resource_group_name = data.azurerm_resource_group.spoke.name
}