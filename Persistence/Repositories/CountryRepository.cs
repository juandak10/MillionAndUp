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
    public class CountryRepository : ICountryRepository
    {

        private readonly MillionAndUpContext millionAndUpContext;

        public CountryRepository(MillionAndUpContext millionAndUpContext)
        {
            this.millionAndUpContext = millionAndUpContext;
        }


        //Get account from database
        public async Task<Country> Get(Guid? id)
        {
            return await millionAndUpContext.Countries.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Get all accounts from database
        public async Task<List<Country>> GetAll()
        {
            return await millionAndUpContext.Countries.ToListAsync();
        }

    }
}
