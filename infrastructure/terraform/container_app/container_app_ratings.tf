resource "azurerm_container_app" "ratings" {
  name                         = "ratings"
  container_app_environment_id = azurerm_container_app_environment.ratemybeer.id
  resource_group_name          = azurerm_resource_group.ratemybeer.name
  revision_mode                = "Single"

  template {
    min_replicas = 1
    max_replicas = 10
    
    container {
      name   = "ratings"
      image  = "thinkexception.azurecr.io/ratings:dev"
      cpu    = 0.25
      memory = "0.5Gi"

      env {
        name = "DOTNET_ENVIRONMENT"
        value = local.environment_mapping[var.environment]
      }
      
      env {
        name = "Dependencies__NServiceBus__TransportConnectionString"
        secret_name = "nservicebus-connectionstring"
      }

      env {
        name = "ConnectionStrings__RatingsDbConnectionString"
        secret_name = "ratingsdb-connectionstring"
      }
    }
  }

  secret {
    name = "nservicebus-connectionstring"
    value = azurerm_servicebus_namespace.ratemybeer.default_primary_connection_string
  }

  secret {
    name = "ratingsdb-connectionstring"
    value = local.ratingsdb_connectionstring
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