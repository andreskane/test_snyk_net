provider "azurerm" {
  #version = "~> 2.16.0"
  subscription_id = var.subscription_id
  client_id = var.client_id
  client_secret = var.client_secret
  tenant_id = var.tenant_id
  skip_provider_registration = true
  features {}
}



variable "subscription_id" {
  description = "Id de la subscripcion"
}

variable "client_id" {
  description = "Id de la SA"
}

variable "client_secret" {
  description = "password Generado"
}

variable "tenant_id" {
  description = "tenant id"
}