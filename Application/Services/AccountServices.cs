﻿using Application.Contracts.Persistence;
using Application.Interfaces;
using Domain.Entities;
using Domain.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.EnumType;

namespace Application.Services
{
    //In this class all the processes associated with the accounts are managed.
    public class AccountServices: IAccountServices
    {
        private readonly IAccountRepository accountRepository;
        private readonly IMessageServices messageServices;

        //Controller
        public AccountServices(IAccountRepository accountRepository, IMessageServices messageServices)
        {
            this.accountRepository = accountRepository;
            this.messageServices = messageServices;
        }

        //Method to obtain an account for login
        public async Task<BaseResponse<Account>> GetAccountLogin(LoginRequest login)
        {
            BaseResponse<Account> response = new BaseResponse<Account>();

            if (login == null) return await ResponseDefault(4, MessageType.Error, "LoginEntity");
            if (string.IsNullOrEmpty(login.Email)) return await ResponseDefault(4, MessageType.Error, "Email");
            if (string.IsNullOrEmpty(login.Password)) return await ResponseDefault(4, MessageType.Error, "Passwork");

            var account = await GetForEmailAndPassword(login.Email, login.Password);

            if (account == null) return await ResponseDefault(3, MessageType.Error, "Account");

            response = await ResponseDefault(1, MessageType.Success, "Account");
            response.Data = account;

            return response;
        }

        //Method to delete a account
        public BaseResponse<Account> Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        //Method to get account for id
        public async Task<Account> Get(Guid? id)
        {
            Account accountEntity = new Account();
            if (id.HasValue) accountEntity = await accountRepository.Get(id);
            return accountEntity;
        }

        //Method to get all system accounts
        public async Task<List<Account>> GetAll()
        {
            var accountEntities = new List<Account>();

            var accounts = await accountRepository.GetAll();
            if (accounts.Any())
            {
                accountEntities = accounts.ToList();
            }

            return accountEntities;
        }

        //Method to add a account
        public BaseResponse<Account> Insert(Account @object)
        {
            throw new NotImplementedException();
        }

        //Method to update a account
        public BaseResponse<Account> Update(Account @object)
        {
            throw new NotImplementedException();
        }

        //Method to obtain an account by email and password
        private async Task<Account> GetForEmailAndPassword(string email, string password)
        {
            return await accountRepository.GetForEmailAndPassword(email, password);
        }

        //Method to return response message
        public async Task<BaseResponse<Account>> ResponseDefault(int code, MessageType messageType, string additionalMessage = "")
        {
            BaseResponse<Account> response = new BaseResponse<Account>();
            response.Code = code;
            response.Message = String.Format("{0} {1}",await messageServices.GetMessage(code, messageType), additionalMessage);
            response.MessageType = messageType;
            return response;
        }

    }
}