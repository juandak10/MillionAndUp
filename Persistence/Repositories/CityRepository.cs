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
    public class CityRepository : ICityRepository
    {

        private readonly MillionAndUpContext millionAndUpContext;

        public CityRepository(MillionAndUpContext millionAndUpContext)
        {
            this.millionAndUpContext = millionAndUpContext;
        }

        //Get account from database
        public async Task<City> Get(Guid? id)
        {
            return await millionAndUpContext.Cities.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Get all accounts from database
        public async Task<List<City>> GetAll()
        {
            return await millionAndUpContext.Cities.ToListAsync();
        }

    }
}
