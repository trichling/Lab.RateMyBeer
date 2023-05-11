resource "azurerm_servicebus_namespace" "ratemybeer" {
  location            = data.azurerm_resource_group.ratemybeer.location
  resource_group_name = data.azurerm_resource_group.ratemybeer.name
  name                = "${var.environment}-${var.application}-${var.version_number}"
  sku                 = "Standard"
}

resource "azurerm_key_vault_secret" "service_bus_connection_string" {
  key_vault_id = data.azurerm_key_vault.ratemybeer.id
  name         = "servicebusconnectionstring-${var.version_number}"
  value        = azurerm_servicebus_namespace.ratemybeer.default_primary_connection_string
}