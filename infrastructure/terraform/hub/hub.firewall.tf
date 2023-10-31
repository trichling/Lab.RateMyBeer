resource "azurerm_public_ip" "hub_firewall_pip" {
    name                = "pip-firewall"
    location            = var.location
    resource_group_name = azurerm_resource_group.hub.name
    allocation_method   = "Static"
    sku                 = "Standard"
}

resource "azurerm_firewall_policy" "hub_fw_policy" {
    name                = "fw-policy-hub"
    location            = var.location
    resource_group_name = azurerm_resource_group.hub.name
    sku = "Standard"
}

resource "azurerm_firewall_policy_rule_collection_group" "standard_rules" {
  name               = "example-fwpolicy-rcg"
  firewall_policy_id = azurerm_firewall_policy.hub_fw_policy.id
  priority           = 500
 network_rule_collection {
    name     = "network_rule_collection1"
    priority = 400
    action   = "Deny"
    rule {
      name = "DNS"
      destination_addresses = [ "*" ]
      source_addresses = [ "*" ]
      destination_ports = [ "53" ]
      protocols = [ "UDP" ]
    }
  }

}

resource "azurerm_firewall" "hub_firewall" {
    name                = "afw-hub"
    location            = var.location
    resource_group_name = azurerm_resource_group.hub.name
    sku_name            = "AZFW_VNet"
    sku_tier            = "Standard"

    firewall_policy_id = azurerm_firewall_policy.hub_fw_policy.id

    ip_configuration {
        name                 = "fw-pip"
        subnet_id            = azurerm_subnet.firewall_subnet.id
        public_ip_address_id = azurerm_public_ip.hub_firewall_pip.id
    }
}