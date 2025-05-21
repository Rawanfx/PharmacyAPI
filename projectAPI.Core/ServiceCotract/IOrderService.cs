using projectAPI.Core.DTO;
using projectAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Core.ServiceCotract
{
    public interface IOrderService
    {
        OrderDTO CreateOrder(string userId);
        void UpdateOrderStatusAsync(string userId, OrderStatus status);
    }
}
