using afterimage.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static afterimage.Client.Constants.Api;

namespace afterimage.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var albums = await HttpClient.GetFromJsonAsync<GetAlbumsResponse>(Endpoints.Album);
        }
    }
}
