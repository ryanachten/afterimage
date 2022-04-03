using afterimage.Server.Constants;
using afterimage.Server.Models;
using Amazon.S3;
using Amazon.S3.Model;

namespace afterimage.Server.Repositories
{
    public class FileStorageRepository : IFileStorageRepository
    {
        private const string _imageBucketName = Storage.ImageBucketName;
        private const string _userFolderName = "123abc"; // TODO: this will later be derived from user ID
        private readonly AmazonS3Client _client;

        public FileStorageRepository()
        {
            _client = new AmazonS3Client();
        }

        public async Task<IEnumerable<string>> GetFiles()
        {
            var objects = await _client.ListObjectsAsync(new ListObjectsRequest()
            {
                BucketName = _imageBucketName,
                Prefix = _userFolderName,
            });
            // TODO: there's more useful information we might want to use here, i.e. last updated
            var prefixes = objects.CommonPrefixes;

            return objects.S3Objects.Select(o => o.Key);
        }

        public async Task UploadFiles(UploadFilesRequest request)
        {
            var uploads = new List<Task>();
            foreach (var file in request.Files)
            {
                var upload = _client.PutObjectAsync(new PutObjectRequest()
                {
                    BucketName = _imageBucketName,
                    InputStream = file.OpenReadStream(),
                    Key = $"{_userFolderName}/{request.FolderName}/{file.FileName}",
                });
                uploads.Add(upload);
            }

            try
            {
                await Task.WhenAll(uploads);
            }
            catch (Exception)
            {
                // TODO: Add better error handling for uploading images
                throw;
            }
        }
    }
}
