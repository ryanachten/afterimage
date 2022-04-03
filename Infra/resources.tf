resource "aws_s3_bucket" "image_storage" {
  bucket = var.image_storage_bucket_name
}

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
