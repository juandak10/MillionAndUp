using Application.Contracts.Persistence;
using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloCore.Entities;

namespace Application.Services
{
    //In this class all the processes associated with the zone are managed.
    public class ZoneServices : IZoneServices
    {
        private readonly IMapper mapper;
        private readonly IZoneRepository zoneRepository;
        private readonly ICityRepository cityRepository;
        private readonly IStateRepository stateRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IPropertyRepository propertyRepository;

        //Controller
        public ZoneServices(IMapper mapper, IZoneRepository zoneRepository, IPropertyRepository propertyRepository, ICityRepository cityRepository, IStateRepository stateRepository, ICountryRepository countryRepository)
        {
            this.mapper = mapper;
            this.zoneRepository = zoneRepository;
            this.propertyRepository = propertyRepository;
            this.cityRepository = cityRepository;
            this.stateRepository = stateRepository;
            this.countryRepository = countryRepository;
        }

        //Method to get all a zone for city
        public async Task<List<Zone>> GetAllForCity(Guid? idCity)
        {
            return await zoneRepository.GetAllForCity(idCity);
        }

        //Method to get a zone info
        public async Task<PropertyZoneInfo> GetInfo(Guid? id)
        {
            var zoneInfoEntity = new PropertyZoneInfo();

            if (id.HasValue)
            {
                var zone = await zoneRepository.Get(id);
                if(zone != null)
                {
                    zoneInfoEntity = mapper.Map<PropertyZoneInfo>(zone);
                    if (zoneInfoEntity != null)
                    {
                        var city = await cityRepository.Get(zone.CityId);
                        if (city != null)
                        {
                            zoneInfoEntity.NameCity = city.Name;
                            zoneInfoEntity.AbbreviativeCity = city.Abbreviative;
                            zoneInfoEntity.IdState = city.StateId;
                            zoneInfoEntity.IdCity = city.Id;

                            var state = await stateRepository.Get(city.StateId);
                            if (state != null)
                            {
                                zoneInfoEntity.NameState = state.Name;
                                zoneInfoEntity.AbbreviativeState = state.Abbreviative;
                                zoneInfoEntity.IdCountry = state.CountryId;

                                var country = await countryRepository.Get(state.CountryId);
                                if (country != null)
                                {
                                    zoneInfoEntity.NameCountry = country.Name;
                                    zoneInfoEntity.AbbreviativeCountry = country.Abbreviative;
                                }
                            }
                        }
                    }

                }
            }

            return zoneInfoEntity;
        }


        //Method to get a zone
        public async Task<ZoneInfoDto> Get(Guid? id)
        {
            ZoneInfoDto zoneInfo = new ZoneInfoDto();
            if (id.HasValue) zoneInfo = mapper.Map<ZoneInfoDto>(await zoneRepository.Get(id));
            if (zoneInfo != null)
            {
                var propertis = await propertyRepository.GetAllForZones(new List<Guid>() { zoneInfo.Id});
                if (propertis.Any()) zoneInfo.Properties = propertis.Select(x => mapper.Map<ZonePropertyInfoDto>(x)).ToList();
            }
            return zoneInfo;
        }


    }
}
