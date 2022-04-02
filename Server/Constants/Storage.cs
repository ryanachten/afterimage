namespace afterimage.Server.Constants
{
    public static class Storage
    {
        /// <summary>
        /// S3 bucket name for storing images. Defined via Terraform variables.tf
        /// </summary>
        public const string ImageBucketName = "afterimage-imagestorage";
    }
}
