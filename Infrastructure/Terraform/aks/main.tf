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
  name     = "Lab_RateMyBeer${var.ressourceNameSuffix}"
  location = "westeurope"
}

data "azurerm_container_registry" "container_registry" {
  name = "thinkexception"
  resource_group_name = "Infrastructure"
}

resource "azurerm_kubernetes_cluster" "RateMyBeerCluster" {
  name = "RateMyBeerCluster${var.ressourceNameSuffix}"
  resource_group_name = azurerm_resource_group.RateMyBeerRessourceGroup.name
  location = azurerm_resource_group.RateMyBeerRessourceGroup.location
  dns_prefix = "ratemybeer${var.ressourceNameSuffix}"
  default_node_pool {
    name = "default"
    node_count = 3
    vm_size    = "Standard_B2s"
  }

  identity {
    type = "SystemAssigned"
  }
}

# --attach-acr
resource "azurerm_role_assignment" "acrpull_role" {
  scope                            = data.azurerm_container_registry.container_registry.id
  role_definition_name             = "AcrPull"
  # This refers to the identity of the kluster itself
  #   principal_id                     = azurerm_kubernetes_cluster.RateMyBeerCluster.identity[0].principal_id
  # This refers to the identity of the agentpool 
  principal_id                     = azurerm_kubernetes_cluster.RateMyBeerCluster.kubelet_identity[0].object_id
  skip_service_principal_aad_check = true
}

provider "helm" {
    kubernetes {
        host     = azurerm_kubernetes_cluster.RateMyBeerCluster.kube_config.0.host
        client_key             = base64decode(azurerm_kubernetes_cluster.RateMyBeerCluster.kube_config.0.client_key)
        client_certificate     = base64decode(azurerm_kubernetes_cluster.RateMyBeerCluster.kube_config.0.client_certificate)
        cluster_ca_certificate = base64decode(azurerm_kubernetes_cluster.RateMyBeerCluster.kube_config.0.cluster_ca_certificate)
    }  
}

resource "helm_release" "nginx_ingress" {
  name       = "ingress-nginx"
  namespace        = "ingress-nginx"
  create_namespace = true
  repository  = "https://kubernetes.github.io/ingress-nginx"
  chart      = "ingress-nginx"

  set {
    name  = "controller.replicaCount"
    value = "3"
  }
  set {
    name  = "controller.nodeSelector.kubernetes\\.io/os" 
    value = "linux"
  }
  set {
    name  = "defaultBackend.nodeSelector.kubernetes\\.io/os"
    value = "linux"
  }
  set {
    name = "controller.admissionWebhooks.patch.nodeSelector.kubernetes\\.io/os"
    value = "linux"
  }
}

resource "helm_release" "cert_manager" {
  name       = "cert-manager"
  namespace        = "cert-manager"
  create_namespace = true
  repository  = "https://charts.jetstack.io"
  chart      = "cert-manager"

  set {
    name  = "installCRDs"
    value = "true"
  }

}