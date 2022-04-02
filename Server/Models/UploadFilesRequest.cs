namespace afterimage.Server.Models
{
    public class UploadFilesRequest
    {
        public string FolderName { get; set; } = string.Empty;
        public List<IFormFile> Files { get; set; } = new();
    }
}
