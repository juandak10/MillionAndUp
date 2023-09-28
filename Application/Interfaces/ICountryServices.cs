using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICountryServices
    {
        public Task<CountryInfoDto> Get(Guid? id);

        public Task<List<Country>> GetAll();

    }
}
