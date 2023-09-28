using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class PropertyTraceDto
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? DateSale { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Tax { get; set; }

        [Required]
        public Guid? OwnerNewId { get; set; }
        public string NameOwnerNew { get; set; }

        [Required]
        public Guid? OwnerOldId { get; set; }
        [Required]

        public string NameOwnerOld { get; set; }

        public Guid? PropertyId { get; set; }

        public DateTime Create { get; set; }

    }
}
