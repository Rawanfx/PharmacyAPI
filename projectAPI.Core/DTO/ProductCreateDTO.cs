using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace projectAPI.Core.DTO
{
    public class ProductCreateDTO
    {
        public IFormFile? image { get; set; }
        [MinLength(5)]
        public string name { get; set; }
        [Range(1,int.MaxValue)]
        public double price { get; set; }
        public string description { get; set; }
        [Range(1, int.MaxValue)]
        public int quantity { get; set; }
        public int category_id { get; set; }
        //public double descount { get; set; }
    }
}
