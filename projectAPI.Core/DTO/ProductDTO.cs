using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace projectAPI.Core.DTO
{
    public class ProductDTO
    {
       public string? image { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
    }
}
