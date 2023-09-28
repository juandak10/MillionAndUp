using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.EnumType;

namespace Domain.Dtos
{
    public class PropertySaveDto : IValidatableObject
    {
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!ZoneId.HasValue) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(ZoneId) });
            if (!OwnerId.HasValue) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(OwnerId) });

            if (string.IsNullOrEmpty(Name)) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Name) });
            if (string.IsNullOrEmpty(Address)) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Address) });
            if (Latitude < -90 || Latitude > 90) yield return new ValidationResult(MessageCode.Minimum.ToString(), new[] { nameof(Name) });
            if (Longitude < -180 || Longitude > 180) yield return new ValidationResult(MessageCode.Minimum.ToString(), new[] { nameof(Name) });
            if (Year <= 0) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Year) });
            if (Price <= 0) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Price) });

            if (!Enum.IsDefined(typeof(PropertyType), PropertyType)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(PropertyType) });
            if (!Enum.IsDefined(typeof(ConditionType), ConditionType)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(ConditionType) });
            if (!Enum.IsDefined(typeof(SecurityType), SecurityType)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(SecurityType) });
            if (!Enum.IsDefined(typeof(AreaType), AreaType)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(AreaType) });

            if (Rooms <= 0) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Rooms) });
            if (Bathrooms <= 0) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Bathrooms) });
            if (TotalSquareFeet <= 0) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(TotalSquareFeet) });
            if (Garages <= 0) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Garages) });
            if (Floor <= 0) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Floor) });
            if (Levels <= 0) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Levels) });

        }


    }
}
