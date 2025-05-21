using projectAPI.Core.Domain.Entities;
using projectAPI.Core.DTO;
using projectAPI.Core.ServiceCotract;
using projectAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Services
{
    public class OrderServices : IOrderService
    {
        private readonly  AppDbContext context;
        public OrderServices(AppDbContext context)
        {
            this.context = context;
        }
        public OrderDTO? CreateOrder(string userId)
        {
            bool hasActiveOrder = context.Order.Any(x => x.userId == userId &&
                (x.OrderStatus == Enums.OrderStatus.Pending || x.OrderStatus == Enums.OrderStatus.Processing));

            if (hasActiveOrder)
            {
                return new OrderDTO() { success = 0 };
            }

            bool hasItemsInCart = context.ShopingCart.Any(x => x.userId == userId);
            if (!hasItemsInCart)
            {
                return new OrderDTO() { success = 0 };
            }

            Order order = new Order()
            {
                date = DateTime.UtcNow,
                userId = userId,
                OrderStatus = Enums.OrderStatus.Pending
            };

            context.Order.Add(order);
            context.SaveChanges();

            OrderDTO orderDTO = new OrderDTO()
            {
                id = order.id,
                success = 1,
                date = order.date,
                userId = userId
            };

            return orderDTO;
        }

        public void UpdateOrderStatusAsync(string userId, OrderStatus state)
        {
            var order = context.Order.Where(x => x.userId == userId && x.OrderStatus == OrderStatus.Paid).FirstOrDefault();
            order.OrderStatus = state;
            context.Order.Update(order);
            context.SaveChanges();
        }
    }
}
