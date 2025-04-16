using Stripe.Checkout;
using TrinchPhotosAPI.Data;

namespace TrinchPhotosAPI.Helpers
{
    public class OrderFulfiller
    {

        public static string FulfillStripeOrder(string sessionId)
        {
            var options = new SessionGetOptions
            {
                Expand = new List<string> { "line_items" },
            };
            var service = new SessionService();
            var checkoutSession = service.Get(sessionId, options);

            if (checkoutSession.Status != "complete")
            {
                return null; // payment not processed
            }

            using var db = new DatabaseContext();
            var order = db.Orders.FirstOrDefault(o => o.StripeSessionId == sessionId);

            if (order == null)
            {
                db.Orders.Add(new Database.Models.Orders
                {
                    OrderId = Guid.NewGuid(),
                    StripeSessionId = sessionId,
                    CustomerName = checkoutSession.CustomerDetails.Email.Split("@")[0], // best way to do it i think
                    CustomerEmail = checkoutSession.CustomerDetails.Email,
                    IsFulfilled = false,
                    FulfilledBy = "None",
                    FulfillComment = "Order created Stripe automation",
                    WebViews = 0,
                    Content = new Dictionary<string, string>()
                });
                db.SaveChanges();
            }

            order = db.Orders.FirstOrDefault(o => o.StripeSessionId == sessionId); // not null anymore

            checkoutSession.LineItems.Data.ForEach(item =>
            {
                var product = db.Products.FirstOrDefault(p => p.StripeProductId == item.Price.ProductId);
                if (product != null && !order.Content.ContainsKey(product.Name))
                {
                    order.Content.Add(product.Name, product.ProductImageUrl);
                }
            });

            order.IsFulfilled = true;
            order.FulfilledBy = "System";
            order.FulfillComment = "Order fulfilled by automation at " + DateTime.UtcNow;
            db.SaveChanges();
            return order.OrderId.ToString();
        }
    }
}
