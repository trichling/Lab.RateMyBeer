variable "environment" {
  type = string
}

variable "application" {
  type = string
  default = "ratemybeer"
}

variable "hub_virtualnetwork_address_space" {
  type = string
  default = "10.0.0.0/22"
}

variable "applicationgateway_subnet_address_space" {
  type = string
}