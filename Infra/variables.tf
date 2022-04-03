variable "server_client_name" {
  description = "Cognito client name used by server"
  type        = string
  default     = "afterimage-server"
}

variable "user_pool_name" {
  description = "Cognito user pool name"
  type        = string
  default     = "afterimage-users"
}

variable "image_storage_bucket_name" {
  description = "S3 bucket name for image storage"
  type        = string
  default     = "afterimage-imagestorage"
}

#### SECRETS - Replaced by process environment variables ####
variable "aws_access_key" {
  description = "Access key for authenticating with AWS"
  type        = string
  sensitive   = true
}

variable "aws_secret_access_key" {
  description = "Secret access key for authenticating with AWS"
  type        = string
  sensitive   = true
}
