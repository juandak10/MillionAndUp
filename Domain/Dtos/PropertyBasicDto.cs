using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.EnumType;

namespace Domain.Dtos
{
    public class PropertyBasicDto
    {
        public Guid Id { get; set; }
        public Guid ZoneId { get; set; }
        public Guid OwnerId { get; set; }
        public int CodeInternal { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required]
        [Range(1900, 3000)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Year { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public PropertyType PropertyType { get; set; }
        public string Type { get; set; }
        [Required]
        public ConditionType ConditionType { get; set; }
        public string Condition { get; set; }
        [Required]
        public SecurityType SecurityType { get; set; }
        public string Security { get; set; }
        [Required]
        public AreaType AreaType { get; set; }
        public string Area { get; set; }

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Rooms { get; set; }

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Bathrooms { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int TotalSquareFeet { get; set; }

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Garages { get; set; }

        [Required]
        public bool Furnished { get; set; }
        [Required]
        public bool? SwimmingPool { get; set; }
        [Required]
        public bool? Gym { get; set; }
        [Required]
        public bool? Oceanfront { get; set; }
        [Required]
        public bool Enabled { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}
