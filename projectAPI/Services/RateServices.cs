using projectAPI.Core.Domain.Entities;
using projectAPI.Core.ServiceCotract;
using projectAPI.Result;
using System.Data;
using System.Net;


namespace projectAPI.Services
{
    public class RateServices : IRateServices
    {
        private readonly AppDbContext context;
        public RateServices (AppDbContext context)
        {
            this.context = context;
        }
      public  OperationResult AddRate(string userId, double rate,  int productId, string? comment)
        {
            var order = context.Order.Where(x => x.userId == userId)
                
                .FirstOrDefault();
            if (order ==null)
            {
                return new OperationResult() { success=false,message="No items in shoping cart."};
            }
            var productOrder = context.ProductOrders
                                      .Where(x => x.Order.userId == userId && x.ProductId == productId);
            if (productOrder==null)
            {
                return new OperationResult() { success=false,message="items not found!"};
            }
            try {
                var Review = context.Review.Where(x => x.productId == productId && x.userId == userId).FirstOrDefault();
                if (Review!=null && order.OrderStatus!=Enums.OrderStatus.Cancelled)
                {
                    Review.rate = rate;
                    context.Review.Update(Review);
                    context.SaveChanges();
                    return new OperationResult() { message = "Your commint has been updated.", success = true };
                }
                Review review = new Review()
                {
                    create = DateTime.UtcNow,
                    productId = productId,
                    rate = rate,
                    userId = userId,
                    comment=comment
                };
                context.Review.Add(review);
                context.SaveChanges();
                return new OperationResult() { message = "Thanks! Your comment has been added.", success = true };

            }
            catch {
                return new OperationResult() { message = "An error occurred while submitting your comment. Please try again later.",success = false };
            }
        }

        public OperationResult delete(string userID, int productID)
        {
            var result = context.Review.FirstOrDefault(x => x.userId == userID && x.productId == productID);
            if (result == null)
            {
                return new OperationResult() { message = "This comment doesn’t exist or was already deleted.", success = false };
            }
            context.Review.Remove(result);
            context.SaveChanges();
            return new OperationResult() { success = true,message= "You comment has successfully delet" };
        }
    }
}
