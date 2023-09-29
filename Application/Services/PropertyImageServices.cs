using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.References;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloCore.Entities;
using static Domain.Enums.EnumType;

namespace Application.Services
{
    //In this class all the processes associated with the image of property are managed.
    public class PropertyImageServices: IPropertyImageServices
    {
        private readonly IMapper mapper;
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly IFireBaseServices fireBaseServices;
        private readonly IMessageServices messageServices;
        private readonly IConfiguration config;

        //Controller
        public PropertyImageServices(IMapper mapper, IPropertyImageRepository propertyImageRepository, IPropertyRepository propertyRepository,
            IFireBaseServices fireBaseServices, IMessageServices messageServices, IConfiguration config)
        {
            this.mapper = mapper;
            this.propertyImageRepository = propertyImageRepository;
            this.propertyRepository = propertyRepository;
            this.fireBaseServices = fireBaseServices;
            this.messageServices = messageServices;
            this.config = config;
        }


        //Method to get a specific image of property
        public async Task<PropertyImage> Get(Guid? id)
        {
            var property = new PropertyImage();

            if (id.HasValue)
            {
                property = await propertyImageRepository.Get(id);
            }
            return property;
        }

        //Method to get a specific image of property
        public PropertyImage GetNotAsync(Guid? id)
        {
            var property = new PropertyImage();

            if (id.HasValue)
            {
                property = propertyImageRepository.GetNotAsync(id);
            }
            return property;
        }


        //Method to create new image of property
        public BaseResponse<PropertyImage> New(NewImageRequest newImageRequest)
        {
            BaseResponse<PropertyImage> response = new BaseResponse<PropertyImage>();

            response = ValidateProperty(newImageRequest.IdProperty);
            if (response.MessageCode > 0) return response;

            response = ValidateImage(newImageRequest.File.FileName);
            if (response.MessageCode > 0) return response;

            var urlImage = fireBaseServices.UpLoadImage(newImageRequest.File.OpenReadStream(), newImageRequest.File.FileName, config).Result;
            if (string.IsNullOrEmpty(urlImage)) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Image");

            var propertyImage = propertyImageRepository.Insert(new PropertyImage(urlImage, newImageRequest.IdProperty));

            if (propertyImage == null) return MessageResponse(MessageCode.TransactionNotProcessed, MessageType.Error);

            response = MessageResponse(MessageCode.Success, MessageType.Success, "Image");
            response.Data = propertyImage;

            return response;
        }


        //Method to obtain all the images of a property
        public async Task<List<PropertyImageBasicDto>> GetAllForProperty(Guid? idProperty)
        {
            var propertyImageEntities = new List<PropertyImageBasicDto>();
            if (idProperty.HasValue)
            {
                var propertyImages = await propertyImageRepository.GetAllForIdProperty(idProperty);
                if (propertyImages.Any()) propertyImageEntities = propertyImages.Select(x => mapper.Map<PropertyImageBasicDto>(x)).ToList();
            }
            return propertyImageEntities;
        }

        //Method to obtain one the image of a property
        public async Task<string> GetFirstForProperty(Guid? idProperty)
        {
            var imagenUrl = string.Empty;
            if (idProperty.HasValue)
            {
                var propertyImage = await propertyImageRepository.GetFirstForIdProperty(idProperty);
                if (propertyImage != null) imagenUrl = propertyImage.Url;
            }
            return imagenUrl;
        }

        //Method to update enable image of property
        public BaseResponse<PropertyImage> UpdatePropertyImageEnable(Guid? id, bool enable)
        {
            BaseResponse<PropertyImage> response = new BaseResponse<PropertyImage>();

            if (!id.HasValue) return MessageResponse(MessageCode.Required, MessageType.Error, "Image");
            var exitspropertyImage = propertyImageRepository.GetNotAsync(id);
            if (exitspropertyImage == null) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Image");

            var property = propertyImageRepository.UpdateEnable(id, enable);

            if (property == null) return MessageResponse(MessageCode.TransactionNotProcessed, MessageType.Error);

            response = MessageResponse(MessageCode.Success, MessageType.Success, "Image");
            response.Data = property;

            return response;
        }

        //Method to return response message
        private BaseResponse<PropertyImage> MessageResponse(EnumType.MessageCode code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            BaseResponse<PropertyImage> response = new BaseResponse<PropertyImage>();
            response.MessageCode = code;
            response.Message = String.Format("{0} {1}", messageServices.GetMessage(code, messageType), additionalMessage);
            response.MessageType = messageType;
            return response;
        }

        //Method to Validate property string
        private BaseResponse<PropertyImage> ValidateProperty(Guid? id)
        {
            if (!id.HasValue) return MessageResponse(MessageCode.Required, MessageType.Error, "Id");
            if (id == Guid.Empty) return MessageResponse(MessageCode.Required, MessageType.Error, "Id");
            var property = propertyRepository.Get(id);
            if (property == null) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Property");

            return new BaseResponse<PropertyImage>();
        }

        //Method to Validate image string
        private BaseResponse<PropertyImage> ValidateImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Image");
            if (!fireBaseServices.IsImage(fileName)) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Image");

            return new BaseResponse<PropertyImage>();
        }


    }

}
