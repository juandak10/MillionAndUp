using Application.Contracts.Persistence;
using Application.Interfaces;
using Domain.Entities;
using Domain.References;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Tools;
using static Domain.Enums.EnumType;

namespace Application.Services
{
    //In this class all the processes associated with the accounts are managed.
    public class AccountServices: IAccountServices
    {
        public IConfiguration configuration;
        private readonly IAccountRepository accountRepository;
        private readonly IMessageServices messageServices;
        private readonly JwtToken jwtToken;

        //Controller
        public AccountServices(IConfiguration configuration, IAccountRepository accountRepository, IMessageServices messageServices)
        {
            this.accountRepository = accountRepository;
            this.messageServices = messageServices;
            this.configuration = configuration;
            jwtToken = new JwtToken(this.configuration);
        }

        //Method to obtain an account for login
        public async Task<BaseResponse<Account>> GetAccountLogin(LoginRequest login)
        {
            var response = await ValidateLoginRequest(login);

            if(response.MessageType == MessageType.Error)  return response;

            var account = await GetForEmailAndPassword(login.Email, login.Password);

            if (account == null) return await ResponseDefault(MessageCode.DoesNotexist, MessageType.Error, "Account");

            response = await ResponseDefault(MessageCode.Success, MessageType.Success, "Account");
            response.Data = account;
            response.Token = CreateToken(account);

            return response;
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

        //Method to delete a account
        public BaseResponse<Account> Delete(Guid? id)
        {
            throw new NotImplementedException();
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

        private async Task<BaseResponse<Account>> ValidateLoginRequest(LoginRequest login)
        {
            if (login == null)
                return await ResponseDefault(MessageCode.Required, MessageType.Error, "Login");
            if (string.IsNullOrEmpty(login.Email))
                return await ResponseDefault(MessageCode.Required, MessageType.Error, "Email");
            if (string.IsNullOrEmpty(login.Password))
                return await ResponseDefault(MessageCode.Required, MessageType.Error, "Passwork");

            return new BaseResponse<Account>();
        }

        //Method create token sesion
        public string CreateToken(Account account)
        {
            try
            {
                return jwtToken.CreateToken(account);
            }
            catch (Exception error) when (error.Data != null)
            {
                return string.Empty;
            }

        }

        //Method to return response message
        public async Task<BaseResponse<Account>> ResponseDefault(MessageCode messageCode, MessageType messageType, string additionalMessage = "")
        {
            BaseResponse<Account> response = new BaseResponse<Account>();
            response.MessageCode = messageCode;
            response.Message = String.Format("{0} {1}",await messageServices.GetMessage(messageCode, messageType), additionalMessage);
            response.MessageType = messageType;
            return response;
        }

    }
}
