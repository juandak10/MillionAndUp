using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.References;
using WeeloCore.Entities;

namespace Utilities.Tools
{
    // This class is where entities are mapped or converted to other entities.
    public class MappingProfile : Profile
    {
        //Method to convert one entity to another
        public MappingProfile()
        {
            CreateMap<AccountDto, Account>();

            CreateMap<Account, AccountDto>(); 
            CreateMap<Account, AccountInfoDto>();
            CreateMap<Property, AccountPropertyInfoDto>();

            CreateMap<Country, CountryInfoDto>();
            CreateMap<State, CountryStateInfoDto>();

            CreateMap<State, StateInfoDto>();
            CreateMap<City, StateCityInfoDto>();

            CreateMap<City, CityInfoDto>();
            CreateMap<Zone, CityZoneInfoDto>();

            CreateMap<Zone, PropertyZoneInfo>();
            CreateMap<Zone, ZoneInfoDto>();
            CreateMap<Property, ZonePropertyInfoDto>();

            CreateMap<PropertyImage, PropertyImageBasicDto>();

            CreateMap<PropertyTrace, PropertyTraceDto>();
            CreateMap<PropertyTraceDto, PropertyTrace>();
            CreateMap<PropertyTraceRequest, PropertyTraceDto> ();

            CreateMap<Property, PropertyBasicDto>();
            CreateMap<PropertyBasicDto, Property>();
            CreateMap<Property, PropertyDto>();

        }
    }
}
