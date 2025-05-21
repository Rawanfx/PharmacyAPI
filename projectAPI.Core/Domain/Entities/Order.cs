using projectAPI.Core.Domain.IdentityEntities;
using projectAPI.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace projectAPI.Core.Domain.Entities
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        public ApplicationUser user { get; set; }
        public DateTime date { get; set; }
        [ForeignKey("user")]
        public string userId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? paymentId { get; set; }
    }
}
