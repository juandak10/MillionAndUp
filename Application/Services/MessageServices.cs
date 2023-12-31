﻿using Application.Contracts.Persistence;
using Application.Interfaces;
using static Domain.Enums.EnumType;

namespace Application.Services
{
    public class MessageServices : IMessageServices
    {
        private readonly IMessageRepository messageRepository;

        public MessageServices(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        //Method to get success or error messages from database
        public string GetMessage(MessageCode messageCode, MessageType messageType)
        {
            var message = string.Empty;

            var result = messageRepository.GetAll();

            if (result != null && result.Any())
            {
                var messages = result.Where(x => x.MessageCode == messageCode && x.MessageType == messageType);

                if (messages != null && messages.Any())
                {
                    message = messages.Select(x => x.Text).FirstOrDefault();
                }
            }

            return message;
        }
    }
}
