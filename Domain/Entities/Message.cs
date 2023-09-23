using System.ComponentModel.DataAnnotations;
using static Domain.Enums.EnumType;

namespace Domain.Entities
{
    public class Message
    {
        [Required]
        public int Code { get; set; }

        [Required]
        public MessageType MessageType { get; set; }

        [Required]
        public string? Text { get; set; }

    }
}
