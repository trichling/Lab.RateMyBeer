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