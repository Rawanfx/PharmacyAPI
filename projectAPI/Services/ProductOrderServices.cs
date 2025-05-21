using projectAPI.Core.Domain.Entities;
using projectAPI.Core.DTO;
using projectAPI.Core.ServiceCotract;
using projectAPI.Enums;
using projectAPI.Result;

namespace projectAPI.Services
{
    public class ProductOrderServices : IProductOrderServices
    {
       private readonly IOrderService orderService;
        private readonly AppDbContext context;
        private readonly IShopingCartServices shopingCartServices;
        public ProductOrderServices (IOrderService orderService, 
            AppDbContext context,IShopingCartServices shopingCartServices)
        {
            this.orderService = orderService;
            this.context = context;
            this.shopingCartServices = shopingCartServices;
        }
        public OperationResult Cancel (string userID)
        {
            var order = context.Order.Where(x => x.userId == userID).FirstOrDefault();

            if (order == null)
            {
                return new OperationResult()
                {
                    success = false,
                    statusCode=System.Net.HttpStatusCode.NotFound,
                    message = "Order not found."
                };
            }

            if (order.OrderStatus == Enums.OrderStatus.Cancelled ||
                order.OrderStatus == Enums.OrderStatus.Delivered)
            {
                return new OperationResult()
                {
                    success = false,
                    statusCode = System.Net.HttpStatusCode.BadRequest,
                    message = $"Can't cancel this order. The order is already {order.OrderStatus}."
                };
            }


            order.OrderStatus = Enums.OrderStatus.Cancelled;
            context.Order.Update(order);
            context.SaveChanges();

            return new OperationResult()
            {
                success = true,
                message = "Order cancelled successfully."
            };
        }
        public bool ConfirmOrder(string userId)
        {
            OrderDTO order = orderService.CreateOrder(userId);
            if (order.success == 0)
            {
                return false;
            }

            var products = shopingCartServices.GetProductsFromCart(userId);
            if (products == null || !products.Any())
            {
                return false;
            }

            foreach (var item in products)
            {
                ProductOrder productOrder = new ProductOrder()
                {
                    Count = item.count,
                    OrderId = order.id,
                    Price = item.price,
                    ProductId = item.productId
                };
                context.ProductOrders.Add(productOrder);
            }

            var cartItems = context.ShopingCart.Where(x => x.userId == userId).ToList();
           context.ShopingCart.RemoveRange(cartItems);

            context.SaveChanges();

            return true;
        }

        public double GetTotalPrice(string userId)
        {
            double result = context.ProductOrders
                .Where(x => x.Order.userId == userId)
                .Sum(x => x.Product.price);
            return result;
        }
    }
}
