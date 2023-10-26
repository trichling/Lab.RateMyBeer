resource "azurerm_container_registry" "thinkexception" {
  resource_group_name       = azurerm_resource_group.infrastructure.name
  location                  = azurerm_resource_group.infrastructure.location
  name                = "thinkexception"
  sku                 = "Basic"
}