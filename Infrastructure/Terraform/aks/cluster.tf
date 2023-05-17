resource "azurerm_subnet" "cluster_subnet" {
  address_prefixes     = [var.cluster_subnet_address_space]
  name                 = "${var.environment}-${var.application}-${var.version_number}"
  resource_group_name  = data.azurerm_resource_group.ratemybeer.name
  virtual_network_name = data.azurerm_virtual_network.ratemybber.name
}

resource "azurerm_kubernetes_cluster" "rate_my_beer_cluster" {
  name                = "${var.environment}-${var.application}-${var.version_number}"
  resource_group_name = data.azurerm_resource_group.ratemybeer.name
  location            = data.azurerm_resource_group.ratemybeer.location
  dns_prefix          = "ratemybeer-${var.version_number}"

  default_node_pool {
    name           = "default"
    node_count     = var.node_count
    vm_size        = "Standard_B2s"
    vnet_subnet_id = azurerm_subnet.cluster_subnet.id
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

# service principal must be network contributor on mc resource group

data "azurerm_resource_group" "cluster_mc_ressource_group" {
  depends_on = [azurerm_kubernetes_cluster.rate_my_beer_cluster]
  name = "MC_${data.azurerm_resource_group.ratemybeer.name}_${var.environment}-${var.application}-${var.version_number}_${data.azurerm_resource_group.ratemybeer.location}"
}

resource "azurerm_role_assignment" "network_contributor_to_service_principal" {
  principal_id = data.azurerm_key_vault_secret.service_principal_objectId.value
  scope        = data.azurerm_resource_group.cluster_mc_ressource_group.id
  role_definition_name = "Network Contributor"
}