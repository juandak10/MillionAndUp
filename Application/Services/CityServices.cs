using Application.Contracts.Persistence;
using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.References;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Application.Services
{
    //In this class all the processes associated with the cities are managed.
    public class CityServices : ICityServices
    {

        private readonly ICityRepository cityRepository;
        private readonly IZoneRepository zoneRepository;
        private readonly IMapper mapper;

        //Controller
        public CityServices(IZoneRepository zoneRepository, ICityRepository cityRepository, IMapper mapper)
        {
            this.cityRepository = cityRepository;
            this.zoneRepository = zoneRepository;
            this.mapper = mapper;
        }


        //Method to get a city
        public async Task<CityInfoDto> Get(Guid? id)
        {
            CityInfoDto cityInfo = new CityInfoDto();
            if (id.HasValue) cityInfo = mapper.Map<CityInfoDto>(await cityRepository.Get(id));
            if (cityInfo != null)
            {
                var zones = await zoneRepository.GetAllForCity(cityInfo.Id);
                if (zones.Any()) cityInfo.Zones = zones.Select(x => mapper.Map<CityZoneInfoDto>(x)).ToList();
            }
            return cityInfo;
        }

        //Method to get all system cities
        public async Task<List<City>> GetAll()
        {
           return await cityRepository.GetAll();
        }


    }
}
