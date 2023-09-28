using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Persistence.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    //This class is a repository that connects us to the database
    public class ZoneRepository : IZoneRepository
    {

        private readonly MillionAndUpContext millionAndUpContext;

        public ZoneRepository(MillionAndUpContext millionAndUpContext)
        {
            this.millionAndUpContext = millionAndUpContext;
        }


        //Get property from database
        public async Task<Zone> Get(Guid? id)
        {
            return await millionAndUpContext.Zones.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Get property from database
        public Zone GetNotAsync(Guid? id)
        {
            return millionAndUpContext.Zones.Where(x => x.Id == id).FirstOrDefault();
        }

        //Get all zones from database
        public async Task<List<Zone>> GetAll()
        {
            return await millionAndUpContext.Zones.ToListAsync();
        }

        //Get all zones for cities from database
        public async Task<List<Zone>> GetAllForCity(Guid? idCity)
        {
            return await millionAndUpContext.Zones.Where(x => x.CityId == idCity).ToListAsync();
        }

    }
}
