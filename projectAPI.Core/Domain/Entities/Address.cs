using projectAPI.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectAPI.Core.Domain.Entities
{
    public class Address
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("user")]
        public string userId { get; set; }
      
        public ApplicationUser user { get; set; }
        public string city { get; set; }
        public string street { get; set; }
    }
}
