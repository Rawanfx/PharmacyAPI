using System.ComponentModel.DataAnnotations;

namespace projectAPI.Core.Domain.Entities
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
       public string name { get; set; }
    }
}
