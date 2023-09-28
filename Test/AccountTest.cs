using Application.Contracts.Persistence;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Castle.Core.Configuration;
using Domain.Dtos;
using Domain.Entities;
using Domain.References;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.EnumType;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Test
{
    public class AccountTest
    {

        private AccountServices _accountServices;
        private Mock<IAccountRepository> _accountRepositoryMock;
        private Mock<IMessageServices> _messageServicesMock;
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IConfiguration> _configuration;


        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IConfiguration>();
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _messageServicesMock = new Mock<IMessageServices>();
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _mapperMock = new Mock<IMapper>();

            _accountServices = new AccountServices(
                _configuration.Object,
                _accountRepositoryMock.Object,
                _messageServicesMock.Object,
                _mapperMock.Object,
                _propertyRepositoryMock.Object
            );


        }

        [Test]
        public async Task GetAccountLogin_ValidLogin_ReturnsValidResponse()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "jadesignature@gmail.com",
                Password = "June123+"
            };

            var mockAccount = new Account
            {
                Id = Guid.NewGuid(),
                // Define other properties as needed
            };

            _accountRepositoryMock.Setup(repo => repo.GetForEmailAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(mockAccount);

            // Act
            var response = await _accountServices.GetAccountLogin(loginRequest);

            // Assert
            Assert.AreEqual(MessageType.Success, response.MessageType);
            Assert.AreEqual(MessageCode.Success, response.MessageCode);
            Assert.IsNotNull(response.Data);
            Assert.IsNotNull(response.Token);
        }

        [Test]
        public async Task GetAccountLogin_InvalidLogin_ReturnsErrorMessage()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "invalid@example.com",
                Password = "invalidpassword"
            };

            _accountRepositoryMock.Setup(repo => repo.GetForEmailAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((Account)null);

            // Act
            var response = await _accountServices.GetAccountLogin(loginRequest);

            // Assert
            Assert.AreEqual(MessageType.Error, response.MessageType);
            Assert.AreEqual(MessageCode.DoesNotexist, response.MessageCode);
            Assert.IsNull(response.Data);
            Assert.AreEqual(string.Empty,response.Token);
        }


        [Test]
        public async Task Get_WithValidId_ReturnsAccountInfoDto()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var accountEntity = new Account { Id = accountId };
            var propertyEntities = new List<Property> { new Property { OwnerId = accountId } };
            var accountInfoDto = new AccountInfoDto();

            _accountRepositoryMock.Setup(repo => repo.Get(accountId)).ReturnsAsync(accountEntity);
            _propertyRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(propertyEntities);
            _mapperMock.Setup(mapper => mapper.Map<AccountInfoDto>(accountEntity)).Returns(accountInfoDto);
            _mapperMock.Setup(mapper => mapper.Map<AccountPropertyInfoDto>(It.IsAny<Property>())).Returns(new AccountPropertyInfoDto());

            // Act
            var result = await _accountServices.Get(accountId);

            // Assert
            Assert.AreEqual(accountInfoDto, result);
        }

        [Test]
        public async Task Get_WithInvalidId_ReturnsEmptyAccountInfoDto()
        {
            // Arrange
            var invalidAccountId = Guid.NewGuid();
            _accountRepositoryMock.Setup(repo => repo.Get(invalidAccountId)).ReturnsAsync((Account)null);

            // Act
            var result = await _accountServices.Get(invalidAccountId);

            // Assert
            Assert.IsNull(result);
        }



        [Test]
        public async Task Insert_WithValidAccountDto_ReturnsSuccessResponse()
        {
            // Arrange
            var accountDto = new AccountDto { /* Configurar el objeto según tus necesidades */ };
            var accountEntity = new Account { /* Configurar el objeto según tus necesidades */ };

            _accountRepositoryMock.Setup(repo => repo.GetNotAsync(accountDto.Id)).Returns((Account)null);
            _mapperMock.Setup(mapper => mapper.Map<Account>(accountDto)).Returns(accountEntity);
            _accountRepositoryMock.Setup(repo => repo.Insert(accountEntity)).ReturnsAsync(accountEntity);
            _messageServicesMock.Setup(service => service.GetMessage(MessageCode.Success, MessageType.Success)).Returns("Success message");

            // Act
            var result = await _accountServices.Insert(accountDto);

            // Assert
            Assert.AreEqual(MessageCode.Success, result.MessageCode);
            Assert.AreEqual(MessageType.Success, result.MessageType);
            Assert.AreEqual(accountEntity, result.Data);
        }

        [Test]
        public async Task Insert_WithNullAccountDto_ReturnsErrorResponse()
        {
            // Arrange
            AccountDto nullAccountDto = null;

            // Act
            var result = await _accountServices.Insert(nullAccountDto);

            // Assert
            Assert.AreEqual(MessageCode.Required, result.MessageCode);
            Assert.AreEqual(MessageType.Error, result.MessageType);
            Assert.IsNull(result.Data);
        }




    }
}
