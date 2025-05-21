using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Core.DTO
{
    public class CategoryAddDTO
    {
        [Required]
        [MinLength(5)]
        public string name { get; set; }
        public string description { get; set; }
    }
}
