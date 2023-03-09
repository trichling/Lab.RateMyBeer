resource "azurerm_container_app" "frontendapi" {
  name                         = "frontendapi"
  container_app_environment_id = azurerm_container_app_environment.ratemybeer.id
  resource_group_name          = azurerm_resource_group.ratemybeer.name
  revision_mode                = "Single"

  template {
    min_replicas = 1
    max_replicas = 10
    
    container {
      name   = "frontendapi"
      image  = "thinkexception.azurecr.io/frontendapi:dev"
      cpu    = 0.25
      memory = "0.5Gi"

      env {
        name = "ASPNETCORE_ENVIRONMENT"
        value = local.environment_mapping[var.environment]
      }
      
      env {
        name        = "Dependencies__NServiceBus__TransportConnectionString"
        secret_name = "nservicebus-connectionstring"
      }
      
      env {
        name  = "Dependencies__APIs__CheckinsApiBaseUrl"
        value = "https://${azurerm_container_app.checkinsapi.ingress[0].fqdn}"
      }

      env {
        name  = "Dependencies__APIs__RatingsApiBaseUrl"
        value = "https://${azurerm_container_app.ratingsapi.ingress[0].fqdn}"
      }

      env {
        name  = "Dependencies__APIs__CommentsApiBaseUrl"
        value = "https://${azurerm_container_app.commentsapi.ingress[0].fqdn}"
      }
    }
  }

  secret {
    name  = "nservicebus-connectionstring"
    value = azurerm_servicebus_namespace.ratemybeer.default_primary_connection_string
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