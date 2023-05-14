## sql server
resource "azurerm_sql_server" "ratemybeer" {
  resource_group_name          = azurerm_resource_group.RateMyBeerRessourceGroup.name
  location                     = azurerm_resource_group.RateMyBeerRessourceGroup.location
  name                         = "${var.environment}-${var.application}"
  version                      = "12.0"
  administrator_login          = var.sql_server_administrator_login
  administrator_login_password = var.sql_server_administrator_login_password
}

resource "azurerm_key_vault_secret" "connectionstringtemplate" {
  key_vault_id = azurerm_key_vault.ratemybeer.id
  name         = "connectionstringtemplate"
  value        = "Server=${azurerm_sql_server.ratemybeer.fully_qualified_domain_name};Database={0};User Id=${var.sql_server_administrator_login};Password=${var.sql_server_administrator_login_password};MultipleActiveResultSets=true"
}

## databases
resource "azurerm_sql_database" "CheckinsDb" {
  location            = azurerm_resource_group.RateMyBeerRessourceGroup.location
  resource_group_name = azurerm_resource_group.RateMyBeerRessourceGroup.name
  name                = "CheckinsDb"
  server_name         = azurerm_sql_server.ratemybeer.name
}

resource "azurerm_sql_database" "CheckinsDb" {
  location            = azurerm_resource_group.RateMyBeerRessourceGroup.location
  resource_group_name = azurerm_resource_group.RateMyBeerRessourceGroup.name
  name                = "RatingsDb"
  server_name         = azurerm_sql_server.ratemybeer.name
}

resource "azurerm_sql_database" "CheckinsDb" {
  location            = azurerm_resource_group.RateMyBeerRessourceGroup.location
  resource_group_name = azurerm_resource_group.RateMyBeerRessourceGroup.name
  name                = "CommentsDb"
  server_name         = azurerm_sql_server.ratemybeer.name
}