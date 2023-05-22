data "azurerm_client_config" "current" {}
data "azuread_client_config" "current" {}

data "azurerm_resource_group" "infrastructure" {
  name = "infrastructure"
}

data "azurerm_container_registry" "container_registry" {
  name = "thinkexception"
  resource_group_name = "Infrastructure"
}

data "azurerm_storage_account" "thinkexception" {
  name                = "thinkexception"
  resource_group_name = data.azurerm_resource_group.infrastructure.name
}