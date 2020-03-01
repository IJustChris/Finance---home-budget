using Finance.Api;
using Finance.Infrastructure.Commands.User;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Tests.EndToEnd.Controllers
{
    [TestFixture]
    public class LoginControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;


        public LoginControllerTests()
        {
            _server = new TestServer(WebHost
                .CreateDefaultBuilder()
                .UseStartup<Startup>());

            _client = _server.CreateClient();
        }

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public async Task Login_ValidData_ShouldSucced()
        {
            var request = new Login
            {
                Email = "user1@test.com",
                Password = "VerySecretPassword"
            };


            var payload = GetPayload(request);

            var response = await _client.PostAsync("login", payload);

            //ASSERT
            response.IsSuccessStatusCode.Should().BeTrue();

        }

        [Test]
        public async Task Login_InvalidData_ShouldNotSucced()
        {
            var request = new Login
            {
                Email = "use@test.com",
                Password = "V"
            };


            var payload = GetPayload(request);

            var response = await _client.PostAsync("login", payload);

            //ASSERT


        }


        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
