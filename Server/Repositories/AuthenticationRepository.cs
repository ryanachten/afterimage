using afterimage.Server.Models;
using afterimage.Shared.Models;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;

namespace afterimage.Server.Repositories
{
    // Useful references for .NET / AWS authentication:
    // - https://referbruv.com/blog/posts/implementing-cognito-user-login-and-signup-in-aspnet-core-using-aws-sdk
    // - https://aws.amazon.com/blogs/developer/introducing-the-asp-net-core-identity-provider-preview-for-amazon-cognito/
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AmazonCognitoIdentityProviderClient _client;
        private readonly CognitoUserPool _userPool;

        public AuthenticationRepository(IConfiguration config)
        {
            var awsConfig = config.GetSection("AWS").Get<AwsConfiguration>();
            
            _client = new AmazonCognitoIdentityProviderClient();
            _userPool = new CognitoUserPool(awsConfig.UserPoolId, awsConfig.UserPoolClientId, _client);
        }

        public async Task<LoginResponse> Login(string email, string password)
        {
            var user = new CognitoUser(email, _userPool.ClientID, _userPool, _client);
            var request = new InitiateSrpAuthRequest()
            {
                Password = password
            };
            var response = await user.StartWithSrpAuthAsync(request);
            var result = response.AuthenticationResult;

            return new LoginResponse()
            {
                UserId = user.UserID,
                UserName = user.Username,
                Token = new AuthenticationToken()
                {
                    IdToken = result.IdToken,
                    AccessToken = result.AccessToken,
                    ExpiresIn = result.ExpiresIn,
                    RefreshToken = result.RefreshToken,
                }
            };
        }
    }
}
