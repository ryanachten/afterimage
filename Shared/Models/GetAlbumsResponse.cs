namespace afterimage.Shared.Models
{
    public class GetAlbumsResponse
    {
        public GetAlbumsResponse()
        {

        }

        public GetAlbumsResponse(List<string> albums)
        {
            Albums = albums;
        }

        public List<string> Albums { get; set; } = new();
    }
}
