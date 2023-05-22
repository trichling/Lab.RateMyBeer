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

## network
resource "azurerm_virtual_network" "spoke" {
  name                = "${var.environment}-${var.application}"
  resource_group_name = azurerm_resource_group.RateMyBeerRessourceGroup.name
  location            = azurerm_resource_group.RateMyBeerRessourceGroup.location
  address_space       = var.spoke_vnet_address_spaces
}

