using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;

namespace projectAPI.Core.ServiceCotract
{
    public interface IPaymentSevices
    {
        PaymentIntent CreatePaymentIntent(string userId);
    }
}
