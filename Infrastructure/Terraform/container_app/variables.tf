variable "application_name" {
  type = string
  default = "ratemybeer"
}

variable "environment" {
  type = string
}

variable "container_registry_admin_password" {
  type = string
}

variable "sql_server_sa_password" {
  type = string 
}