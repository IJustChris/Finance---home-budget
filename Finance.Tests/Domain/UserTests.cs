using Finance.Core.Domain;
using Finance.Core.Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace Finance.Tests
{
    [TestFixture]
    public class UserTests
    {
        public User user;
        public int bank1, bank2, bank3;
        public int category1, category2, category3;

        public int validint;

        public UserTests()
        {

        }

        [SetUp]
        public void Setup()
        {
            user = User.Create(1, "validemail@test.pl", "User1", (int)UserRole.user, "secretpassword123", "salt");
        }

        [Test]
        [TestCase("bankName", 100, "PLN", "#ffffff")]
        public void CreateBankAccount_ValidDataOnInput_ShouldCreateBankAccount(string bankAccName, decimal initialValue, string currency, string hexColor)
        {
            user.CreateBankAccount(1, bankAccName, initialValue, currency, hexColor);
        }

        [Test]
        public void Create_GivenValidInput_ShouldCreateUser()
        {
            var newUser = User.Create(2, "validemail@test.com", "ValidUsername", (int)UserRole.user, "Secretpassword", "salt");

            newUser.Email.Should().Be("validemail@test.com");
            newUser.Should().NotBeNull();
        }

        [Test]
        public void Create_EmptyintOnInput_ShoudThrowException()
        {
            User newUser = null;
            DomainException domainException = null;
            try
            {
                newUser = User.Create(new int(), "validemail@test.com", "ValidUsername", (int)UserRole.user, "Secretpassword", "salt");
            }
            catch (Exception exception)
            {
                if (exception is DomainException ex)
                {
                    domainException = ex;
                }
            }

            newUser.Should().BeNull();
            domainException.Should().NotBeNull();
            domainException.Code.Should().Be(DomainErrorCodes.EmptyId);
        }
        [Test]
        public void CreateBankAccount_GivenValidData_ShouldSucced_ReturnsBankAccountObject_BankAccountObjectIsAddedToBankList(int id, string banAccountName, decimal initialBalance)
        {

        }

        [Test]
        public void CreateBankAccount_EmptyIdOnInput_ShouldFail_ThrowsExceptionWithErrorCodeEmptyString()
        {
            DomainException domainException = null;
            try
            {
                user.CreateBankAccount(new int(), "name", 00.00m, "PLN", "#ffffff");
            }
            catch (Exception ex)
            {
                if (ex is DomainException exception)
                {
                    exception.Code.Should().Be(DomainErrorCodes.EmptyId);
                    domainException = exception;
                }
            }

            Assert.IsFalse(user.BankAccounts.Any());
            Assert.IsNotNull(domainException);
        }

        [Test]
        public void CreateBankAccount_EmptyStringOnInput_ShouldFail_ThrowsExceptionWithErrorCodeEmptyString()
        {
            DomainException domainException = null;
            try
            {
                user.CreateBankAccount(3, string.Empty, 00.00m, "PLN", "#fff");
            }
            catch (Exception ex)
            {
                if (ex is DomainException exception)
                {
                    exception.Code.Should().Be(DomainErrorCodes.EmptyString);
                    domainException = exception;
                }
            }

            Assert.IsFalse(user.BankAccounts.Any());
            Assert.IsNotNull(domainException);
        }

        [Test]
        public void setPassword_GivenValidPassword_ShouldSuccedAndChangeUserPassword()
        {
            var newPassword = "newpassword123";
            var updatedAt = user.UpdatedAt;

            user.SetPassword(newPassword);

            user.Password.Should().Be(newPassword);
            user.UpdatedAt.Should().NotBe(updatedAt);
        }

        [Test]
        public void setPassword_GivenTooShort_ReturnInvalidPasswordErrorCode()
        {
            var newPassword = "123";
            var updatedAt = user.UpdatedAt;
            DomainException exception = null;
            try
            {
                user.SetPassword(newPassword);
            }
            catch (Exception ex)
            {
                if (ex is DomainException exc)
                {
                    exception = exc;
                    exception.Code.Should().Be(DomainErrorCodes.InvalidPassword);
                }
            }

            Assert.IsNotNull(exception);
        }


        [Test]
        public void SetUsername_ValidUsernameOnInput_ShouldChangeUsername()
        {
            var username = "changedUser";
            var updatedAt = user.UpdatedAt;

            user.SetUsername(username);

            user.Username.Should().Be(username);
            user.UpdatedAt.Should().NotBe(updatedAt);

        }

        [Test]
        public void SetUsername_EmptyStringOnInput_ShouldThrowInvalidUsernameErrorCode()
        {
            DomainException domainException = null;
            try
            {
                user.SetUsername(null);
            }
            catch (Exception ex)
            {
                if (ex is DomainException exception)
                {
                    exception.Code.Should().Be(DomainErrorCodes.EmptyString);
                    domainException = exception;
                }
            }

            Assert.IsNotNull(domainException);

        }


        [TestCase("user1!")]
        [TestCase("user1@")]
        [TestCase("user1#")]
        [TestCase("user1$")]
        [TestCase("user1%")]
        [TestCase("user1^")]
        [TestCase("user1!&")]
        [TestCase("user1!*")]
        [TestCase("user1!(")]
        [TestCase("user1!)")]
        [TestCase("user1!=")]
        [TestCase("user1!+")]
        [TestCase("user1![")]
        [TestCase("user1!]")]
        [TestCase("user1!'")]
        [TestCase("user1!")]
        [TestCase("user1!;")]
        [TestCase("user1!:")]
        [TestCase("user1!,")]
        [TestCase("user1!.")]
        [TestCase("user1!//")]
        [TestCase("user1!?")]
        [TestCase("user1!\\")]
        [TestCase("user1!|")]
        public void SetUsername_StringWithForbiddenCharactersOnInput_ShouldThrowInvalidUsernameErrorCode(string value)
        {
            var updatedAt = user.UpdatedAt;

            DomainException domainException = null;
            try
            {
                user.SetUsername(value);
            }
            catch (Exception e)
            {
                if (e is DomainException ex)
                {
                    ex.Code.Should().Be(DomainErrorCodes.InvalidUsername);
                    domainException = ex;
                }
            }

            Assert.IsNotNull(domainException);

        }



        [TestCase("email@example.com")]
        [TestCase("firstname.lastname@example.com")]
        [TestCase("email@subdomain.example.com")]
        [TestCase("firstname+lastname@example.com")]
        [TestCase("email@123.123.123.123")]
        [TestCase("email@[123.123.123.123]")]
        [TestCase("\"email\"@example.com")]
        [TestCase("1234567890@example.com")]
        [TestCase("email@example-one.com")]
        [TestCase("_______@example.com")]
        [TestCase("email@example.name")]
        [TestCase("email@example.museum")]
        [TestCase("email@example.co.jp")]
        [TestCase("firstname-lastname@example.com")]
        public void SetEmail_GivenValidEmail_ShouldChangeEmail(string value)
        {
            user.SetEmail(value);

            user.Email.Should().Be(value);
        }


        [TestCase("plainaddress")]
        [TestCase("#@%^%#$@#$@#.com")]
        [TestCase("@example.com")]
        [TestCase("Joe Smith <email@example.com>")]
        [TestCase("email.example.com")]
        [TestCase("email@example@example.com")]
        [TestCase(".email@example.com")]
        [TestCase("email.@example.com")]
        [TestCase("email..email@example.com")]
        [TestCase("あいうえお@example.com")]
        [TestCase("email@example.com (Joe Smith)")]
        [TestCase("email@example")]
        [TestCase("email@-example.com")]
        [TestCase("email@example.web")]
        [TestCase("email@111.222.333.44444")]
        [TestCase("email@example..com")]
        [TestCase("Abc..123@example.com")]
        [TestCase("”(),:;<>[\\]@example.com")]
        [TestCase("just”not”right@example.com")]
        [TestCase("this\\ is\"really\"not\\allowed@example.com")]
        public void SetEmail_GivenInvalidEmail_ShouldThrowInvalidEmailErrorCode(string value)
        {
            DomainException domainException = null;

            try
            {
                user.SetEmail(value);
            }
            catch (Exception e)
            {
                if (e is DomainException ex)
                {
                    ex.Code.Should().Be(DomainErrorCodes.InvalidEmail);
                    domainException = ex;
                }
            }

            Assert.IsNotNull(domainException);
        }
    }
}
