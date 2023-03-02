terraform {

  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = ">= 3.43"
    }
  }


}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "ratemybeer" {
  name     = "${var.environment}-${var.application_name}"
  location = "West Europe"
}

resource "azurerm_log_analytics_workspace" "ratemybeer" {
  name                = "${var.environment}-${var.application_name}"
  location            = azurerm_resource_group.ratemybeer.location
  resource_group_name = azurerm_resource_group.ratemybeer.name
  sku                 = "PerGB2018"
  retention_in_days   = 30
}

#resource "azurerm_storage_account" "ratemybeer" {
#  name                     = "${var.environment}-${var.application_name}"
#  resource_group_name      = azurerm_resource_group.ratemybeer.name
#  location                 = azurerm_resource_group.ratemybeer.location
#  account_tier             = "Standard"
#  account_replication_type = "LRS"
#}