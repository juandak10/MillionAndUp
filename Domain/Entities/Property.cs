using System;
using System.Collections.Generic;
using static Domain.Enums.EnumType;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Domain.Entities
{
    public partial class Property
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodeInternal { get; set; }

        [Required]
        [Range(1900, 3000)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Year { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public bool Enabled { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Create { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Update { get; set; }

        [Required]
        public PropertyType PropertyType { get; set; }

        [Required]
        public ConditionType ConditionType { get; set; }

        [Required]
        public SecurityType SecurityType { get; set; }

        [Required]
        public AreaType AreaType { get; set; }

        public bool Furnished { get; set; }

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
        public bool? SwimmingPool { get; set; }

        [Required]
        public bool? Gym { get; set; }

        [Required]
        public bool? Oceanfront { get; set; }

        [Required]
        public bool? Elevator { get; set; }

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Floor { get; set; }

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Levels { get; set; }

        public Guid? ZoneId { get; set; }
        public Guid? OwnerId { get; set; }

        public virtual Zone Zone { get; set; }
        public virtual Account Owner { get; set; }

        public virtual List<PropertyImage> PropertyImages { get; set; }
        public virtual List<PropertyTrace> PropertyTraces { get; set; }
    }
}
