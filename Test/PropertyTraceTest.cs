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
using static Domain.Enums.EnumType;

namespace Test
{
    public class PropertyTraceTest
    {
        private PropertyTraceServices propertyTraceServices;
        private Mock<IPropertyTraceRepository> propertyTraceRepositoryMock;
        private Mock<IPropertyRepository> propertyRepositoryMock;
        private Mock<IMessageServices> messageServicesMock;
        private Mock<IAccountRepository> accountRepositoryMock;
        private Mock<IMapper> mapperMock;

        [SetUp]
        public void Setup()
        {
            propertyTraceRepositoryMock = new Mock<IPropertyTraceRepository>();
            propertyRepositoryMock = new Mock<IPropertyRepository>();
            messageServicesMock = new Mock<IMessageServices>();
            accountRepositoryMock = new Mock<IAccountRepository>();
            mapperMock = new Mock<IMapper>();

            propertyTraceServices = new PropertyTraceServices(
                mapperMock.Object, messageServicesMock.Object, propertyTraceRepositoryMock.Object, propertyRepositoryMock.Object,
                 accountRepositoryMock.Object);
        }


        [Test]
        public async Task GetAllForProperty_WithNullId_ReturnsEmptyList()
        {
            // Arrange
            Guid? idProperty = null;

            // Act
            var result = await propertyTraceServices.GetAllForProperty(idProperty);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }



    }
}
