resource "azurerm_kubernetes_cluster" "rate_my_beer_cluster" {
  name                = "${var.environment}-${var.application}-${var.version_number}"
  resource_group_name = data.azurerm_resource_group.ratemybeer.name
  location            = data.azurerm_resource_group.ratemybeer.location
  dns_prefix          = "ratemybeer-${var.version_number}"

  default_node_pool {
    name           = "default"
    vnet_subnet_id = azurerm_subnet.cluster_subnet.id
    node_count     = var.node_count
    vm_size        = "Standard_B2s"
  }

  network_profile {
    network_plugin = "kubenet"
  }

  identity {
    type = "SystemAssigned"
  }
}

# --attach-acr
resource "azurerm_role_assignment" "acrpull_role" {
  scope                = data.azurerm_container_registry.container_registry.id
  role_definition_name = "AcrPull"
  # This refers to the identity of the kluster itself
  #   principal_id                     = azurerm_kubernetes_cluster.RateMyBeerCluster.identity[0].principal_id
  # This refers to the identity of the agentpool 
  principal_id                     = azurerm_kubernetes_cluster.rate_my_beer_cluster.kubelet_identity[0].object_id
  skip_service_principal_aad_check = true
}

