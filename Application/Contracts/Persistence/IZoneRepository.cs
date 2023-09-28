using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IZoneRepository
    {
        public Task<Zone> Get(Guid? id);

        public Zone GetNotAsync(Guid? id);

        public Task<List<Zone>> GetAll();

        public Task<List<Zone>> GetAllForCity(Guid? idCity);

    }
}
