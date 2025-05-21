using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectAPI.Core.Domain.Entities
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int quatity { get; set; }
        public string manufacturer { get; set; }
        [ForeignKey("category")]
        public int category_id { get; set; }
      
        public Category category { get; set; }
        public string? ImageUrl { get; set; }
        public string? description { get; set; }
      //  public double? decount { get; set; }

    }
}
