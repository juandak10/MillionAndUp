using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeloCore.Entities;

namespace Application.Interfaces
{
    public interface IZoneServices
    {

        public Task<List<Zone>> GetAllForCity(Guid? idCity);

        public Task<PropertyZoneInfo> GetInfo(Guid? id);

        public Task<ZoneInfoDto> Get(Guid? id);

        public ZoneInfoDto GetNotAsync(Guid? id);

    }
}
