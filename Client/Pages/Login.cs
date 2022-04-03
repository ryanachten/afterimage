using afterimage.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static afterimage.Shared.Constants.Api;

namespace afterimage.Client.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        protected HttpClient _client { get; set; }
        protected string _email = "";
        protected string _password = "";

        protected async void LoginUser()
        {
            // TODO: add user-facing validation
            if (_email.Length == 0 || _password.Length == 0) return;
            
            var request = new LoginRequest(_email, _password);
            await _client.PostAsJsonAsync(Endpoints.Authentication, request);
        }
    }
}
