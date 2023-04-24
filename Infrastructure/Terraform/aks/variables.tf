variable "environment" {
  type = string
}

variable "application" {
  type = string
  default = "ratemybeer"
}

variable "version_number" {
  type = string
}

variable "cluster_subnet_address_space" {
  type = string
}

variable "node_count" {
  type = number
}