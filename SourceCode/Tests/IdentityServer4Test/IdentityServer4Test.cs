using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServer4Test
{
    public class IdentityServer4Test
    {
        [Fact]
        public async Task ClientCredentials_Test()
        {
            // request token
            var httpClient = new HttpClient();

            var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5999/");
            //var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            //var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");

            var token = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "all"
            });

            Assert.False(token.IsError);
            Console.WriteLine(token.Json);

            // call api
            //var client = new HttpClient();
            //client.SetBearerToken(tokenResponse.AccessToken);

            //var response = await client.GetAsync("http://localhost:5010/values");
            //Assert.True(response.IsSuccessStatusCode);
            //var content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(content);
        }
    }
}