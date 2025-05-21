using projectAPI.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectAPI.Core.Domain.Entities
{
    public class Payment
    {
        
        public Order order { get; set; }
        [ForeignKey("order")]
        public int orderId { get; set; }
        public DateTime date { get; set; }
        public double price { get; set; }
        
        public ApplicationUser user { get; set; }
        [ForeignKey("user")]
        public string userId { get; set; }
        [Key]
        public int id { get; set; }
    }
}
