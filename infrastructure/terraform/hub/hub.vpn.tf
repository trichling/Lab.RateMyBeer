resource "azurerm_public_ip" "hub_vpn_pip" {
    name                = "pip-vpn"
    location            = var.location
    resource_group_name = azurerm_resource_group.hub.name
    allocation_method   = "Static"
    sku                 = "Standard"
}


resource "azurerm_virtual_network_gateway" "hub_vpn_gateway" {
        name                = "vgw-hub"
        location            = var.location
        resource_group_name = azurerm_resource_group.hub.name
        
        type                = "Vpn"
        sku                 = "VpnGw2AZ"
        vpn_type            = "RouteBased"
        generation          = "Generation2"

        ip_configuration {
            name                          = "ipconfig-hub"
            subnet_id                     = azurerm_subnet.gateway_subnet.id
            public_ip_address_id          = azurerm_public_ip.hub_vpn_pip.id
            private_ip_address_allocation = "Dynamic"
        }    
}

