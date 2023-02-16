resource "azurerm_container_app" "checkinsapi" {
  name                         = "checkinsapi"
  container_app_environment_id = azurerm_container_app_environment.ratemybeer.id
  resource_group_name          = azurerm_resource_group.ratemybeer.name
  revision_mode                = "Single"

  template {
    container {
      name   = "checkinsapi"
      image  = "thinkexception.azurecr.io/checkinsapi:dev"
      cpu    = 0.25
      memory = "0.5Gi"

      env {
        name = "Dependencies__NServiceBus__TransportConnectionString"
        secret_name = "nservicebus-connectionstring"
      }
    }
  }



  secret {
    name = "nservicebus-connectionstring"
    value = azurerm_servicebus_namespace.ratemybeer.default_primary_connection_string
  }

  ingress {
    external_enabled = false
    target_port = 80
    traffic_weight {
      percentage = 100
    }
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