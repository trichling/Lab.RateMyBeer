variable "environment" {
  type = string
}

variable "application" {
  type = string
  default = "ratemybeer"
}

variable "spoke_vnet_address_spaces" {
  type = list(string)
}
