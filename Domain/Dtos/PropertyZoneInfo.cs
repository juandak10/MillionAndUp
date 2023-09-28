using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeloCore.Entities
{
    public class PropertyZoneInfo
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public Guid? IdCity { get; set; }
        public string NameCity { get; set; }
        public string AbbreviativeCity { get; set; }
        public Guid? IdState { get; set; }
        public string NameState { get; set; }
        public string AbbreviativeState { get; set; }
        public Guid? IdCountry { get; set; }
        public string NameCountry { get; set; }
        public string AbbreviativeCountry { get; set; }
    }
}
