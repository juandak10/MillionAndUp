using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class PropertyImageBasicDto
    {
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string Url { get; set; }

        [Required]
        public bool? Enabled { get; set; }
    }
}
