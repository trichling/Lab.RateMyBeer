﻿resource "azurerm_virtual_network" "ratemybeer" {
name                = "${var.environment}-${var.application_name}"
address_space       = ["10.40.0.0/20"]
location            = azurerm_resource_group.ratemybeer.location
resource_group_name = azurerm_resource_group.ratemybeer.name
}

resource "azurerm_subnet" "ratemybeer_container_app_environment" {
name = "${var.environment}-${var.application_name}"
address_prefixes = ["10.40.0.0/23"]
resource_group_name = azurerm_resource_group.ratemybeer.name
virtual_network_name = azurerm_virtual_network.ratemybeer.name
}

resource "azurerm_private_dns_zone" "azurecontainerapps" {
name                = azurerm_container_app_environment.ratemybeer.default_domain
resource_group_name = azurerm_resource_group.ratemybeer.name
}

resource "azurerm_private_dns_zone_virtual_network_link" "azurecontainerapps_to_dev_ratemybeer" {
name                  = "azurecontainerapps_to_${var.environment}-${var.application_name}"
resource_group_name   = azurerm_resource_group.ratemybeer.name
private_dns_zone_name = azurerm_private_dns_zone.azurecontainerapps.name
virtual_network_id    = azurerm_virtual_network.ratemybeer.id
# registration_enabled = true ??
}

resource "azurerm_private_dns_a_record" "resolve_all_names_to_container_app_environment_ip" {
name                = "*"
resource_group_name = azurerm_resource_group.ratemybeer.name
ttl                 = 60
zone_name           = azurerm_private_dns_zone.azurecontainerapps.name
records             = [azurerm_container_app_environment.ratemybeer.static_ip_address]
}