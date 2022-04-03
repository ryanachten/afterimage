namespace afterimage.Shared.Models
{
    public class LoginRequest
    {
        public string Email { get; set; }
        // TODO: password should probably not be clear text in transit
        public string Password { get; set; }

        public LoginRequest()
        {

        }

        public LoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
