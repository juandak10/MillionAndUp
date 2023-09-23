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

        public Task<Account> Get(Guid? id);

        public Task<List<Account>> GetAll();

        public Task<BaseResponse<Account>> ResponseDefault(int code, MessageType messageType, string additionalMessage = "");
    }
}
