using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Core.DTO
{
    public class ProductFromCartDTO
    {
        public int productId { get; set; }
      public  string name { get; set; }
        public string image { get; set; }
        public int count { get; set; }
        public double price { get; set; }
    }
}
