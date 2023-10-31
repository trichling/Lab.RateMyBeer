resource "azurerm_public_ip" "hub_vpn_pip" {
    name                = "pip-vpn"
    location            = var.location
    resource_group_name = azurerm_resource_group.hub.name
    allocation_method   = "Dynamic"
}


resource "azurerm_virtual_network_gateway" "hub_vpn_gateway" {
        name                = "vgw-hub"
        location            = var.location
        resource_group_name = azurerm_resource_group.hub.name
        
        type                = "Vpn"
        sku                 = "Basic"
        generation          = "Generation1"
        vpn_type            = "RouteBased"

        ip_configuration {
            name                          = "ipconfig-hub"
            subnet_id                     = azurerm_subnet.gateway_subnet.id
            public_ip_address_id          = azurerm_public_ip.hub_vpn_pip.id
            private_ip_address_allocation = "Dynamic"
        }    

          vpn_client_configuration {
            address_space = [var.vpn_client_subnet_address_space]

            vpn_client_protocols = ["OpenVPN"]

            vpn_auth_types = ["AAD"]

            # To register the Azuer VPN app paste this in your browser
            # https://login.microsoftonline.com/<Entra Tennant Id>/oauth2/authorize?client_id=41b23e61-6c1e-4545-b367-cd054e0ed4b4&response_type=code&redirect_uri=https://portal.azure.com&nonce=1234&prompt=admin_consent
            # then use the apps application id here
            aad_audience   = var.vpn_config_aad_audience
            aad_issuer     = "https://sts.windows.net/${data.azuread_client_config.current.tenant_id}/"
            aad_tenant     = "https://login.microsoftonline.com/${data.azuread_client_config.current.tenant_id}"
          }
}

