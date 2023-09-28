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
    public class FindPropertyDto
    {
        [Required]
        public Guid? IdCity { get; set; }

        public Guid? IdZone { get; set; }

        [Required]
        [Range(1900, 3000)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int YearMin { get; set; }

        [Required]
        [Range(1900, 3000)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int YearMax { get; set; } = DateTime.Now.Year;

        [Required]
        [DataType(DataType.Currency)]
        public decimal PriceMin { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PriceMax { get; set; }

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int RoomsMin { get; set; }

        [Required]
        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int RoomsMax { get; set; }

        [Required]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Page { get; set; }

        [Required]
        public PropertyType PropertyType { get; set; } = PropertyType.None;
        [Required]
        public ConditionType ConditionType { get; set; } = ConditionType.None;
        [Required]
        public SecurityType SecurityType { get; set; } = SecurityType.None;
        [Required]
        public AreaType AreaType { get; set; } = AreaType.None;
        [Required]
        public WithFurnished WithFurnished { get; set; } = WithFurnished.Both;
        [Required]
        public WithGarages WithGarages { get; set; } = WithGarages.Both;
        [Required]
        public WithSwimmingPool WithSwimmingPool { get; set; } = WithSwimmingPool.Both;
        [Required]
        public WithGym WithGym { get; set; } = WithGym.Both;
        [Required]
        public WithOceanfront WithOceanfront { get; set; } = WithOceanfront.Both;
        [Required]
        public WithImages WithImages { get; set; } = WithImages.Both;
        [Required]
        public OrderProperty OrderProperty { get; set; } = OrderProperty.None;
        [Required]
        public EnabledProperty EnabledProperty { get; set; } = EnabledProperty.Both;
    }
}
