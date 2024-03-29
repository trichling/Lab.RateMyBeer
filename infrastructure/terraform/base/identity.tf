# create a ratemybeer service principal and add clientid / clientsecret into keyvault
# use the same sp for azure devops pipeline

resource "azuread_application" "application" {
  display_name     = "${var.environment}-${var.application}"
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
  key_vault_id = azurerm_key_vault.ratemybeer.id
  name         = "service-principal-objectid"
  value        = azuread_service_principal.service_principal.application_id
}

resource "azurerm_key_vault_secret" "service_principal_clientid" {
  key_vault_id = azurerm_key_vault.ratemybeer.id
  name         = "service-principal-clientid"
  value        = azuread_service_principal.service_principal.application_id
}

resource "azurerm_key_vault_secret" "service_principal_clientsecret" {
  key_vault_id = azurerm_key_vault.ratemybeer.id
  name         = "service-principal-clientsecret"
  value        = azuread_service_principal_password.service_principal_password.value
}


