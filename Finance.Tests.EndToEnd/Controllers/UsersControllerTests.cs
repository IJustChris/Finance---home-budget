using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using Finance.Api;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Finance.Infrastructure.DTO;
using System.Net;
using Finance.Infrastructure.Commands.User;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;

namespace Finance.Tests.EndToEnd.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private string _existingValidEmail;
        private string _notexistingValidEmail;
        private string _uniqueValidEmail;


        public UsersControllerTests()
        {
            _server = new TestServer(WebHost
                .CreateDefaultBuilder()
                .UseStartup<Startup>());

            _client = _server.CreateClient();
        }

        [SetUp]
        public void Setup()
        {
            _existingValidEmail = "user1@test.com";
            _notexistingValidEmail = "user10000@email.pl";
            _uniqueValidEmail = "testendtoend@test.pl";
    }


        [Test]
        public async Task Register_UniqeVailidEmailOnInput_ShouldRegisterNewUser()
        {
            var request = new CreateUser
            {
                Email = _uniqueValidEmail,
                Password = "Password1234567",
                Username = "Christopher"
            };

            var payload = GetPayload(request);     
            
            var response = await _client.PostAsync("users/register",payload);

            //ASSERT
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().Be($"users/{_uniqueValidEmail}");
        }

        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
