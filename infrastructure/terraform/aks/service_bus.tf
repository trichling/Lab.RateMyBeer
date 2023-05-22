resource "azurerm_servicebus_namespace" "ratemybeer" {
  name                = "${var.environment}-${var.application}-${var.version_number}"
  resource_group_name = data.azurerm_resource_group.ratemybeer.name
  location            = data.azurerm_resource_group.ratemybeer.location
  sku                 = "Standard"
}

resource "azurerm_key_vault_secret" "service_bus_connection_string" {
  name         = "servicebusconnectionstring-${var.version_number}"
  value        = azurerm_servicebus_namespace.ratemybeer.default_primary_connection_string
  key_vault_id = data.azurerm_key_vault.ratemybeer.id
}