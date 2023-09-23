using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    //This class is a repository that connects us to the database
    public class AccountRepository: IAccountRepository
    {

        private readonly MillionAndUpContext millionAndUpContext;

        public AccountRepository(MillionAndUpContext millionAndUpContext)
        {
            this.millionAndUpContext = millionAndUpContext;
        }

        //Delete account from database
        public async Task<Account> Delete(Guid? id)
        {
            var account = await  Get(id);
            account = millionAndUpContext.Accounts.Remove(account).Entity;
            millionAndUpContext.SaveChanges();
            return account;
        }

        //Get account from database
        public async Task<Account> Get(Guid? id)
        {
            return await millionAndUpContext.Accounts.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Get all accounts from database
        public async Task<List<Account>> GetAll()
        {
            return await millionAndUpContext.Accounts.ToListAsync();
        }

        //Add account from database
        public Account Insert(Account @object)
        {
            throw new NotImplementedException();
        }

        //Update account from database
        public Account Update(Account @object)
        {
            throw new NotImplementedException();
        }

        //Get account with email and password from database
        public async Task<Account> GetForEmailAndPassword(string email, string password)
        {
            return await millionAndUpContext.Accounts.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }

    }
}
