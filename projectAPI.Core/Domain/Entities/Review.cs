using projectAPI.Core.Domain.IdentityEntities;
using projectAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectAPI.Core.Domain.Entities
{
    public class Review
    {
         [Key]
         public int id { get; set; }
        [Range(1,6)]
         public double rate { get; set; }
        
         public ApplicationUser user { get; set; }
         
       
         public Product product { get; set; }
         public DateTime create { get; set; }

        [ForeignKey("user")]
        public string userId { get; set; }

        [ForeignKey("product")]
        public int productId { get; set; }
         public string ?comment { get; set; }
    }
}
