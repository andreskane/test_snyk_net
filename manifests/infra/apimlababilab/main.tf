terraform {
  backend "azurerm" {
    resource_group_name  = "#{tf_sa_rg_name}#"
    storage_account_name = "#{tf_sa_name}"
    container_name       = "#{proyecto}#"
    key                  = "#{api_lowercase_name}#-#{env_short_code}#-#{proyecto}#.terraform.tfstate"
  }
}
locals {
  api             = "#{api_lowercase_name}#"
  api_serviceUrl  = "https://#{api_lowercase_name}#-#{url_subdomain}#"
  api_description = "#{api_long_description}# #{BranchVersion}#"
  rg_name         = "#{apim_rg_name}#"
  apim_name       = "#{apim_name}#"
  product_id      = "#{api_product_id}#"
  env             = "#{env_short_code}#"
}

# Version Set (Common)

module "api_version_set" {
  source         = "../tf_modules/apim_api_version_set"
  name           = "${local.api}-${local.env}"
  rg_name        = local.rg_name
  apim_name      = local.apim_name
  display_name   = "${local.api}-${local.env}"
  scheme         = "Segment"
}

module "api_product" {
  source     = "../tf_modules/apim_product"
  api_name   = "${local.api}-${local.env}"
  rg_name    = local.rg_name
  apim_name  = local.apim_name
  product_id = local.product_id
  dependency = module.api.apimapi_id
}

# API Version 1

module "api" {
  source             = "../tf_modules/apim_api_version"
  name               = "${local.api}-${local.env}"
  rg_name            = local.rg_name
  apim_name          = local.apim_name
  revision           = "1"
  description        = "${local.api_description} _ Version 1"
  display_name       = "${local.api}-${local.env}"
  path               = "${local.api}-${local.env}"
  protocol           = "https"
  service_url        = local.api_serviceUrl
  content_format     = "openapi"
  swagger_json       = "${file("apis/${local.api}_swagger_v1.json")}"
  version_identifier = "v1"
  version_set_id     = module.api_version_set.id
}


# API Version 2

module "api_v2" {
  source             = "../tf_modules/apim_api_version"
  name               = "${local.api}-${local.env}-v2"
  rg_name            = local.rg_name
  apim_name          = local.apim_name
  revision           = "1"
  description        = "${local.api_description} _ Version 2"
  display_name       = "${local.api}-${local.env}"
  path               = "${local.api}-${local.env}"
  protocol           = "https"
  service_url        = local.api_serviceUrl
  content_format     = "openapi"
  swagger_json       = "${file("apis/${local.api}_swagger_v2.json")}"
  version_identifier = "v2"
  version_set_id     = module.api_version_set.id
}

#module "api_policy" {
#  source     = "../tf_modules/apim_api_policy"
#  name       = "${local.api}-${local.env}"
#  rg_name    = local.rg_name
#  apim_name  = local.apim_name
#  policy_xml = "${file("apis/${local.api}_policy.xml")}"
#  dependency = module.api.apimapi_id
#}
#module "operation_post_policy" {
#  source       = "../tf_modules/apim_operation_policy"
#  name         = "${local.api}-${local.env}"
#  rg_name      = local.rg_name
#  apim_name    = local.apim_name
#  policy_xml   = "${file("apis/${local.api}/operation_post_policy.xml")}"
#  dependency   = module.api.apimapi_id
#  operation_id = "post"
#}


#module "api_product" {
#  source     = "../tf_modules/apim_product"
#  api_name   = "${local.api}-${local.env}"
#  rg_name    = local.rg_name
#  apim_name  = local.apim_name
#  product_id = local.product_id
#  dependency = module.api_v2.apimapi_id
#}
#module "api_policy" {
#  source     = "../tf_modules/apim_api_policy"
#  name       = "${local.api}-${local.env}"
#  rg_name    = local.rg_name
#  apim_name  = local.apim_name
#  policy_xml = "${file("apis/${local.api}/${local.api}_policy.xml")}"
#  dependency = module.api_v2.apimapi_id
#}
#module "operation_post_policy" {
#  source       = "../tf_modules/apim_operation_policy"
#  name         = "${local.api}-${local.env}"
#  rg_name      = local.rg_name
#  apim_name    = local.apim_name
#  policy_xml   = "${file("apis/${local.api}/operation_post_policy.xml")}"
#  dependency   = module.api_v2.apimapi_id
#  operation_id = "post"
#}
