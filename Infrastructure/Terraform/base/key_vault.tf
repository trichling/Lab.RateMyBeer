## Key vault
resource "azurerm_key_vault" "ratemybeer" {
  tenant_id                 = data.azurerm_client_config.current.tenant_id
  resource_group_name       = azurerm_resource_group.RateMyBeerRessourceGroup.name
  location                  = azurerm_resource_group.RateMyBeerRessourceGroup.location
  name                      = "${var.environment}-${var.application}"
  sku_name                  = "standard"
  enable_rbac_authorization = true
}

resource "azurerm_role_assignment" "KeyVaultSecretsOfficeToCurrentUser" {
  principal_id = data.azurerm_client_config.current.object_id
  scope        = azurerm_key_vault.ratemybeer.id  
  role_definition_name = "Key Vault Secrets Officer"
}



