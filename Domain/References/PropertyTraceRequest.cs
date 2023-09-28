using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Domain.Enums.EnumType;

namespace Domain.References
{
    public class PropertyTraceRequest : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!OwnerNewId.HasValue) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(OwnerNewId) });
            if (!OwnerOldId.HasValue) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(OwnerOldId) });
            if (!PropertyId.HasValue) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(PropertyId) });
            if (!DateSale.HasValue) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(DateSale) });
            if (string.IsNullOrEmpty(Name)) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Name) });
            if (Value < 0) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Value) });
            if (Tax < 0) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(Tax) });
        }

    }
}
