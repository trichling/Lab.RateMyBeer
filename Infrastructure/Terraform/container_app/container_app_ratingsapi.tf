locals {
  ratingsdb_connectionstring = "Server=tcp:dev-ratemybeer.database.windows.net,1433;Initial Catalog=ratingsdb;Persist Security Info=False;User ID=superadmin;Password=${var.sql_server_sa_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

resource "azurerm_container_app" "ratingsapi" {
  name                         = "ratingsapi"
  container_app_environment_id = azurerm_container_app_environment.ratemybeer.id
  resource_group_name          = azurerm_resource_group.ratemybeer.name
  revision_mode                = "Single"

  template {
    min_replicas = 0
    max_replicas = 10
    
    container {
      name   = "ratingsapi"
      image  = "thinkexception.azurecr.io/ratingsapi:dev"
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
  
  ingress {
    external_enabled = false
    target_port = 80
    traffic_weight {
      latest_revision = true
      percentage = 100
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