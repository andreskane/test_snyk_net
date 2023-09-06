resource "azurerm_api_management_api_policy" "policy" {
  api_name            = var.name
  api_management_name = var.apim_name
  resource_group_name = var.rg_name

  xml_content = var.policy_xml
  depends_on = [var.dependency]
}