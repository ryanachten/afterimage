using afterimage.Shared.Models;

namespace afterimage.Server.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<LoginResponse> Login(string email, string password);
    }
}
