locals {
  commentsdb_connectionstring = "Server=tcp:dev-ratemybeer.database.windows.net,1433;Initial Catalog=commentsdb;Persist Security Info=False;User ID=superadmin;Password=${var.sql_server_sa_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

resource "azurerm_container_app" "commentsapi" {
  name                         = "commentsapi"
  container_app_environment_id = azurerm_container_app_environment.ratemybeer.id
  resource_group_name          = azurerm_resource_group.ratemybeer.name
  revision_mode                = "Single"

  template {
    min_replicas = 1
    max_replicas = 10
    
    container {
      name   = "commentsapi"
      image  = "thinkexception.azurecr.io/commentsapi:dev"
      cpu    = 0.25
      memory = "0.5Gi"

      env {
        name = "ASPNETCORE_ENVIRONMENT"
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

  ingress {
    external_enabled = false
    target_port = 80
    traffic_weight {
      latest_revision = true
      percentage = 100
    }
  }

  registry {
    password_secret_name = "container-registry-admin-password"
    username = "thinkexception"
    server = "thinkexception.azurecr.io"
  }
}