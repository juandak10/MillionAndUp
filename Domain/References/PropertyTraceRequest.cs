using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.References
{
    public class PropertyTraceRequest 
    {

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

        [Required]
        public Guid? OwnerOldId { get; set; }
        [Required]

        public Guid? PropertyId { get; set; }


    }
}
