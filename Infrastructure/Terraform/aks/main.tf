terraform {
  
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

variable "ressourceNameSuffix" {
  description = "A suffix added to each ressource name. Can be set to empty string for production."
  type = string
  default = ""
}

resource "azurerm_resource_group" "RateMyBeerRessourceGroup" {
  name     = "${var.environment}_ratemybeer"
  location = "westeurope"
}

