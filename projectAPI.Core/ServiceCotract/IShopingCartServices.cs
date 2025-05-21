using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using projectAPI.Core.DTO;
using projectAPI.Result;

namespace projectAPI.Core.ServiceCotract
{
    public interface IShopingCartServices
    {
       bool AddToCart(int product_id ,int count, string user_id);
        List<ProductFromCartDTO> GetProductsFromCart(string user_id);
        public double GetTotalPrice(string userId);
        public double PriceForOnePicie(int productId,int count);
        OperationResult DeleteFromCart(string userId, int productId);
    }
}
