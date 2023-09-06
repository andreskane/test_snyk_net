resource "azurerm_api_management_product_api" "api2prod" {
  api_name            = var.api_name
  product_id          = var.product_id
  api_management_name = var.apim_name
  resource_group_name = var.rg_name
  depends_on = [var.dependency]
}