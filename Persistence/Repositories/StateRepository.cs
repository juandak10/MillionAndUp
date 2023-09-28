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
    public class StateRepository : IStateRepository
    {

        private readonly MillionAndUpContext millionAndUpContext;

        public StateRepository(MillionAndUpContext millionAndUpContext)
        {
            this.millionAndUpContext = millionAndUpContext;
        }

        //Get state from database
        public async Task<State> Get(Guid? id)
        {
            return await millionAndUpContext.States.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Get all state from database
        public async Task<List<State>> GetAll()
        {
            return await millionAndUpContext.States.ToListAsync();
        }


    }
}
