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

variable "firewall_subnet_address_space" {
  type = string
}

variable "private_dns_zone_name" {
  type = string
  default = "dev.thinkex.net"
}