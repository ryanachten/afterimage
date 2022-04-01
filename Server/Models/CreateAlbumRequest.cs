namespace afterimage.Server.Models
{
    public class CreateAlbumRequest
    {
        public string Title { get; set; }
        public List<IFormFile> Images { get; set; } = new();
    }
}
