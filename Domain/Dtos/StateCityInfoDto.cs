using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class StateCityInfoDto
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
    }
}
