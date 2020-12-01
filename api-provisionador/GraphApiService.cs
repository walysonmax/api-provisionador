using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace api_provisionador
{
    public class GraphApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AzureAdOptions _authenticationOptions;
        public GraphApiService(IHttpClientFactory clientFactory, 
                                IOptions<AzureAdOptions> authenticationOptions)
        {
            _clientFactory = clientFactory;
            _authenticationOptions = authenticationOptions.Value;
        }

        public async Task<IList<User>> GetGraphApiUser()
        {
            return await Create().Users.Request().GetAsync();
        }


        public IGraphServiceClient Create()
        {
          

            GraphServiceClient graphClient;

            try
            {
                // Initiate client application
                var confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(_authenticationOptions.ClientId)
                    .WithTenantId(_authenticationOptions.TenantId)
                    .WithClientSecret(_authenticationOptions.ClientSecret)
                    .Build();

                // Create the auth provider
                //var authProvider = new ClientCredentialProvider(confidentialClientApplication);

                var scopes = new string[] { "https://graph.microsoft.com/.default" };
                // Create Graph Service Client
                graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) => {

                    // Retrieve an access token for Microsoft Graph (gets a fresh token if needed).
                    var authResult = await confidentialClientApplication.AcquireTokenForClient(scopes).ExecuteAsync();

                    // Add the access token in the Authorization header of the API
                    requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

                }));
            }
            catch (ServiceException ex)
            {
                throw ex;
            };

            // Return
            return graphClient;
        }
    }
}
