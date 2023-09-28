using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Enums.EnumType;

namespace Domain.Dtos
{
    public class FindPropertyDto : IValidatableObject
    {

        [Required]
        public Guid? IdCity { get; set; }

        public Guid? IdZone { get; set; }

        [Range(1900, 3000)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int YearMin { get; set; } = 2000;

        [Range(1900, 3000)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int YearMax { get; set; } = DateTime.Now.Year;

        [DataType(DataType.Currency)]
        public decimal PriceMin { get; set; } = 0;

        [DataType(DataType.Currency)]
        public decimal PriceMax { get; set; }

        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int RoomsMin { get; set; } = 1;

        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int RoomsMax { get; set; } = 10;


        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Page { get; set; } = 10;


        public PropertyType PropertyType { get; set; } = PropertyType.None;

        public ConditionType ConditionType { get; set; } = ConditionType.None;

        public SecurityType SecurityType { get; set; } = SecurityType.None;

        public AreaType AreaType { get; set; } = AreaType.None;

        public WithFurnished WithFurnished { get; set; } = WithFurnished.Both;

        public WithGarages WithGarages { get; set; } = WithGarages.Both;

        public WithSwimmingPool WithSwimmingPool { get; set; } = WithSwimmingPool.Both;

        public WithGym WithGym { get; set; } = WithGym.Both;

        public WithOceanfront WithOceanfront { get; set; } = WithOceanfront.Both;

        public WithImages WithImages { get; set; } = WithImages.Both;

        public OrderProperty OrderProperty { get; set; } = OrderProperty.None;

        public EnabledProperty EnabledProperty { get; set; } = EnabledProperty.Both;


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IdCity.HasValue) yield return new ValidationResult(MessageCode.Required.ToString(), new[] { nameof(IdCity) });
            if (PriceMin > PriceMax) yield return new ValidationResult(MessageCode.Minimum.ToString(), new[] { nameof(PriceMin), nameof(PriceMax) });
            if (YearMin > YearMax) yield return new ValidationResult(MessageCode.Minimum.ToString(), new[] { nameof(YearMin), nameof(YearMax) });
            if (RoomsMin > RoomsMax) yield return new ValidationResult(MessageCode.Minimum.ToString(), new[] { nameof(RoomsMin), nameof(RoomsMax) });
            if (!Enum.IsDefined(typeof(PropertyType), PropertyType)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(PropertyType) });
            if (!Enum.IsDefined(typeof(ConditionType), ConditionType)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(ConditionType) });
            if (!Enum.IsDefined(typeof(SecurityType), SecurityType)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(SecurityType) });
            if (!Enum.IsDefined(typeof(AreaType), AreaType)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(AreaType) });
            if (!Enum.IsDefined(typeof(WithFurnished), WithFurnished)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(WithFurnished) });
            if (!Enum.IsDefined(typeof(WithGarages), WithGarages)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(WithGarages) });
            if (!Enum.IsDefined(typeof(WithSwimmingPool), WithSwimmingPool)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(WithSwimmingPool) });
            if (!Enum.IsDefined(typeof(WithGym), WithGym)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(WithGym) });
            if (!Enum.IsDefined(typeof(WithOceanfront), WithOceanfront)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(WithOceanfront) });
            if (!Enum.IsDefined(typeof(WithImages), WithImages)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(WithImages) });
            if (!Enum.IsDefined(typeof(OrderProperty), OrderProperty)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(OrderProperty) });
            if (!Enum.IsDefined(typeof(EnabledProperty), EnabledProperty)) yield return new ValidationResult(MessageCode.DoesNotexist.ToString(), new[] { nameof(EnabledProperty) });
        }
    }
}
