variable "location" {
  type = string
  default = "westeurope"
}

variable "hub_network_address_space" {
  type = string
}

variable "vpn_gateway_subnet_address_space" {
  type = string
}

variable "vpn_client_subnet_address_space" {
  type = string
}

variable "vpn_config_aad_audience" {
  type = string
  default = "41b23e61-6c1e-4545-b367-cd054e0ed4b4"
  description = "This is the entraid enterprise application id of the application that holds the groups and users eligibel for login via vpn."
}

variable "firewall_subnet_address_space" {
  type = string
}

variable "private_dns_zone_name" {
  type = string
  default = "dev.thinkex.net"
}