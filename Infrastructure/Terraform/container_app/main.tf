terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = ">= 3.46"
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
