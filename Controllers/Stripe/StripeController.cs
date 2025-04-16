using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using TrinchPhotosAPI.Helpers;

namespace TrinchPhotosAPI.Controllers.Stripe
{
    [Route("api/stripe/create-checkout")]
    [ApiController]
    public class StripeController : Controller
    {

        [HttpPost]
        public ActionResult Create([FromQuery] List<string> priceIds)
        {
            var domain = "http://trinchphotos.com";
            var lineItems = priceIds.Select(priceId => new SessionLineItemOptions
            {
                Price = priceId,
                Quantity = 1,
            }).ToList();

            var options = new SessionCreateOptions
            {
                UiMode = "embedded",
                SubmitType = "pay",
                BillingAddressCollection = "auto",
                LineItems = lineItems,
                Mode = "payment",
                ReturnUrl = domain + "/shop/checkout/return?session_id={CHECKOUT_SESSION_ID}",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Json(new { clientSecret = session.ClientSecret });
        }

    }

    [Route("api/stripe/session-status")]
    [ApiController]
    public class SessionStatusController : Controller
    {
        [HttpGet]
        public ActionResult SessionStatus([FromQuery] string session_id)
        {
            var sessionService = new SessionService();
            try
            {
                Session session = sessionService.Get(session_id);
                if (session.Status == "complete")
                {
                    string orderid = OrderFulfiller.FulfillStripeOrder(session_id);
                    return Json(new { status = session.Status, customer_email = session.CustomerDetails.Email, order_id = orderid });
                }

                return Json(new { status = session.Status, customer_email = session.CustomerDetails.Email });
            }
            catch (StripeException e)
            {
                return Json(new { status = "error", message = e.Message });
            }
        }
    }

    [Route("api/stripe/get-price")]
    [ApiController]
    public class PriceGetterController : Controller
    {
        [HttpGet]
        public ActionResult PriceGetter([FromQuery] string priceId)
        {
            try
            {
                var service = new PriceService();
                return Json(new { price_id = priceId, price = service.Get(priceId).UnitAmount });
            }
            catch (StripeException e)
            {
                return Json(new { status = "error", message = e.Message });
            }

        }
    }
}
