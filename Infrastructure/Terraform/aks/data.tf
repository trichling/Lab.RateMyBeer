data "azurerm_container_registry" "container_registry" {
  name = "thinkexception"
  resource_group_name = "Infrastructure"
}