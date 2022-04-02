resource "aws_s3_bucket" "image_storage" {
  bucket = var.image_storage_bucket_name
}
