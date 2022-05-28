using Duende.IdentityServer.Models;
using ApiClient = Duende.IdentityServer.Models.Client;

namespace afterimage.Server.Identity
{
    public static class IdentityConfig
    {
        public readonly static IEnumerable<ApiResource> Resources = new List<ApiResource>
        {
            new ApiResource(Constants.Identity.LocalApi.ScopeName),
        };

        public readonly static IEnumerable<ApiClient> Clients = new List<ApiClient>
        {
            new ApiClient()
            {
                AllowedScopes = { Constants.Identity.LocalApi.ScopeName }
            },
        };
    }
}
