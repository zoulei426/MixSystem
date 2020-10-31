using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace IdentityServer4Test
{
    public class IdentityServer4Test
    {
        private readonly ITestOutputHelper output;

        public IdentityServer4Test(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task ClientCredentials_Test()
        {
            var httpClient = new HttpClient();

            var disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000/");

            Assert.False(disco.IsError);
            //output.WriteLine(disco.Json.ToString());

            var token = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "77DAABEF-697A-4CC1-A400-3CC561B9AD83",
                Scope = "api1",
            });

            output.WriteLine(token.Json.ToString());
            Assert.False(token.IsError);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(token.AccessToken);

            var response = await apiClient.GetAsync("http://localhost:5002/api/Companies");
            var content = await response.Content.ReadAsStringAsync();
            output.WriteLine(response.StatusCode.ToString());
            output.WriteLine(response.Headers.ToString());
            output.WriteLine(content);
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}