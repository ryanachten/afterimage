using afterimage.Server.Constants;
using afterimage.Server.Models;
using Amazon.S3;
using Amazon.S3.Model;

namespace afterimage.Server.Services
{
    public class StorageService : IStorageService
    {
        private readonly AmazonS3Client _client;

        public StorageService()
        {
            _client = new AmazonS3Client();
        }

        public async Task<bool> UploadFiles(UploadFilesRequest request)
        {
            // TODO: need to handle all files, just not the first one
            var file = request.Files[0];
            using var stream = file.OpenReadStream();
            try
            {
                var res = await _client.PutObjectAsync(new PutObjectRequest()
                {
                    BucketName = Storage.ImageBucketName,
                    InputStream = stream,
                    Key = file.FileName,
                });
                var httpStatus = res.HttpStatusCode;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
