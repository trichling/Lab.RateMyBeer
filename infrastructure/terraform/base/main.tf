terraform {
  backend "azurerm" {}

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">= 2.26"
    }

    azuread = {
      source  = "hashicorp/azuread"
      version = ">= 2.30.0"
    }
    
  }

  required_version = ">= 0.14.9"
}

provider "azurerm" {
  features {}
}

provider "azuread" {
}

## resource group
resource "azurerm_resource_group" "RateMyBeerRessourceGroup" {
  name     = "${var.environment}-${var.application}"
  location = "westeurope"
}



