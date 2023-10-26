# create a ratemybeer service principal and add clientid / clientsecret into keyvault
# use the same sp for azure devops pipeline


resource "azuread_application" "application" {
  display_name     = "azuredevops_serviceconnection_principal"
  owners           = [data.azuread_client_config.current.object_id]
  sign_in_audience = "AzureADMyOrg"
}

resource "azuread_service_principal" "service_principal" {
  application_id = azuread_application.application.application_id
  owners         = [data.azuread_client_config.current.object_id]
}

resource "time_rotating" "main" {
  rotation_days = 365
}

resource "azuread_service_principal_password" "service_principal_password" {
  service_principal_id = azuread_service_principal.service_principal.object_id

  rotate_when_changed = {
    rotation = time_rotating.main.id
  }
}

# secrets
resource "azurerm_key_vault_secret" "service_principal_objectid" {
  key_vault_id = azurerm_key_vault.thinkexception.id
  name         = "service-principal-objectid"
  value        = azuread_service_principal.service_principal.application_id
}

resource "azurerm_key_vault_secret" "service_principal_clientid" {
  key_vault_id = azurerm_key_vault.thinkexception.id
  name         = "service-principal-clientid"
  value        = azuread_service_principal.service_principal.application_id
}

resource "azurerm_key_vault_secret" "service_principal_clientsecret" {
  key_vault_id = azurerm_key_vault.thinkexception.id
  name         = "service-principal-clientsecret"
  value        = azuread_service_principal_password.service_principal_password.value
}


# role assignments
resource "azurerm_role_assignment" "KeyVaultSecretsOfficeToServicePrincipal" {
  principal_id         = azuread_service_principal.service_principal.object_id
  scope                = data.azurerm_subscription.current.id
  role_definition_name = "Key Vault Secrets Officer"
}

resource "azurerm_role_assignment" "ContributorOnApplicationToServicePrincipal" {
  principal_id         = azuread_service_principal.service_principal.object_id
  scope                = data.azurerm_subscription.current.id
  role_definition_name = "Contributor"
}

resource "azurerm_role_assignment" "UserAccessAdminOnApplicationToServicePrincipal" {
  principal_id         = azuread_service_principal.service_principal.object_id
  scope                = data.azurerm_subscription.current.id
  role_definition_name = "User Access Administrator" 
}

resource "azurerm_role_assignment" "NetworkContributorToServicePrincipal" {
  principal_id         = azuread_service_principal.service_principal.object_id
  scope                = data.azurerm_subscription.current.id
  role_definition_name = "Network Contributor" 
}

resource "azurerm_role_assignment" "ReaderToServicePrincipal" {
  scope                            = azurerm_container_registry.thinkexception.id
  role_definition_name             = "Reader"
  principal_id                     = azuread_service_principal.service_principal.object_id
  skip_service_principal_aad_check = true
}