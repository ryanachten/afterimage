using afterimage.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using static afterimage.Shared.Constants.Api;

namespace afterimage.Client.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        protected IJSRuntime _jsRuntime { get; set; }
        [Inject]
        protected HttpClient _client { get; set; }
        [Inject]
        protected NavigationManager _navigationManager { get; set; }

        protected string _email = "";
        protected string _password = "";
        private readonly JsonSerializerOptions _jsonSettings;

        public Login()
        {
            _jsonSettings = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        protected override async Task OnInitializedAsync()
        {
           var token = await RetriveToken();
            if(token != null)
            {
                SetAuthorisationHeaders(token);
                NavigateToHome();
            }
        }

        protected async void LoginUser()
        {
            // TODO: add user-facing validation
            if (_email.Length == 0 || _password.Length == 0) return;
            
            var request = new LoginRequest(_email, _password);
            var response = await _client.PostAsJsonAsync(Endpoints.Authentication, request);
            
            var json = await response.Content.ReadAsStringAsync();
            var loginDetails = JsonSerializer.Deserialize<LoginResponse>(json, _jsonSettings);

            if (loginDetails?.Token != null)
            {
                await StoreToken(loginDetails.Token);
                SetAuthorisationHeaders(loginDetails.Token);
                NavigateToHome();
            }
            else
            {
                // TODO: add better error handling here
            }
        }

        private async Task<AuthenticationToken?> RetriveToken()
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
            if (json != null)
            {
                var token = JsonSerializer.Deserialize<AuthenticationToken>(json, _jsonSettings);
                return token;
            }
            else
            {
                // TODO: add better error handling here
                return null;
            }
        }

        private async Task StoreToken(AuthenticationToken token)
        {
            var json = JsonSerializer.Serialize(token);
            await _jsRuntime.InvokeAsync<string>("localStorage.setItem", "token", json);
        }

        private void SetAuthorisationHeaders(AuthenticationToken token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        private void NavigateToHome()
        {
            _navigationManager.NavigateTo("/home");
        }
    }
}
