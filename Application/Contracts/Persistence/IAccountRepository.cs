﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IAccountRepository
    {

        public Task<Account> Delete(Guid? id);

        public Task<Account> Get(Guid? id);

        public Task<List<Account>> GetAll();

        public Account Insert(Account @object);

        public Account Update(Account @object);

        public Task<Account> GetForEmailAndPassword(string email, string password);

    }
}
