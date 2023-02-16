resource "azurerm_servicebus_namespace" "ratemybeer" {
  location            = azurerm_resource_group.ratemybeer.location
  name                = "${var.environment}-${var.application_name}"
  resource_group_name = azurerm_resource_group.ratemybeer.name
  sku                 = "Standard"
}