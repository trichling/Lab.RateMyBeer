resource "azurerm_container_app" "comments" {
  name                         = "comments"
  container_app_environment_id = azurerm_container_app_environment.ratemybeer.id
  resource_group_name          = azurerm_resource_group.ratemybeer.name
  revision_mode                = "Single"

  template {
    min_replicas = 1
    max_replicas = 10
    
    container {
      name   = "comments"
      image  = "thinkexception.azurecr.io/comments:dev"
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
        name = "ConnectionStrings__CommentsDbConnectionString"
        secret_name = "commentsdb-connectionstring"
      }
    }
  }

  secret {
    name = "nservicebus-connectionstring"
    value = azurerm_servicebus_namespace.ratemybeer.default_primary_connection_string
  }

  secret {
    name = "commentsdb-connectionstring"
    value = local.commentsdb_connectionstring
  }

  secret {
    name = "container-registry-admin-password"
    value = var.container_registry_admin_password
  }

  registry {
    password_secret_name = "container-registry-admin-password"
    username = "thinkexception"
    server = "thinkexception.azurecr.io"
  }
}