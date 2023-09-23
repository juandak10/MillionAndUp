using System.ComponentModel.DataAnnotations;
using static Domain.Enums.EnumType;

namespace Domain.Entities
{
    public class Message
    {
        [Key]
        public MessageCode MessageCode { get; set; }

        [Required]
        public MessageType MessageType { get; set; }

        [Required]
        public string? Text { get; set; }

    }
}
