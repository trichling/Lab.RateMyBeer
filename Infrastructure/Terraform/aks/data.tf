data "azurerm_container_registry" "container_registry" {
  name = "thinkexception"
  resource_group_name = "Infrastructure"
}

data "azurerm_resource_group" "ratemybeer" {
  name     = "${var.environment}-${var.application}"
}

data "azurerm_virtual_network" "ratemybber" {
  name = "${var.environment}-${var.application}"
  resource_group_name = data.azurerm_resource_group.ratemybeer.name
}

data "azurerm_key_vault" "ratemybeer" {
  name = "${var.environment}-${var.application}"
  resource_group_name = data.azurerm_resource_group.ratemybeer.name
}

data "azurerm_key_vault_secret" "service_principal_objectId" {
  name = "service-principal-objectid"
  key_vault_id = data.azurerm_key_vault.ratemybeer.id
}
