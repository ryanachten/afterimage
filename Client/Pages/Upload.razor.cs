using afterimage.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static afterimage.Shared.Constants.Api;

namespace afterimage.Client.Pages
{
    public partial class Upload : ComponentBase
    {
        [Inject]
        protected HttpClient _client { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var albums = await _client.GetFromJsonAsync<GetAlbumsResponse>(Endpoints.Album);
        }
    }
}
