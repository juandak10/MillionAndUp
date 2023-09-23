using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domain.Entities
{
    public partial class PropertyImage
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string Url { get; set; }

        [Required]
        public bool? Enabled { get; set; }
        public Guid? PropertyId { get; set; }

        public virtual Property Property { get; set; }
    }
}
