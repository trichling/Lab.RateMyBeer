resource "azurerm_container_app" "frontendapi" {
  name                         = "frontendapi"
  container_app_environment_id = azurerm_container_app_environment.ratemybeer.id
  resource_group_name          = azurerm_resource_group.ratemybeer.name
  revision_mode                = "Single"

  template {
    container {
      name   = "frontendapi"
      image  = "thinkexception.azurecr.io/frontendapi:dev"
      cpu    = 0.25
      memory = "0.5Gi"

      env {
        name        = "Dependencies__NServiceBus__TransportConnectionString"
        secret_name = "nservicebus-connectionstring"
      }
      env {
        name  = "Dependencies__APIs__CheckinsApiBaseUrl"
        value = "http://localhost:5000"
      }
    }
  }

  secret {
    name  = "container-registry-admin-password"
    value = var.container_registry_admin_password
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
    password_secret_name = "container-registry-admin-password"
    username             = "thinkexception"
    server               = "thinkexception.azurecr.io"
  }
}