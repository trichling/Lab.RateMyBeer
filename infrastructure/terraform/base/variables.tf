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

variable "sql_server_administrator_login" {
  type = string
}

variable "sql_server_administrator_login_password" {
  type = string
}