resource "azurerm_api_management_api_version_set" "api_version_set" {
  name                = var.name
  resource_group_name = var.rg_name
  api_management_name = var.apim_name
  display_name        = var.display_name
  versioning_scheme   = var.scheme
}