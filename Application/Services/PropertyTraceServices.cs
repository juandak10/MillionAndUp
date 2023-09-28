using Application.Contracts.Persistence;
using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using WeeloCore.Entities;
using static Domain.Enums.EnumType;

namespace Application.Services
{
    //In this class all the processes associated with the trace of property are managed.
    public class PropertyTraceServices : IPropertyTraceServices
    {
        private readonly IMapper mapper;
        private readonly IPropertyTraceRepository propertyTraceRepository;
        private readonly IPropertyRepository propertyRepository;
        private readonly IMessageServices messageServices;
        private readonly IAccountRepository accountRepository;

        //Controller
        public PropertyTraceServices(IMapper mapper, IMessageServices messageServices, IPropertyTraceRepository propertyTraceRepository, 
            IPropertyRepository propertyRepository, IAccountRepository accountRepository)
        {
            this.mapper = mapper;
            this.accountRepository = accountRepository;
            this.propertyTraceRepository = propertyTraceRepository;
            this.propertyRepository = propertyRepository;
            this.messageServices = messageServices;
        }


        //Method to add a new  trace of property
        public BaseResponse<PropertyTraceDto> Insert(PropertyTraceRequest propertyTraceEntity)
        {
            BaseResponse<PropertyTraceDto> response = new BaseResponse<PropertyTraceDto>();
            
            var propertyTraceDto = mapper.Map<PropertyTraceDto>(propertyTraceEntity);

            response = Validate(propertyTraceDto);
            if (response.MessageCode > 0) return response;

            var property = propertyTraceRepository.Insert(mapper.Map<PropertyTrace>(propertyTraceDto));

            if (property == null) return MessageResponse(MessageCode.TransactionNotProcessed, MessageType.Error);

            response = MessageResponse(MessageCode.Success, MessageType.Success, "Trace");
            response.Data = mapper.Map<PropertyTraceDto>(property);

            return response;

        }


        //Method to obtain all the trace of a property
        public async Task<List<PropertyTraceDto>> GetAllForProperty(Guid? idProperty)
        {
            var propertyTraceEntities = new List<PropertyTraceDto>();
            if (idProperty.HasValue)
            {
                var propertyTraces = await propertyTraceRepository.GetAllForIdProperty(idProperty);

                if (propertyTraces.Any())
                {
                    propertyTraceEntities = propertyTraces.Select(x => mapper.Map<PropertyTraceDto>(x)).ToList();
                    if (propertyTraceEntities.Any())
                    {
                        foreach(var property in propertyTraceEntities) {
                            var accountOld = await accountRepository.Get(property.OwnerOldId);
                            if (accountOld != null) property.NameOwnerOld = accountOld.Name;
                            var accountNew = await accountRepository.Get(property.OwnerNewId);
                            if (accountNew != null) property.NameOwnerNew = accountNew.Name;
                        };

                    }
                }
            }
            return propertyTraceEntities;
        }

        //Method to return response message
        private BaseResponse<PropertyTraceDto> MessageResponse(EnumType.MessageCode code, EnumType.MessageType messageType, string additionalMessage = "")
        {
            BaseResponse<PropertyTraceDto> response = new BaseResponse<PropertyTraceDto>();
            response.MessageCode = code;
            response.Message = String.Format("{0} {1}", messageServices.GetMessage(code, messageType), additionalMessage);
            response.MessageType = messageType;
            return response;
        }

        //Method to Validate trace of property
        private BaseResponse<PropertyTraceDto> Validate(PropertyTraceDto propertyTraceEntity)
        {
            if (!propertyTraceEntity.OwnerNewId.HasValue) return MessageResponse(MessageCode.Required, MessageType.Error, "OwnerNew");
            if (!propertyTraceEntity.OwnerOldId.HasValue) return MessageResponse(MessageCode.Required, MessageType.Error, "OwnerOld");
            if (!propertyTraceEntity.PropertyId.HasValue) return MessageResponse(MessageCode.Required, MessageType.Error, "Property");

            return new BaseResponse<PropertyTraceDto>();
        }

    }
}
