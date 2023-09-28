using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.EnumType;

namespace Application.Contracts.Persistence
{
    public interface IMessageRepository
    {
        public List<Message> GetAll();
    }
}
