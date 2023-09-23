using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Domain.Entities
{
    public partial class PropertyTrace
    {
        [Key]
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

        [Required]
        public Guid? OwnerOldId { get; set; }
        [Required]

        public Guid? PropertyId { get; set; }

        public DateTime Create { get; set; }

        public virtual Property Property { get; set; }


    }
}
