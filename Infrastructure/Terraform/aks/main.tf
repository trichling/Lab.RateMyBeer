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


