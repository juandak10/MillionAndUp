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
            var account = await Get(id);
            account.Enabled = false;
            account.Update = DateTime.Now;
            millionAndUpContext.Accounts.Update(account);
            await millionAndUpContext.SaveChangesAsync();
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
        public async Task<Account> Insert(Account account)
        {
            var accountInsert = millionAndUpContext.Accounts.Add(account).Entity;
            await millionAndUpContext.SaveChangesAsync();
            return accountInsert;
        }

        //Update account from database
        public async Task<Account> Update(Account account)
        {
            var accountUpdate = await Get(account.Id);
            accountUpdate.Address = account.Address;
            accountUpdate.Update = DateTime.Now;
            accountUpdate.RoleType = account.RoleType;
            accountUpdate.AccountType = account.AccountType;
            accountUpdate.Name = account.Name;
            accountUpdate.Address = account.Address;
            accountUpdate.Phone = account.Phone;
            accountUpdate.Email = account.Email;
            accountUpdate.Password = account.Password;
            accountUpdate.PhotoUrl = account.PhotoUrl;
            accountUpdate.Birthday = account.Birthday;
            millionAndUpContext.Accounts.Update(accountUpdate);
            await millionAndUpContext.SaveChangesAsync();
            return accountUpdate;
        }

        //Get account with email and password from database
        public async Task<Account> GetForEmailAndPassword(string email, string password)
        {
            return await millionAndUpContext.Accounts.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }

    }
}
