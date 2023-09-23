using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.EnumType;

namespace Application.Interfaces
{
    public interface IMessageServices
    {
        public Task<string> GetMessage(int Code, MessageType messageType);
    }
}
