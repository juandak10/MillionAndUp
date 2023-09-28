using Domain.Dtos;
using Domain.Entities;
using Domain.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.EnumType;

namespace Application.Interfaces
{
    public interface IAccountServices
    {
        public Task<BaseResponse<Account>> GetAccountLogin(LoginRequest login);

        public Task<Account> GetForToken(string srtoken);

        public Task<AccountInfoDto> Get(Guid? id);

        public AccountInfoDto GetNotAsync(Guid? id);

        public Task<AccountDto> GetBasic(Guid? id);

        public Task<List<Account>> GetAll();

        public Task<BaseResponse<Account>> Delete(Guid? id);

        public Task<BaseResponse<Account>> Insert(AccountDto account);

        public Task<BaseResponse<Account>> Update(AccountDto account);

    }
}
