resource "azurerm_container_app" "frontend" {
  name                         = "frontend"
  container_app_environment_id = azurerm_container_app_environment.ratemybeer.id
  resource_group_name          = azurerm_resource_group.ratemybeer.name
  revision_mode                = "Single"

  template {
    min_replicas = 0
    max_replicas = 10
    
    container {
      name   = "frontend"
      image  = "thinkexception.azurecr.io/frontend:dev"
      cpu    = 0.25
      memory = "0.5Gi"

      env {
        name = "BLAZOR_ENVIRONMENT"
        value = local.environment_mapping[var.environment]
      }
      
      env {
        name  = "Dependencies__APIs__CheckinsApiBaseUrl"
        value = "https://${azurerm_container_app.frontendapi.ingress[0].fqdn}/checkins"
      }
    }
  }

  ingress {
    external_enabled           = true
    target_port                = 80
    traffic_weight {
      latest_revision = true
      percentage      = 100
    }
  }

  registry {
    server               = "thinkexception.azurecr.io"
    identity = azurerm_user_assigned_identity.identity.id
  }

  identity {
    type = "UserAssigned"
    identity_ids = [
      azurerm_user_assigned_identity.identity.id
    ]
  }
}