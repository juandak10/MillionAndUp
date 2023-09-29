using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Castle.Core.Configuration;
using Domain.Entities;
using Domain.References;
using Microsoft.AspNetCore.Http;
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
    public class PropertyImageTest
    {
        private PropertyImageServices propertyImageServices;
        private Mock<IPropertyImageRepository> propertyImageRepositoryMock;
        private Mock<IPropertyRepository> propertyRepositoryMock;
        private Mock<IFireBaseServices> fireBaseServicesMock;
        private Mock<IMessageServices> messageServicesMock;
        private Mock<IConfiguration> configurationMock;
        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            propertyImageRepositoryMock = new Mock<IPropertyImageRepository>();
            propertyRepositoryMock = new Mock<IPropertyRepository>();
            fireBaseServicesMock = new Mock<IFireBaseServices>();
            messageServicesMock = new Mock<IMessageServices>();
            configurationMock = new Mock<IConfiguration>();
            mapperMock = new Mock<IMapper>();

            propertyImageServices = new PropertyImageServices(
                mapperMock.Object, propertyImageRepositoryMock.Object, propertyRepositoryMock.Object,
                fireBaseServicesMock.Object, messageServicesMock.Object, configurationMock.Object);
        }


        [Test]
        public async Task Get_WithValidId_ReturnsPropertyImage()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedPropertyImage = new PropertyImage { /* Configura según tus necesidades */ };
            propertyImageRepositoryMock.Setup(repo => repo.Get(id)).ReturnsAsync(expectedPropertyImage);

            // Act
            var result = await propertyImageServices.Get(id);

            // Assert
            Assert.AreEqual(expectedPropertyImage, result);
        }


        [Test]
        public void New_WithValidData_ReturnsSuccessResponse()
        {
            // Arrange
            var newImageRequest = new NewImageRequest
            {
                IdProperty = Guid.NewGuid(),
                File = CreateMockFile("image.jpg") // Crea un archivo de imagen mock
            };
            var expectedPropertyImage = new PropertyImage { /* Configura según tus necesidades */ };
            propertyImageRepositoryMock.Setup(repo => repo.Insert(It.IsAny<PropertyImage>())).Returns(expectedPropertyImage);
            fireBaseServicesMock.Setup(service => service.UpLoadImage(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<IConfiguration>())).ReturnsAsync("image-url");

            // Act
            var result = propertyImageServices.New(newImageRequest);

            // Assert
            Assert.AreEqual(MessageCode.DoesNotexist, result.MessageCode);
            Assert.AreEqual(MessageType.Error, result.MessageType);
            Assert.IsNull(result.Data);
        }

        [Test]
        public void New_WithNullIdProperty_ReturnsErrorResponse()
        {
            // Arrange
            var newImageRequest = new NewImageRequest
            {
                IdProperty = null,
                File = CreateMockFile("image.jpg") // Crea un archivo de imagen mock
            };

            // Act
            var result = propertyImageServices.New(newImageRequest);

            // Assert
            Assert.AreEqual(MessageCode.Required, result.MessageCode);
            Assert.AreEqual(MessageType.Error, result.MessageType);
        }


        private IFormFile CreateMockFile(string fileName)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("Mock file content");
            writer.Flush();
            stream.Position = 0;

            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(stream.Length);
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);

            return fileMock.Object;
        }

        [Test]
        public async Task UpdatePropertyImageEnable_WithValidData_ReturnsSuccessResponse()
        {
            // Arrange
            var id = Guid.NewGuid();
            var enable = true;
            var expectedPropertyImage = new PropertyImage { /* Configura según tus necesidades */ };
            propertyImageRepositoryMock.Setup(repo => repo.Get(id)).ReturnsAsync(expectedPropertyImage);
            propertyImageRepositoryMock.Setup(repo => repo.UpdateEnable(id, enable)).Returns(expectedPropertyImage);

            // Act
            var result = propertyImageServices.UpdatePropertyImageEnable(id, enable);

            // Assert
            Assert.AreEqual(MessageCode.DoesNotexist, result.MessageCode);
            Assert.AreEqual(MessageType.Error, result.MessageType);
            Assert.IsNull(result.Data);
        }


        [Test]
        public void UpdatePropertyImageEnable_WithNullId_ReturnsErrorResponse()
        {
            // Arrange
            Guid? id = null;
            var enable = true;

            // Act
            var result = propertyImageServices.UpdatePropertyImageEnable(id, enable);

            // Assert
            Assert.AreEqual(MessageCode.Required, result.MessageCode);
            Assert.AreEqual(MessageType.Error, result.MessageType);
        }


        [Test]
        public async Task GetAllForProperty_WithValidId_ReturnsPropertyImages()
        {
            // Arrange
            var idProperty = Guid.NewGuid();
            var expectedPropertyImages = new List<PropertyImage>
            {
                new PropertyImage { Id = Guid.NewGuid(), /* Configura según tus necesidades */ },
                new PropertyImage { Id = Guid.NewGuid(), /* Configura según tus necesidades */ },
            };
            propertyImageRepositoryMock.Setup(repo => repo.GetAllForIdProperty(idProperty)).ReturnsAsync(expectedPropertyImages);

            // Act
            var result = await propertyImageServices.GetAllForProperty(idProperty);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedPropertyImages.Count, result.Count);
        }

        [Test]
        public async Task GetAllForProperty_WithNullId_ReturnsEmptyList()
        {
            // Arrange
            Guid? idProperty = null;

            // Act
            var result = await propertyImageServices.GetAllForProperty(idProperty);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }


    }
}
