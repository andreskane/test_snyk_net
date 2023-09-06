resource "azurerm_api_management_api_operation_policy" "oper_policy" {
  api_name            = var.name
  api_management_name = var.apim_name
  resource_group_name = var.rg_name
  operation_id        = var.operation_id

  xml_content = var.policy_xml
  depends_on = [var.dependency]
}