resource "azurerm_container_app_environment" "ratemybeer" {
  name                       = "${var.environment}-${var.application_name}"
  location                   = azurerm_resource_group.ratemybeer.location
  resource_group_name        = azurerm_resource_group.ratemybeer.name
  log_analytics_workspace_id = azurerm_log_analytics_workspace.ratemybeer.id
}

