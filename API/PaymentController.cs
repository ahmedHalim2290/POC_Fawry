using Microsoft.AspNetCore.Mvc;
using POC_Fawry.Services;
using POC_Fawry.Models;
using Newtonsoft.Json;
using POC_Fawry.Filters;

namespace POC_Fawry.API {
    /// <summary>
    /// Controller responsible for handling payment-related operations such as transactions, callbacks, and refunds.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase {
        private readonly IPayment _fawryService;

        /// <summary>
        /// Initializes a new instance of the <seecref="PaymentController"/> class.
        /// </summary>
        /// <paramname="fawryService">The service used for interacting with Fawry's API.</param>
        public PaymentController(IPayment fawryService)
        {
            _fawryService = fawryService;
        }


        /// <summary>
        /// Handles the callback from Fawry after a payment is processed.
        /// </summary>
        /// <paramname="response">The payment response object from Fawry.</param>
        /// <returns>The processed payment response.</returns>
        [HttpPost("payment-callback")]
        [ServiceFilter(typeof(SignatureValidationFilter))] // Apply the Signature filter
        public async Task<IActionResult> FawryCallback([FromBody] object response)
        {
            try
            {
                FawryCallbackResponse createdOrder = JsonConvert.DeserializeObject<FawryCallbackResponse>(response.ToString());
                var result = await _fawryService.FawryCallbackAsync(createdOrder);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("initpayment")]
        public async Task<IActionResult> InitPayment([FromBody] FawryChargeRequest request)
        {

            return Ok(await _fawryService.FawryChargeAsync(request));

        }

        /// <summary>
        /// This API can be used to authorize client payment using his card information. This authorization request shall place a hold on the client's card with the charged amount until you capture the payment later.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("authorizepayment")]
        public async Task<IActionResult> AuthorizePayment([FromBody] FawryChargeAuthRequest request)
        {

            var data = await _fawryService.FawryAuthChargeAsync(request);
            return Ok(data);
        }

        /// <summary>
        /// This API can be used to capture a payment for which you have created a payment authorization. To capture an authorized payment, include the authorization ID (merchantRefNum) you received with your payment authorization request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("capturepayment")]
        public async Task<IActionResult> CapturePayment([FromBody] FawryCaptureRequest request)
        {
            var data = await _fawryService.FawryCaptureAsync(request);
            return Ok(data);
        }

        /// <summary>
        /// This API can be used to cancel a payment authorization. To cancel an authorized payment, include the authorization ID (merchantRefNum) you received with your payment authorization request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("cancelauthorder")]
        public async Task<IActionResult> CancelAuthOrder([FromBody] FawryAuthCancelRequest request)
        {

            return Ok(await _fawryService.CancelAuthTransactionAsync(request));

        }

        /// <summary>
        /// Depending on the status of your payment, FawryPay allows you to cancel payment that has been submitted by your customer. As long as the status of your payment is still unpaid, you can easily cancel the payment this API. A notification will be sent to customer via his/her mobile number if it exists with the payment information. Meanwhile, if the payment is already paid, you will need to use our Refund API to refund your customer.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("cancelorder")]
        public async Task<IActionResult> CancelOrder([FromBody] FawryCancelRequest request)
        {
            return Ok(await _fawryService.CancelTransactionAsync(request));

        }

        /// <summary>
        /// FawryPay allows you to refund amounts that have previously been paid by customers, returning a unique reference for this request. Refunding can be done on the full captured amount or a partial amount. Payments which have been authorised, but not captured, cannot be refunded.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("refundorder")]
        public async Task<IActionResult> RefundOrder([FromBody] FawryRefundRequest request)
        {
            return Ok(await _fawryService.RefundTransactionAsync(request));

        }

        /// <summary>
        /// FawryPay delivers the Get Payment Status API as a responsive API for merchants who wishes to pull the status of their transactions whenever needed. Our valued merchants can use Get Payment Status API to retrieve the payment status for the charge request, usingGET method.
        /// </summary>
        /// <param name="merchantCode"></param>
        /// <param name="merchantRefNumber"></param>
        /// <returns></returns>
        [HttpGet("GetPaymentStatus")]
        public async Task<IActionResult> GetPaymentStatus()
        {
            var data = await _fawryService.GetPaymentStatus();

            return Ok(data);
        }
    }
}