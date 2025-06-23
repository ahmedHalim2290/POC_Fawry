using Microsoft.AspNetCore.Mvc;
using POC_Fawry.Models;
using POC_Fawry.Services;

namespace POC_Fawry.Controllers {
    public class HomeController : Controller {
        private readonly FawryService _fawryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(FawryService fawryService, IHttpContextAccessor httpContextAccessor)
        {
            _fawryService = fawryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        /// <summary>
        /// To Create New Order the with initPayment in api
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ConfirmOrder()
        {
            FawryChargeRequest request = new FawryChargeRequest()
            {
                CustomerMobile = "01xxxxxxxxx",
                CustomerEmail = "email@domain.com",
                CustomerName = "Customer Name",
                CustomerProfileId = 1212,
                PaymentExpiry = 1631138400000,
                Language = "en-gb",
                ChargeItems = new List<FawryChargeItem>()
                            {
                                 new FawryChargeItem(){
                                        ItemId= "6b5fdea340e31b3b0339d4d4ae5",
                                        Description= "Product Description",
                                        Price= (decimal)50.00,
                                        Quantity= 2,
                                        ImageUrl= "https=//developer.fawrystaging.com/photos/45566.jpg",
                                    },
                                    new FawryChargeItem(){
                                        ItemId= "97092dd9e9c07888c7eef36",
                                        Description= "Product Description",
                                        Price=(decimal) 75.25,
                                        Quantity= 3,
                                        ImageUrl= "https=//developer.fawrystaging.com/photos/639855.jpg",
                                    }
                            },
                PaymentMethod = FawryPaymentMethod.PayAtFawry,
                ReturnUrl = Url.Action("RedirectOrder"),
                AuthCaptureModePayment = false
            };

            var data = await _fawryService.FawryChargeAsync(request);
            return View(data);
        }

        public async Task<IActionResult> AuthorizePayment()
        {
            FawryChargeAuthRequest request = new FawryChargeAuthRequest()
            {
                CustomerProfileId = 777777,
                CustomerName = "customer name",
                CustomerMobile = "01234567891",
                CustomerEmail = "example@gmail.com",
                CardNumber = "4242424242424242",
                CardExpiryYear = 21,
                CardExpiryMonth = 05,
                Cvv = "123",
                Amount = (decimal)580.55,
                CurrencyCode = "EGP",
                Language = "en-gb",
                ChargeItems = new List<FawryChargeItem>() {
                    new FawryChargeItem() {
                                   ItemId =  "897fa8e81be26df25db592e81c31c",
                                   Description =  "Item Description",
                                   Price = (decimal) 580.55
                                 }},
                AuthCaptureModePayment = true,
                PaymentMethod = FawryPaymentMethod.Card.ToString(),
                Description = "example description"
            };
            var data = await _fawryService.FawryAuthChargeAsync(request);
            return View(data);
        }

        public async Task<IActionResult> CapturePayment()
        {
            FawryCaptureRequest request = new FawryCaptureRequest();

            var data = await _fawryService.FawryCaptureAsync(request);
            return View(data);
        }

        public async Task<IActionResult> CancelOrder()
        {
            FawryCancelRequest request = new FawryCancelRequest();
            request.CancelAmount = (decimal)362.50;
            request.Reason = "Item not as described";
            var data = await _fawryService.CancelTransactionAsync(request);
            return View(data);
        }

        public async Task<IActionResult> CancelAuthOrder()
        {
            FawryAuthCancelRequest request = new FawryAuthCancelRequest();

            var data = await _fawryService.CancelAuthTransactionAsync(request);
            return View(data);
        }

        public async Task<IActionResult> RefundOrder()
        {
            FawryRefundRequest request = new FawryRefundRequest();
            request.RefundAmount = (decimal)362.50;
            request.Reason = "Item not as described";
            var data = await _fawryService.RefundTransactionAsync(request);
            return View(data);
        }

        public async Task<IActionResult> GetPaymentStatus()
        {
            var data = await _fawryService.GetPaymentStatus();

            return View(data);
        }

        public async Task<IActionResult> RedirectOrder()

        {
            var request = HttpContext.Request;
            var result = new FawryChargeResponse
            {
                Type = request.Query.TryGetValue("type", out var type) ? type.ToString() : "ChargeResponse",
                ReferenceNumber = request.Query.TryGetValue("referenceNumber", out var refNum) ? refNum.ToString() : string.Empty,
                MerchantRefNumber = request.Query.TryGetValue("merchantRefNumber", out var merchantRef) ? merchantRef.ToString() : string.Empty,
                OrderAmount = decimal.TryParse(request.Query["orderAmount"], out var orderAmt) ? orderAmt : 0,
                PaymentAmount = decimal.TryParse(request.Query["paymentAmount"], out var paymentAmt) ? paymentAmt : 0,
                FawryFees = decimal.TryParse(request.Query["fawryFees"], out var fees) ? fees : 0,
                PaymentMethod = request.Query.TryGetValue("paymentMethod", out var method) ? Enum.TryParse<FawryPaymentMethod>(method.ToString(), true, out var paymentMethod)
        ? paymentMethod
        : throw new ArgumentException($"Invalid payment method: {method}")
    : throw new ArgumentException("Payment method is required"),
                OrderStatus = request.Query.TryGetValue("orderStatus", out var status) ? Enum.TryParse<FawryOrderStatus>(status.ToString(), true, out var orderStatus)
        ? orderStatus
        : throw new ArgumentException($"Invalid order status: {status}")
    : throw new ArgumentException("Order status is required"),
                PaymentTime = long.TryParse(request.Query["paymentTime"], out var time) ? time : 0,
                CustomerMobile = request.Query.TryGetValue("customerMobile", out var mobile) ? mobile.ToString() : string.Empty,
                CustomerMail = request.Query.TryGetValue("customerMail", out var mail) ? mail.ToString() : string.Empty,
                CustomerProfileId = request.Query.TryGetValue("customerProfileId", out var profileId) ? profileId.ToString() : string.Empty,
                Signature = request.Query.TryGetValue("signature", out var signature) ? signature.ToString() : string.Empty,
                StatusCode = request.Query.TryGetValue("statusCode", out var statusCode) ? statusCode.ToString() : string.Empty,
                StatusDescription = request.Query.TryGetValue("statusDescription", out var desc) ? desc.ToString() : string.Empty
            };

            return View(result);

        }
    }
}

