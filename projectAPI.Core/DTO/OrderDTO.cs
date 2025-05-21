using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Core.DTO
{
    public class OrderDTO
    {
        public int id { get; set; }
        public string userId { get; set; }
        public DateTime date { get; set; }
        public int success { get; set; }
    }
}
