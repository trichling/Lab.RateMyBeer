## Key vault
resource "azurerm_key_vault" "ratemybeer" {
  name                      = "${var.environment}-${var.application}"
  resource_group_name       = azurerm_resource_group.RateMyBeerRessourceGroup.name
  location                  = azurerm_resource_group.RateMyBeerRessourceGroup.location
  tenant_id                 = data.azurerm_client_config.current.tenant_id
  sku_name                  = "standard"
  enable_rbac_authorization = true
}

resource "azurerm_role_assignment" "KeyVaultSecretsOfficeToCurrentUser" {
  principal_id = data.azurerm_client_config.current.object_id
  scope        = azurerm_key_vault.ratemybeer.id  
  role_definition_name = "Key Vault Secrets Officer"
}



