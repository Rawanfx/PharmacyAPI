using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using projectAPI.Core.Domain.Entities;
using projectAPI.Core.DTO;
using projectAPI.Core.ServiceCotract;
using projectAPI.Result;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace projectAPI.Core.Services
{
    public class ShopingCartServices : IShopingCartServices
    {
        private readonly AppDbContext context;
        public ShopingCartServices (AppDbContext context)
        {
            this.context = context;
        }
        
        public bool AddToCart(int product_id, int count,string user_id)
        {
            var result = context.ShopingCart.FirstOrDefault(x => x.productId == product_id && x.userId == user_id);
            if (result != null)
            {
                result.count += count;
            }
            else
            {
                ShopingCart shopingCart = new ShopingCart()
                {
                    count = count,
                    productId = product_id,
                    userId = user_id
                };
                context.ShopingCart.Add(shopingCart);
            }
            try
            {
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public OperationResult DeleteFromCart(string userId, int productId)
        {
            var product = context.ShopingCart.Where(x => x.productId == productId && x.userId == userId);
            if (product == null)
            {
                return new OperationResult() { success = false, message = "product not found",statusCode=System.Net.HttpStatusCode.NotFound };
            }
            try
            {
                context.ShopingCart.Remove(product.FirstOrDefault());
                context.SaveChanges();
                return new OperationResult() { message = "Product removed successfully", success = true,statusCode=System.Net.HttpStatusCode.OK };
            }
            catch
            {
                return new OperationResult() { success = false, message = "Can't remove this product",statusCode=System.Net.HttpStatusCode.BadRequest };
            }
        }

        public List<ProductFromCartDTO> GetProductsFromCart(string user_id)
        {
            var result = context.ShopingCart
                .Where(x=>x.userId==user_id)
                .Join(context.Product, x => x.productId, y => y.id,
                (x, y) => new ProductFromCartDTO {
                    name = y.name, 
                    count = x.count, 
                    image = y.ImageUrl,
                    price=y.price,productId=y.id
                }).ToList();
            return result;
                
        }

        public double GetTotalPrice(string userId)
        {
            var res = context.ShopingCart.Where(x => x.userId == userId)
                             .Sum(x => (x.Product.price)*x.count);
            return res;
        }

        public double PriceForOnePicie( int productId, int count)
        {
            var price = context.Product.Where(x => x.id == productId)
                 .Select(x => x.price)
                 .FirstOrDefault();
            return price * count;
        }
    }
}
