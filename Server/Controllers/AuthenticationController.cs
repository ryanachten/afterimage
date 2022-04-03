using afterimage.Server.Repositories;
using afterimage.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using static afterimage.Shared.Constants.Api;

namespace afterimage.Server.Controllers
{
    [Route(Endpoints.Authentication)]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _repository;

        public AuthenticationController(IAuthenticationRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            var response = await _repository.Login(request.Email, request.Password);
            return Ok(response);
        }
    }
}
