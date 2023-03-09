resource "azurerm_user_assigned_identity" "identity" {
  name                = "${var.environment}-${var.application_name}"
  resource_group_name = azurerm_resource_group.ratemybeer.name
  location            = azurerm_resource_group.ratemybeer.location
}

data "azurerm_container_registry" "thinkexception_container_registry" {
  name = "thinkexception"
  resource_group_name = "infrastructure"
}

resource "azurerm_role_assignment" "acrpull_role" {
  scope                            = data.azurerm_container_registry.thinkexception_container_registry.id
  role_definition_name             = "AcrPull"
  principal_id                     = azurerm_user_assigned_identity.identity.principal_id
  skip_service_principal_aad_check = true
}