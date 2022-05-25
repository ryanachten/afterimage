# User pool
resource "aws_cognito_user_pool" "users" {
  name = var.user_pool_name

  admin_create_user_config {
    allow_admin_create_user_only = true
  }
}

resource "aws_cognito_user_pool_client" "server_client" {
  name         = var.server_client_name
  user_pool_id = aws_cognito_user_pool.users.id
}

# Federated Identity User Pool - used for OIDC authentication
# Configuration based on preferences discussed here:
# https://dev.to/namuny/using-cognito-user-pool-as-an-openid-connect-provider-4n9a
resource "aws_cognito_user_pool" "identity" {
  name = var.identity_pool_name
}

resource "aws_cognito_user_pool_client" "identity_client" {
  name         = var.identity_client_name
  user_pool_id = aws_cognito_user_pool.identity.id
}

resource "aws_cognito_identity_provider" "identity_provider" {
  user_pool_id  = aws_cognito_user_pool.identity.id
  provider_name = "Cognito"
  provider_type = "OIDC"
  provider_details = {
    client_id                 = aws_cognito_user_pool_client.identity_client.id
    authorize_scopes          = "openid"
    attributes_request_method = "GET"
    oidc_issuer               = "https://cognito-idp.${var.region}.amazonaws.com/${aws_cognito_user_pool.identity.id}"
  }

  attribute_mapping = {
    email    = "email"
    username = "sub"
  }
}
