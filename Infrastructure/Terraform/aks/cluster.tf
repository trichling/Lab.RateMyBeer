resource "azurerm_kubernetes_cluster" "RateMyBeerCluster" {
  name = "RateMyBeerCluster${var.ressourceNameSuffix}"
  resource_group_name = azurerm_resource_group.RateMyBeerRessourceGroup.name
  location = azurerm_resource_group.RateMyBeerRessourceGroup.location
  dns_prefix = "ratemybeer${var.ressourceNameSuffix}"
  default_node_pool {
    name = "default"
    node_count = var.node_count
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

