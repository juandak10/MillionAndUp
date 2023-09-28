using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICityServices
    {
        public Task<CityInfoDto> Get(Guid? id);

        public Task<List<City>> GetAll();

    }
}
