using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBase;

namespace Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MillionAndUpContext millionAndUpContext;

        public MessageRepository(MillionAndUpContext millionAndUpContext)
        {
            this.millionAndUpContext = millionAndUpContext;
        }

        public async Task<List<Message>> GetAll()
        {
            return await millionAndUpContext.Messages.ToListAsync();
        }
    }
}
