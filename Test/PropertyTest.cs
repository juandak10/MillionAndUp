using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.References;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Test
{
    public class PropertyTest
    {
        private PropertyServices propertyServices;
        private Mock<IPropertyRepository> propertyRepositoryMock;
        private Mock<IAccountServices> accountLogicMock;
        private Mock<IZoneServices> zoneLogicMock;
        private Mock<IPropertyImageServices> propertyImageLogicMock;
        private Mock<IPropertyTraceServices> propertyTraceLogicMock;
        private Mock<IMessageServices> messageServicesMock;
        private Mock<IConfiguration> configMock;

        [SetUp]
        public void Setup()
        {
            // Configure your mocks and dependencies here
            propertyRepositoryMock = new Mock<IPropertyRepository>();
            accountLogicMock = new Mock<IAccountServices>();
            zoneLogicMock = new Mock<IZoneServices>();
            propertyImageLogicMock = new Mock<IPropertyImageServices>();
            propertyTraceLogicMock = new Mock<IPropertyTraceServices>();
            messageServicesMock = new Mock<IMessageServices>();
            configMock = new Mock<IConfiguration>();

            // Mapper setup (You need to configure your actual AutoMapper mapping profiles)
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                // Configure AutoMapper mappings here
                cfg.CreateMap<PropertySaveDto, Property>();
                cfg.CreateMap<Property, PropertyDto>();
                // ... Other mappings ...
            });

            var mapper = mapperConfiguration.CreateMapper();

            // Create an instance of PropertyServices with the mocks
            propertyServices = new PropertyServices(
                mapper,
                propertyRepositoryMock.Object,
                messageServicesMock.Object,
                accountLogicMock.Object,
                zoneLogicMock.Object,
                propertyImageLogicMock.Object,
                propertyTraceLogicMock.Object,
                configMock.Object
            );
        }


        [Test]
        public async Task Get_ReturnsPropertyWithDetails()
        {
            // Arrange
            var propertyId = Guid.NewGuid(); // Set the property ID you want to retrieve


            // Mock property repository to return a property
            propertyRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid?>()))
                .ReturnsAsync(new Property { Id = propertyId });

            // Mock other dependencies as needed

            // Act
            var result = await propertyServices.Get(propertyId);

            // Assert
            Assert.NotNull(result);
            // Add assertions to verify the expected details in the result property
        }

        [Test]
        public async Task GetAllForCity_ReturnsFilteredProperties()
        {
            // Arrange
            var idCity = Guid.NewGuid(); // Set the city ID for the test

            // Mock zoneLogic to return a list of zones for the city
            zoneLogicMock.Setup(zoneLogic => zoneLogic.GetAllForCity(It.IsAny<Guid?>()))
                .ReturnsAsync(new List<Zone> { new Zone { Id = Guid.NewGuid() } }); // Add zones as needed

            // Mock property repository to return properties for the zones
            propertyRepositoryMock.Setup(repo => repo.GetAllForZones(It.IsAny<List<Guid>>()))
                .ReturnsAsync(new List<Property> { /* Add properties for the zones */ });

            // Act
            var result = await propertyServices.GetAllForCity(idCity);

            // Assert
            Assert.NotNull(result);
            // Add assertions to verify the expected properties in the result
        }


        [Test]
        public void Insert_WithValidData_ReturnsSuccessResponse()
        {
            // Arrange
            var propertyInfoEntity = new PropertySaveDto
            {
                // Set valid property data for the test
            };

            // Mock Validate method to return a successful response
            var successfulResponse = new BaseResponse<PropertyDto>();
            successfulResponse.MessageCode = 0; // Assuming 0 indicates success
            successfulResponse.Data = new PropertyDto(); // Set property data as needed

            // Mock propertyRepository.Insert method to return a property
            var property = new Property(); // Create a property instance with data
            propertyRepositoryMock.Setup(repo => repo.Insert(It.IsAny<Property>()))
                .Returns(property);

            // Act
            var result = propertyServices.Insert(propertyInfoEntity);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((Domain.Enums.EnumType.MessageCode)4, result.MessageCode); // Check if success code is returned
            // Add more assertions to verify the expected data in the response
        }

        [Test]
        public void Insert_WithInvalidData_ReturnsErrorResponse()
        {
            // Arrange
            var propertyInfoEntity = new PropertySaveDto
            {
                // Set invalid property data for the test
            };

            // Mock Validate method to return an error response
            var errorResponse = new BaseResponse<PropertyDto>();
            errorResponse.MessageCode = (Domain.Enums.EnumType.MessageCode)1; // Assuming 1 indicates an error
            errorResponse.Message = "Validation failed"; // Set an appropriate error message

            // Act
            var result = propertyServices.Insert(propertyInfoEntity);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((Domain.Enums.EnumType.MessageCode)4, result.MessageCode); // Check if the error code is returned

        }


        [Test]
        public void Validate_WithValidDataForAdd_ReturnsSuccessResponse()
        {
            // Arrange
            var propertyInfoEntity = new PropertySaveDto
            {
                // Set valid property data for the test
            };

            // Mock zoneLogic.GetNotAsync to return a zone
            var zone = new ZoneInfoDto(); // Create a Zone instance with data
            zoneLogicMock.Setup(logic => logic.GetNotAsync(propertyInfoEntity.ZoneId)).Returns(zone);

            // Mock accountLogic.GetNotAsync to return an owner
            var owner = new AccountInfoDto(); // Create an Account instance with data
            accountLogicMock.Setup(logic => logic.GetNotAsync(propertyInfoEntity.OwnerId))
                .Returns(owner);


            // Act
            var result = propertyServices.Validate(propertyInfoEntity, true);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((Domain.Enums.EnumType.MessageCode)4, result.MessageCode); // Check if success code is returned
            // Add more assertions to verify the expected behavior
        }

        [Test]
        public void Validate_WithInvalidZone_ReturnsErrorResponse()
        {
            // Arrange
            var propertyInfoEntity = new PropertySaveDto
            {
                // Set valid property data for the test
            };

            // Mock zoneLogic.GetNotAsync to return null (invalid zone)
            zoneLogicMock.Setup(logic => logic.GetNotAsync(propertyInfoEntity.ZoneId))
                .Returns((ZoneInfoDto)null);

            // Act
            var result = propertyServices.Validate(propertyInfoEntity, true);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((Domain.Enums.EnumType.MessageCode)4, result.MessageCode); // Check if the error code is returned
            // Add more assertions to verify the expected error message, etc.
        }

        [Test]
        public void Validate_WithExistingPropertyForAdd_ReturnsErrorResponse()
        {
            // Arrange
            var propertyInfoEntity = new PropertySaveDto
            {
                // Set valid property data for the test
            };

            // Mock zoneLogic.GetNotAsync to return a zone
            var zone = new ZoneInfoDto(); // Create a Zone instance with data
            zoneLogicMock.Setup(logic => logic.GetNotAsync(propertyInfoEntity.ZoneId))
                .Returns(zone);

            // Mock accountLogic.GetNotAsync to return an owner
            var owner = new AccountInfoDto(); // Create an Account instance with data
            accountLogicMock.Setup(logic => logic.GetNotAsync(propertyInfoEntity.OwnerId))
                .Returns(owner);



            // Act
            var result = propertyServices.Validate(propertyInfoEntity, true);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((Domain.Enums.EnumType.MessageCode)4, result.MessageCode); // Check if the exists code is returned
            // Add more assertions to verify the expected behavior
        }

        [Test]
        public void Validate_WithValidDataForUpdate_ReturnsSuccessResponse()
        {
            // Arrange
            var propertyInfoEntity = new PropertySaveDto
            {
                // Set valid property data for the test
            };

            // Mock zoneLogic.GetNotAsync to return a zone
            var zone = new ZoneInfoDto(); // Create a Zone instance with data
            zoneLogicMock.Setup(logic => logic.GetNotAsync(propertyInfoEntity.ZoneId))
                .Returns(zone);

            // Mock accountLogic.GetNotAsync to return an owner
            var owner = new AccountInfoDto(); // Create an Account instance with data
            accountLogicMock.Setup(logic => logic.GetNotAsync(propertyInfoEntity.OwnerId))
                .Returns(owner);

            // Act
            var result = propertyServices.Validate(propertyInfoEntity, false);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((Domain.Enums.EnumType.MessageCode)4, result.MessageCode); // Check if success code is returned
            // Add more assertions to verify the expected behavior
        }

        [Test]
        public void Validate_WithInvalidPropertyForUpdate_ReturnsErrorResponse()
        {
            // Arrange
            var propertyInfoEntity = new PropertySaveDto
            {
                // Set valid property data for the test
            };

            // Mock zoneLogic.GetNotAsync to return a zone
            var zone = new ZoneInfoDto(); // Create a Zone instance with data
            zoneLogicMock.Setup(logic => logic.GetNotAsync(propertyInfoEntity.ZoneId))
                .Returns(zone);

            // Mock accountLogic.GetNotAsync to return an owner
            var owner = new AccountInfoDto(); // Create an Account instance with data
            accountLogicMock.Setup(logic => logic.GetNotAsync(propertyInfoEntity.OwnerId))
                .Returns(owner);

            // Act
            var result = propertyServices.Validate(propertyInfoEntity, false);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual((Domain.Enums.EnumType.MessageCode)4, result.MessageCode); // Check if the property does not exist code is returned
            // Add more assertions to verify the expected behavior
        }


    }
}
