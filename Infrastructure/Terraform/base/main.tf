terraform {
  backend "azurerm" {}
  
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = ">= 2.26"
    }

    helm = {
      source = "hashicorp/helm"
      version = ">= 2.2"
    }

    kubectl = {
      source  = "gavinbunney/kubectl"
      version = ">= 1.7.0"
    }
  }

  required_version = ">= 0.14.9"
}

provider "azurerm" {
  features {}
}

data "azurerm_client_config" "current" {}

## resource group
resource "azurerm_resource_group" "RateMyBeerRessourceGroup" {
  name     = "${var.environment}-${var.application}"
  location = "westeurope"
}

## network
resource "azurerm_virtual_network" "spoke" {
  name                = "${var.environment}-${var.application}"
  resource_group_name = azurerm_resource_group.RateMyBeerRessourceGroup.name
  location            = azurerm_resource_group.RateMyBeerRessourceGroup.location
  address_space       = var.spoke_vnet_address_spaces
}

## Key vault
resource "azurerm_key_vault" "ratemybeer" {
  tenant_id           = data.azurerm_client_config.current.tenant_id
  resource_group_name = azurerm_resource_group.RateMyBeerRessourceGroup.name
  location            = azurerm_resource_group.RateMyBeerRessourceGroup.location
  name                = "${var.environment}-${var.application}"
  sku_name            = "standard"
}

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
  value        = "Server=${azurerm_sql_server.ratemybeer.connection};Database={0};User Id=${var.sql_server_administrator_login};Password=${var.sql_server_administrator_login_password};MultipleActiveResultSets=true"
}