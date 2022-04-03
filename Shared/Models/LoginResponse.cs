namespace afterimage.Shared.Models
{
    public class LoginResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public AuthenticationToken Token { get; set; }
    }
}
