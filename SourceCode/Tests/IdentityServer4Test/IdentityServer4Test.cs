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

        [Fact]
        public async Task ResourceOwnerPassword_Test()
        {
            var httpClient = new HttpClient();

            var disco = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000/");

            Assert.False(disco.IsError);

            var token = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "wpf client",
                ClientSecret = "77DAABEF-697A-4CC1-A400-3CC561B9AD83",
                Scope = "api1 openid profile",
                UserName = "alice",
                Password = "123456"
            });

            //output.WriteLine(token.Json.ToString());
            Assert.False(token.IsError);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(token.AccessToken);

            var respones1 = await apiClient.GetAsync(disco.UserInfoEndpoint);
            var content1 = await respones1.Content.ReadAsStringAsync();
            output.WriteLine(respones1.StatusCode.ToString());
            output.WriteLine(respones1.Headers.ToString());
            output.WriteLine(content1);
            output.WriteLine(await respones1.Content.ReadAsStringAsync());
            Assert.True(respones1.IsSuccessStatusCode);

            var response = await apiClient.GetAsync("http://localhost:5002/api/Companies");
            var content = await response.Content.ReadAsStringAsync();
            output.WriteLine(response.StatusCode.ToString());
            output.WriteLine(response.Headers.ToString());
            output.WriteLine(content);
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}