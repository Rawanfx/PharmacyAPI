using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using projectAPI.Core.Domain.Entities;
using projectAPI.Core.DTO;

namespace projectAPI.Core.ServiceCotract
{
    public interface IProductService
    {
        public List<ProductDTO> GetProductWithCategoryId(int id);
        public List<TopProductDTO> GetTopProducts();
        //  public void AddProduct(ProductDTO product);
         List<ProductDTO> Search(string name);
        ProductDetails GetProductDetails(int id);
        bool AddProduct(ProductCreateDTO product);
        double CalculatePrice(int count, int product_id);
        bool delete(int id);
        bool Update(int id, ProductDTO dto);

    }
}
