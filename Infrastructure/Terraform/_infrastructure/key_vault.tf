## Key vault
resource "azurerm_key_vault" "thinkexception" {
  tenant_id                 = data.azurerm_client_config.current.tenant_id
  resource_group_name       = azurerm_resource_group.infrastructure.name
  location                  = azurerm_resource_group.infrastructure.location
  name                      = "thinkexception"
  sku_name                  = "standard"
  enable_rbac_authorization = true
}

resource "azurerm_role_assignment" "KeyVaultSecretsOfficeToCurrentUser" {
  principal_id = data.azurerm_client_config.current.object_id
  scope        = azurerm_key_vault.thinkexception.id
  role_definition_name = "Key Vault Secrets Officer"
}

resource "azurerm_key_vault_secret" "tenant_id" {
  key_vault_id = azurerm_key_vault.thinkexception.id
  name         = "tenant-id"
  value        = data.azurerm_client_config.current.tenant_id
}

resource "azurerm_key_vault_secret" "subscription_id" {
  key_vault_id = azurerm_key_vault.thinkexception.id
  name         = "subscription-id"
  value        = data.azurerm_client_config.current.subscription_id
}

resource "azurerm_key_vault_secret" "infrastructure_storage_account_primary_access_key" {
  key_vault_id = azurerm_key_vault.thinkexception.id
  name         = "infrastructure-storageaccount-thinkexception-AccessKey1"
  value        = azurerm_storage_account.thinkexception.primary_access_key
}