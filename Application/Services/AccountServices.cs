using Application.Contracts.Persistence;
using Application.Interfaces;
using Domain.Entities;
using Domain.References;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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

        //Method to obtain an account for token
        public async Task<Account> GetForToken(string srtoken)
        {
            Account accountEntity = new Account();
            var claims = jwtToken.GetClaims(srtoken);
            if (claims != null && claims.Any())
            {
                var id = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                Guid guid;
                if (Guid.TryParse(id, out guid))
                {
                    accountEntity = await accountRepository.Get(guid);
                }

            }

            return accountEntity;
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
        public async Task<BaseResponse<Account>> Delete(Guid? id)
        {
            var response = new BaseResponse<Account>();

            if (!id.HasValue) return await ResponseDefault(MessageCode.Required, MessageType.Error, "Id");

            var accountEntity = await accountRepository.Delete(id);

            response = await ResponseDefault(MessageCode.Success, MessageType.Success, "Delete Account");
            response.Data = accountEntity;
            
            return response;
        }

        //Method to add a account
        public async Task<BaseResponse<Account>> Insert(Account account)
        {
            var response = new BaseResponse<Account>();

            if (account == null) return await ResponseDefault(MessageCode.Required, MessageType.Error, "Account");

            var accountEntity = await accountRepository.Insert(account);

            response = await ResponseDefault(MessageCode.Success, MessageType.Success, "Add Account");
            response.Data = accountEntity;

            return response;
        }

        //Method to update a account
        public async Task<BaseResponse<Account>> Update(Account account)
        {
            var response = new BaseResponse<Account>();

            if (account == null) return await ResponseDefault(MessageCode.Required, MessageType.Error, "Account");

            var accountEntity = await accountRepository.Update(account);

            response = await ResponseDefault(MessageCode.Success, MessageType.Success, "Update Account");
            response.Data = accountEntity;

            return response;
        }

        //Method create token sesion
        private string CreateToken(Account account)
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

        //Method to obtain an account by email and password
        private async Task<Account> GetForEmailAndPassword(string email, string password)
        {
            return await accountRepository.GetForEmailAndPassword(email, password);
        }

        //Method to validate login request
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
