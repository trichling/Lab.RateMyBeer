resource "azurerm_storage_account" "thinkexception" {
  account_replication_type = "LRS"
  account_tier             = "Standard"
  resource_group_name       = azurerm_resource_group.infrastructure.name
  location                  = azurerm_resource_group.infrastructure.location
  name                     = "thinkexception"
}