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
        public Guid? IdCity { get; set; }

        public Guid? IdZone { get; set; }

        [Range(1900, 3000)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int YearMin { get; set; }

        [Range(1900, 3000)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int YearMax { get; set; } = DateTime.Now.Year;

        [DataType(DataType.Currency)]
        public decimal PriceMin { get; set; }

        [DataType(DataType.Currency)]
        public decimal PriceMax { get; set; }

        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int RoomsMin { get; set; }

        [Range(1, 50)]
        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int RoomsMax { get; set; }


        [RegularExpression("[0-9]*", ErrorMessage = "Only numeric value")]
        public int Page { get; set; }


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
    }
}
