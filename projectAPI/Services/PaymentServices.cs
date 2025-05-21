using Google.Apis.Calendar.v3.Data;
using Microsoft.Extensions.Configuration;
using projectAPI.Core.ServiceCotract;
using projectAPI.Enums;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Services
{
    public class PaymentServices : IPaymentSevices
    {
        private readonly IConfiguration _configuration;
        private readonly IProductOrderServices productOrderServices;
        private readonly AppDbContext context;
        private readonly IOrderService orderService;
        public PaymentServices(IConfiguration configuration,
            IProductOrderServices productOrderServices,AppDbContext context,IOrderService orderService)
        {
            _configuration = configuration;
            this.productOrderServices = productOrderServices;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            this.context = context;
            this.orderService = orderService;
        }

        public PaymentIntent CreatePaymentIntent(string userId)
        {
            var order = context.ProductOrders.Where(x => x.Order.userId == userId && (x.Order.OrderStatus == Enums.OrderStatus.Pending));
            if (!order.Any())
                return null;
            double amount = productOrderServices.GetTotalPrice(userId);
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)amount,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
            };

            var service = new PaymentIntentService();
            return service.Create(options);
        }
        public async Task HandleWebhookAsync(string json, string stripeSignature, string webhookSecret,string userId)
        {
            var stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, webhookSecret);

            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                orderService. UpdateOrderStatusAsync(userId, OrderStatus.Paid);
            }
            else if (stripeEvent.Type == "payment_intent.payment_failed")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                orderService. UpdateOrderStatusAsync(userId, OrderStatus.Failed);
            }
        }

    }
}
