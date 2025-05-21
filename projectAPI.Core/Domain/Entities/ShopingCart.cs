using projectAPI.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectAPI.Core.Domain.Entities
{
    public class ShopingCart
    {
        [Key]
        public int id { get; set; }
      
        public  ApplicationUser user { get; set; }
        public Product Product { get; set; }
        [ForeignKey("user")]
        public string userId { get; set; }
        [ForeignKey("Product")]
        public int productId { get; set; }
        [Range (1,int.MaxValue)]
        public int count { get; set; }
    }
}
