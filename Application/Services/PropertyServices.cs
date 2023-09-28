using Application.Contracts.Persistence;
using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.References;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.EnumType;

namespace Application.Services
{
    //In this class, all the processes associated with the buildings or homes are managed.
    public class PropertyServices : IPropertyServices
    {
        private readonly IMapper mapper;
        private readonly IPropertyRepository propertyRepository;
        private readonly IAccountServices accountLogic;
        private readonly IZoneServices zoneLogic;
        private readonly IPropertyImageServices propertyImageLogic;
        private readonly IPropertyTraceServices propertyTraceLogic;
        private readonly IMessageServices messageServices;
        private readonly IConfiguration config;

        //Controller
        public PropertyServices(IMapper mapper, IPropertyRepository propertyRepository, IMessageServices messageServices, IAccountServices accountLogic,
            IZoneServices zoneLogic, IPropertyImageServices propertyImageLogic, IPropertyTraceServices propertyTraceLogic, IConfiguration config)
        {
            this.mapper = mapper;
            this.accountLogic = accountLogic;
            this.zoneLogic = zoneLogic;
            this.propertyImageLogic = propertyImageLogic;
            this.propertyTraceLogic = propertyTraceLogic;
            this.propertyRepository = propertyRepository;
            this.messageServices = messageServices;
            this.config = config;

        }

        //Method to search for properties by city, area, price, year, room number among other filters
        public async Task<List<PropertyBasicDto>> Find(FindPropertyDto find)
        {
            var properties = new List<PropertyBasicDto>();

            if (find.IdCity.HasValue)
            {
                properties = await GetAllForCity(find.IdCity);
                properties = await Arrive(properties);
                properties = Filter(properties, find);
            }

            var itemsForPage = Convert.ToInt32(config.GetSection("Page")["ItemsForPage"]);

            return properties.Skip(itemsForPage * find.Page).Take(itemsForPage).ToList();
        }

        //Method to get a specific property,with detailed information
        public async Task<PropertyDto> Get(Guid? id)
        {
            var property = new PropertyDto();

            if (id.HasValue)
            {
                property = mapper.Map<PropertyDto>(await propertyRepository.Get(id));

                if (property != null)
                {
                    if (property.OwnerId.HasValue) property.Owner = await accountLogic.GetBasic(property.OwnerId);
                    if (property.ZoneId.HasValue) property.Zone = await zoneLogic.GetInfo(property.ZoneId);

                    property.PropertyImages = await propertyImageLogic.GetAllForProperty(property.Id);
                    property.PropertyTraces = await propertyTraceLogic.GetAllForProperty(property.Id);

                    property = Arrive(property);
                }
            }

            return property;
        }


        //Method to get a specific property,with detailed information
        public PropertyDto GetNotAsync(Guid? id)
        {
            var property = new PropertyDto();

            if (id.HasValue)
            {
                property = mapper.Map<PropertyDto>( propertyRepository.GetNotAsync(id));
            }

            return property;
        }

        //Search properties by a city
        public async Task<List<PropertyBasicDto>> GetAllForCity(Guid? idCity)
        {
            var propertyEntities = new List<PropertyBasicDto>();

            if (idCity.HasValue)
            {
                var zones = await zoneLogic.GetAllForCity(idCity);
                if (zones.Any())
                {
                    var idZones = zones.Select(x => x.Id).ToList();
                    var properties = await propertyRepository.GetAllForZones(idZones);
                    if (properties.Any()) propertyEntities = properties.Select(x => mapper.Map<PropertyBasicDto>(x)).ToList();
                }

            }

            return propertyEntities;
        }

        //Method to get all system properties
        public async Task<List<PropertyDto>> GetAll()
        {
            var propertyEntities = new List<PropertyDto>();

            var properties = await propertyRepository.GetAll();
            if (properties.Any())
            {
                propertyEntities = properties.Select(x => mapper.Map<PropertyDto>(x)).ToList();
                if (propertyEntities.Any()) propertyEntities.ForEach(x => Arrive(x));
            }

            return propertyEntities;
        }

        //Method to get all system properties
        public List<PropertyDto> GetAllNotAsync()
        {
            var propertyEntities = new List<PropertyDto>();

            var properties = propertyRepository.GetAllNotAsync();
            if (properties.Any())
            {
                propertyEntities = properties.Select(x => mapper.Map<PropertyDto>(x)).ToList();
                if (propertyEntities.Any()) propertyEntities.ForEach(x => Arrive(x));
            }

            return propertyEntities;
        }

        //Method to add a new property
        public BaseResponse<PropertyDto> Insert(PropertySaveDto propertyInfoEntity)
        {
            BaseResponse<PropertyDto> response = new BaseResponse<PropertyDto>();

            response = Validate(propertyInfoEntity, true);
            if (response.MessageCode > 0) return response;

            var property = propertyRepository.Insert(mapper.Map<Property>(propertyInfoEntity));

            if (property == null) return MessageResponse(MessageCode.TransactionNotProcessed, MessageType.Error);

            response = MessageResponse(MessageCode.Success, MessageType.Success, "Property");
            response.Data = mapper.Map<PropertyDto>(property);

            return response;
        }

        //Method to delete a property
        public BaseResponse<PropertyDto> Delete(Guid? id)
        {
            BaseResponse<PropertyDto> response = new BaseResponse<PropertyDto>();

            if (!id.HasValue) return MessageResponse(MessageCode.Required, MessageType.Error, "Property");
            var exitsproperty = GetNotAsync(id);
            if (exitsproperty == null) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Property");

            var property = propertyRepository.Delete(id);

            if (property == null) return MessageResponse(MessageCode.TransactionNotProcessed, MessageType.Error);

            response = MessageResponse(MessageCode.Success, MessageType.Success, "Property");
            response.Data = mapper.Map<PropertyDto>(property);

            return response;
        }

        //Method to update a property
        public BaseResponse<PropertyDto> Update(PropertySaveDto propertyInfoEntity)
        {
            BaseResponse<PropertyDto> response = new BaseResponse<PropertyDto>();

            response = Validate(propertyInfoEntity, false);
            if (response.MessageCode > 0) return response;

            var property = propertyRepository.Update(mapper.Map<Property>(propertyInfoEntity));

            if (property == null) return MessageResponse(MessageCode.TransactionNotProcessed, MessageType.Error);

            response = MessageResponse(MessageCode.Success, MessageType.Success, "Property");
            response.Data = mapper.Map<PropertyDto>(property);

            return response;
        }

        //Method to update price a property
        public BaseResponse<PropertyDto> UpdatePropertyPrice(Guid? id, decimal price)
        {
            BaseResponse<PropertyDto> response = new BaseResponse<PropertyDto>();

            if (!id.HasValue) return MessageResponse(MessageCode.Required, MessageType.Error, "Property");
            var exitsproperty = GetNotAsync(id);
            if (exitsproperty == null) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Property");

            if (price < 0) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Price");

            var property = propertyRepository.UpdatePrice(id, price);

            if (property == null) return MessageResponse(MessageCode.TransactionNotProcessed, MessageType.Error);

            response = MessageResponse(MessageCode.Success, MessageType.Success, "Property");
            response.Data = mapper.Map<PropertyDto>(property);

            return response;
        }

        //Method to update enable a property
        public BaseResponse<PropertyDto> UpdatePropertyEnable(Guid? id, bool enable)
        {
            BaseResponse<PropertyDto> response = new BaseResponse<PropertyDto>();

            if (!id.HasValue) return MessageResponse(MessageCode.Required, MessageType.Error, "Property");
            var exitsproperty = GetNotAsync(id);
            if (exitsproperty == null) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Property");

            var property = propertyRepository.UpdateEnable(id, enable);

            if (property == null) return MessageResponse(MessageCode.TransactionNotProcessed, MessageType.Error);

            response = MessageResponse(MessageCode.Success, MessageType.Success, "Property");
            response.Data = mapper.Map<PropertyDto>(property);

            return response;
        }

        //Method to Validate property
        public BaseResponse<PropertyDto> Validate(PropertySaveDto propertyInfoEntity, bool add)
        {
            if (propertyInfoEntity.ZoneId == null) return MessageResponse(MessageCode.Required, MessageType.Error, "Zone");
            var zone = zoneLogic.GetNotAsync(propertyInfoEntity.ZoneId);
            if (zone == null) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Zone");

            if (propertyInfoEntity.OwnerId == null) return MessageResponse(MessageCode.Required, MessageType.Error, "Zone");
            var owner = accountLogic.GetNotAsync(propertyInfoEntity.OwnerId);
            if (owner == null) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Owner");

            if (add)
            {
                var exitsproperty = BuyProperty(propertyInfoEntity);
                if (exitsproperty) return MessageResponse(MessageCode.Exists, MessageType.Error, "Property");
            }
            else
            {
                if (propertyInfoEntity.Id == null) return MessageResponse(MessageCode.Required, MessageType.Error, "Property");
                var exitsproperty = GetNotAsync(propertyInfoEntity.Id);
                if (exitsproperty == null) return MessageResponse(MessageCode.DoesNotexist, MessageType.Error, "Property");

            }

            return new BaseResponse<PropertyDto>();

        }


        //Method to return response message
        private BaseResponse<PropertyDto> MessageResponse(MessageCode code, MessageType messageType, string additionalMessage = "")
        {
            BaseResponse<PropertyDto> response = new BaseResponse<PropertyDto>();
            response.MessageCode = code;
            response.Message = String.Format("{0} {1}", messageServices.GetMessage(code, messageType), additionalMessage);
            response.MessageType = messageType;
            return response;
        }

        //Method to filter properties
        private List<PropertyBasicDto> Filter(List<PropertyBasicDto> properties, FindPropertyDto find)
        {
            if (find != null && properties.Any())
            {
                if (!string.IsNullOrEmpty(find.IdZone.ToString()) && find.IdZone.ToString() != "00000000-0000-0000-0000-000000000000") properties = properties.Where(x => x.ZoneId == find.IdZone).ToList();
                if (find.YearMin > 0 && find.YearMax > find.YearMin) properties = properties.Where(x => x.Year >= find.YearMin && x.Year <= find.YearMax).ToList();
                if (find.PriceMin > 0 && find.PriceMax > find.PriceMin) properties = properties.Where(x => x.Price >= find.PriceMin && x.Price <= find.PriceMax).ToList();
                if (find.RoomsMin > 0 && find.RoomsMax > find.RoomsMin) properties = properties.Where(x => x.Rooms >= find.RoomsMin && x.Rooms <= find.RoomsMax).ToList();
                if (find.PropertyType != PropertyType.None) properties = properties.Where(x => x.PropertyType == find.PropertyType).ToList();
                if (find.ConditionType != ConditionType.None) properties = properties.Where(x => x.ConditionType == find.ConditionType).ToList();
                if (find.SecurityType != SecurityType.None) properties = properties.Where(x => x.SecurityType == find.SecurityType).ToList();
                if (find.AreaType != AreaType.None) properties = properties.Where(x => x.AreaType == find.AreaType).ToList();
                if (find.WithFurnished == WithFurnished.Furnished) properties = properties.Where(x => x.Furnished == true).ToList();
                if (find.WithFurnished == WithFurnished.NotFurnished) properties = properties.Where(x => x.Furnished == false).ToList();
                if (find.WithGarages == WithGarages.Garages) properties = properties.Where(x => x.Garages > 0).ToList();
                if (find.WithGarages == WithGarages.NotGarages) properties = properties.Where(x => x.Garages < 0).ToList();
                if (find.WithSwimmingPool == WithSwimmingPool.SwimmingPool) properties = properties.Where(x => x.SwimmingPool == true).ToList();
                if (find.WithSwimmingPool == WithSwimmingPool.NotSwimmingPool) properties = properties.Where(x => x.SwimmingPool == false).ToList();
                if (find.WithGym == WithGym.Gym) properties = properties.Where(x => x.Gym == true).ToList();
                if (find.WithGym == WithGym.NotGym) properties = properties.Where(x => x.Gym == false).ToList();
                if (find.WithOceanfront == WithOceanfront.Oceanfront) properties = properties.Where(x => x.Oceanfront == true).ToList();
                if (find.WithOceanfront == WithOceanfront.NotOceanfront) properties = properties.Where(x => x.Oceanfront == false).ToList();
                if (find.WithImages == WithImages.Images) properties = properties.Where(x => !string.IsNullOrEmpty(x.ImageUrl)).ToList();
                if (find.WithImages == WithImages.NotImages) properties = properties.Where(x => string.IsNullOrEmpty(x.ImageUrl)).ToList();

                if (find.OrderProperty == OrderProperty.PriceMin) properties = properties.OrderBy(x => x.Price).ToList();
                if (find.OrderProperty == OrderProperty.PriceMax) properties = properties.OrderByDescending(x => x.Price).ToList();
                if (find.OrderProperty == OrderProperty.YearMax) properties = properties.OrderByDescending(x => x.Year).ToList();

                if (find.EnabledProperty == EnabledProperty.Enabled) properties = properties.Where(x => x.Enabled == true).ToList();
                if (find.EnabledProperty == EnabledProperty.NotEnabled) properties = properties.Where(x => x.Enabled == false).ToList();

            }
            return properties;
        }

        //Load rest of information to properties
        private async Task<List<PropertyBasicDto>> Arrive(List<PropertyBasicDto> properties)
        {
            if (properties.Any())
            {
                foreach (var x in properties) { 
                    x.ImageUrl = await propertyImageLogic.GetFirstForProperty(x.Id);
                    x.Type = x.PropertyType.ToString();
                    x.Condition = x.ConditionType.ToString();
                    x.Security = x.SecurityType.ToString();
                    x.Area = x.AreaType.ToString();
                }
            }
            return properties;
        }

        //Load rest of information to a property
        private PropertyDto Arrive(PropertyDto property)
        {
            if (property != null)
            {
                property.Type = property.PropertyType.ToString();
                property.Condition = property.ConditionType.ToString();
                property.Security = property.SecurityType.ToString();
                property.Area = property.AreaType.ToString();
            }
            return property;
        }

        //Method to buy property with existing
        private bool BuyProperty(PropertySaveDto property)
        {
            var properties = GetAllNotAsync();
            return properties.Where(x => x.Address == property.Address && x.ZoneId == property.ZoneId && x.OwnerId == property.OwnerId && x.Year == property.Year).Any();
        }

    }
}
