using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public partial class City
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string Abbreviative { get; set; }

        public Guid? StateId { get; set; }

        public virtual State State { get; set; }

        public virtual List<Zone> Zones { get; set; }
    }
}
