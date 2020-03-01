using Finance.Api;
using Finance.Core.Domain;
using Finance.Core.Repositories;
using Finance.Infrastructure.Commands.BankAccounts;
using Finance.Infrastructure.Commands.User;
using Finance.Infrastructure.Database;
using Finance.Infrastructure.Repositories;
using Finance.Infrastructure.Settings;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Tests.EndToEnd.Controllers
{
    [TestFixture]
    public class BankAccountsControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        private readonly IUserRepository _userRepository;
        private readonly IBankAccountRepository _bankAccountRepository;



        public BankAccountsControllerTests()
        {
            _userRepository = new UserRepository(new FinanceContext(new SqlDatabaseSettings()));

            _server = new TestServer(WebHost
                .CreateDefaultBuilder()
                .UseStartup<Startup>());

            _client = _server.CreateClient();

        }

        [SetUp]
        public void Setup()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "abc");
        }

        [Test]
        public async Task CreateBankAccount_NotLoggedInUserTryingToCreateBankAccount_ShouldFail_ReturnsUnautorizedHttpErrorCode()
        {
            var user1 = await _userRepository.GetAsync("user1@test.com");

            var request = new CreateBankAccount
            {
                BankAccountName = "name",
                InitialBalance = 20,
                userId = user1.UserId
            };

            var payload = GetPayload(request);

            var response = await _client.PostAsync("BankAccounts", payload);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task CreateBankAccount_LoggedInUserTryingToAddNewBankAccount_ShouldSucced_ReturnsOkHttpStatusCode()
        {
            //Arrange
            var user1 = await _userRepository.GetAsync("user1@test.com");
            string token = await GetToken(user1);
            var request = new CreateBankAccount
            {
                BankAccountName = "name",
                InitialBalance = 25,
                userId = user1.UserId,
                Currency = "PLN",
                HexColor = "#FFF"
            };
            var payload = GetPayload(request);


            //Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync("BankAccounts", payload);


            //Assert
            var bankacc = user1.BankAccounts.FirstOrDefault(x => x.AccountName == request.BankAccountName);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            bankacc.Should().NotBeNull();
            bankacc.AccountName.Should().BeEquivalentTo(request.BankAccountName);
        }

        [Test]
        public async Task DeleteBankAccount_NotLoggedInUserTryingToRemoveBankAccount_ShouldFail_ReturnsUnautorizedHttpErrorCode()
        {
            var user1 = await _userRepository.GetAsync("user1@test.com");
            var bank = user1.BankAccounts.FirstOrDefault(x => x.AccountName == "BankAccount 1");

            var request = new DeleteBankAccount
            {
                BankAccountId = bank.BanAccountId
            };

            var payload = GetPayload(request);

            var response = await _client.PostAsync("BankAccounts", payload);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        }

        [Test]
        public async Task DeleteBankAccount_UserTryingToDeleteBankAccountThatBelongsToOtherUser_ShouldFail_ReturnsNotFoundHttpErrorCode()
        {
            var user1 = await _userRepository.GetAsync("user1@test.com");
            var user2 = await _userRepository.GetAsync("user2@test.com");
            var user2bank = user2.BankAccounts.FirstOrDefault(x => x.AccountName == "BankAccount 1");

            string token = await GetToken(user1);
            var request = new DeleteBankAccount
            {
                BankAccountId = user2bank.BanAccountId
            };
            var payload = GetPayload(request);

            var message = new HttpRequestMessage
            {
                Content = payload,
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_client.BaseAddress.ToString()}/BankAccounts")
            };

            //Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.SendAsync(message);


            //Assert
            var bankacc = user2.BankAccounts.FirstOrDefault(x => x.BanAccountId == request.BankAccountId);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            bankacc.Should().NotBeNull();
        }


        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<string> GetToken(User user)
        {
            var request = new Login
            {
                Email = user.Email,
                Password = "VerySecretPassword"
            };

            var payload = GetPayload(request);
            var response = await _client.PostAsync("login", payload);

            dynamic item = await GetObjectFromHttpResponseMessage(response);
            string token = item.value.token;

            return token;
        }

        private static async Task<dynamic> GetObjectFromHttpResponseMessage(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
        }
    }
}
