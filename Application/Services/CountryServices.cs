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
    //In this class all the processes associated with the country are managed.
    public class CountryServices : ICountryServices
    {
        private readonly ICountryRepository countryRepository;
        private readonly IStateRepository stateRepository;
        private readonly IMapper mapper;

        //Controller
        public CountryServices(ICountryRepository countryRepository, IMapper mapper, IStateRepository stateRepository)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
            this.stateRepository = stateRepository;
        }

        //Method to get a country
        public async Task<CountryInfoDto> Get(Guid? id)
        {
            CountryInfoDto countryInfo = new CountryInfoDto();
            if (id.HasValue) countryInfo = mapper.Map<CountryInfoDto>(await countryRepository.Get(id));
            if (countryInfo != null)
            {
                var states = await stateRepository.GetAll();
                if (states.Any()) states = states.Where(x => x.CountryId == countryInfo.Id).ToList();
                if (states.Any()) countryInfo.States = states.Select(x => mapper.Map<CountryStateInfoDto>(x)).ToList();
            }
            return countryInfo;
        }

        //Method to get all countries
        public async Task<List<Country>> GetAll()
        {
            var countriesEntity = new List<Country>();
            var countries = await countryRepository.GetAll();
            if (countries.Any()) countriesEntity = countries.ToList();
            return countriesEntity;
        }


    }

}
