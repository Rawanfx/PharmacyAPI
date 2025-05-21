using Microsoft.OpenApi.Services;
using projectAPI.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Core.ServiceCotract
{
    public interface IRateServices
    {
        OperationResult AddRate(string userId, double rate, int productId, string? comment);

        OperationResult delete(string userID, int productID);
    }
}
