using Application.Contracts.Persistence;
using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    //In this class all the processes associated with the state are managed.
    public class StateServices : IStateServices
    {
        private readonly IStateRepository stateRepository;
        private readonly ICityRepository cityRepository;
        private readonly IMapper mapper;

        //Controller
        public StateServices(IStateRepository stateRepository, ICityRepository cityRepository, IMapper mapper)
        {
            this.stateRepository = stateRepository;
            this.cityRepository = cityRepository;
            this.mapper = mapper;   
        }


        //Method to get a state
        public async Task<StateInfoDto> Get(Guid? id)
        {
            StateInfoDto stateInfo = new StateInfoDto();
            if (id.HasValue) stateInfo = mapper.Map<StateInfoDto>(await stateRepository.Get(id));
            if (stateInfo != null)
            {
                var cities = await cityRepository.GetAll();
                if (cities.Any()) cities = cities.Where(x => x.StateId == stateInfo.Id).ToList();
                if (cities.Any()) stateInfo.Cities = cities.Select(x => mapper.Map<StateCityInfoDto>(x)).ToList();
            }
            return stateInfo;
        }

        //Method to get all states
        public async Task<List<State>> GetAll()
        {
            return await stateRepository.GetAll();
        }


    }
}
