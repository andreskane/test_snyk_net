resource "azurerm_api_management_api" "apimapi" {
  name                  = var.name
  resource_group_name   = var.rg_name
  api_management_name   = var.apim_name
  revision              = var.revision
  description           = var.description
  display_name          = var.display_name
  path                  = var.path
  service_url           = var.service_url
  protocols             = ["${var.protocol}"]
  subscription_required = true

  subscription_key_parameter_names {
    header = "Ocp-Apim-Subscription-Key"
    query  = "subscription-key"
  }

  import {
    content_format = var.content_format
    content_value  = var.swagger_json
  }
}
