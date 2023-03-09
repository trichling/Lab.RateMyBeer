resource "azurerm_log_analytics_workspace" "ratemybeer" {
  name                = "${var.environment}-${var.application_name}"
  location            = azurerm_resource_group.ratemybeer.location
  resource_group_name = azurerm_resource_group.ratemybeer.name
  sku                 = "PerGB2018"
  retention_in_days   = 30
}

resource "azurerm_container_app_environment" "ratemybeer" {
  name                       = "${var.environment}-${var.application_name}"
  location                   = azurerm_resource_group.ratemybeer.location
  resource_group_name        = azurerm_resource_group.ratemybeer.name
  log_analytics_workspace_id = azurerm_log_analytics_workspace.ratemybeer.id
  
  // infrastructure_subnet_id = ""
  // internal_load_balancer_enabled = true 
}

