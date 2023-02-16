
resource "azurerm_mssql_server" "ratemybeer" {
  name                         = "${var.environment}-${var.application_name}"
  resource_group_name          = azurerm_resource_group.ratemybeer.name
  location                     = azurerm_resource_group.ratemybeer.location
  version                      = "12.0"
  administrator_login          = "superadmin"
  administrator_login_password = var.sql_server_sa_password
}



resource "azurerm_mssql_database" "checkinsdb" {
  name           = "checkinsdb"
  server_id      = azurerm_mssql_server.ratemybeer.id
  collation      = "SQL_Latin1_General_CP1_CI_AS"
  license_type   = "LicenseIncluded"
  max_size_gb    = 1
  sku_name       = "Basic"
}

resource "azurerm_mssql_database" "ratingsdb" {
  name                = "ratingsdb"
  server_id      = azurerm_mssql_server.ratemybeer.id
  collation      = "SQL_Latin1_General_CP1_CI_AS"
  license_type   = "LicenseIncluded"
  max_size_gb    = 1
  sku_name       = "Basic"
}

resource "azurerm_mssql_database" "commentsdb" {
  name                = "commentsdb"
  server_id      = azurerm_mssql_server.ratemybeer.id
  collation      = "SQL_Latin1_General_CP1_CI_AS"
  license_type   = "LicenseIncluded"
  max_size_gb    = 1
  sku_name       = "Basic"
}