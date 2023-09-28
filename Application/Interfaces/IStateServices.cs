using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStateServices
    {
        public Task<StateInfoDto> Get(Guid? id);

        public Task<List<State>> GetAll();

    }
}
