using projectAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Core.ServiceCotract
{
    public interface IProductOrderServices
    {
        bool ConfirmOrder(string userId);
        OperationResult Cancel(string userId);
        double GetTotalPrice(string userId);
    }
}
