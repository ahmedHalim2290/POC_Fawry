using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using POC_Fawry.Models;

namespace POC_Fawry.Services {
    public class FawryService : IPayment {
        private readonly FawryHttpService _httpClient;
        private readonly FawryOptions _config;
        public FawryService(FawryHttpService httpClient, IOptions<FawryOptions> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
        }

        public async Task<FawryChargeResponse> FawryChargeAsync(FawryChargeRequest request)
        {
            // 1. Start building the base string
            var signatureBase = new StringBuilder()
                .Append(_config.MerchantCode)
                .Append(_config.MerchantRefNumber)
                .Append(request.CustomerProfileId != null ? request.CustomerProfileId.Value : string.Empty)
                .Append(request.ReturnUrl);

            // 2. Handle items (sorted by itemId)
            foreach (var item in request.ChargeItems.OrderBy(i => i.ItemId))
            {
                signatureBase
                    .Append(item.ItemId)
                    .Append(item.Quantity)
                    .Append(item.Price.ToString("0.00")); // Ensure 2 decimal places
            }

            // 3. Append the secure key
            signatureBase.Append(_config.SecurityKey);

            // 4. Compute SHA-256 hash
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(signatureBase.ToString()));
                request.Signature = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
            request.MerchantCode = _config.MerchantCode;
            request.MerchantRefNum = _config.MerchantRefNumber;
            return await _httpClient.PostAsync<FawryChargeRequest, FawryChargeResponse>("fawrypay-api/api/payments/init", request);
        }

        public async Task<FawryChargeAuthResponse> FawryAuthChargeAsync(FawryChargeAuthRequest request)
        {
            request.MerchantCode = _config.MerchantCode;
            request.MerchantRefNum = long.Parse(_config.MerchantRefNumber);
            var signatureData = $"{request.MerchantCode}{request.MerchantRefNum}" +
                             $"{request.CustomerProfileId.ToString() ?? string.Empty}{request.PaymentMethod}" +
                             $"{request.Amount.ToString("0.00")}{request.CardNumber}" +
                             $"{request.CardExpiryYear:D2}{request.CardExpiryMonth:D2}{request.Cvv}{_config.SecurityKey}";

            using var sha256 = System.Security.Cryptography.SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(signatureData));
            request.Signature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return await _httpClient.PostAsync<FawryChargeAuthRequest, FawryChargeAuthResponse>("ECommerceWeb/Fawry/payments/charge", request);
        }

        public async Task<FawryCaptureResponse> FawryCaptureAsync(FawryCaptureRequest request)
        {
            request.MerchantCode = _config.MerchantCode;
            request.MerchantRefNum = long.Parse(_config.MerchantRefNumber);
            var captureAmountStr = request.CaptureAmount.HasValue
                 ? request.CaptureAmount.Value.ToString("0.00")
                 : string.Empty;

            var signatureData = $"{request.MerchantRefNum}" +
                              $"{captureAmountStr}" +
                              $"{request.MerchantCode}" +
                              $"{_config.SecurityKey}";

            using var sha256 = System.Security.Cryptography.SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(signatureData));
            request.RequestSignature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return await _httpClient.PostAsync<FawryCaptureRequest, FawryCaptureResponse>("ECommerceWeb/api/payment/capture", request);
        }

        public async Task<FawryCancelResponse> CancelTransactionAsync(FawryCancelRequest request)
        {
            // 1. Concatenate fields in EXACT order
            var signatureBase = new StringBuilder()
                .Append(_config.MerchantCode)
                .Append(_config.MerchantRefNumber)
                .Append(request.CancelAmount.ToString("0.00")) // Force 2 decimal places
                .Append(request.Reason ?? string.Empty)  // Empty if reason is null
                .Append(_config.SecurityKey);

            // 2. Compute SHA-256 hash
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(signatureBase.ToString()));
                request.Signature = BitConverter.ToString(bytes).Replace("-", "").ToLower(); // Lowercase hex
            }
            request.MerchantCode = _config.MerchantCode;
            request.ReferenceNumber = _config.MerchantRefNumber;
            return await _httpClient.PostAsync<FawryCancelRequest, FawryCancelResponse>("ECommerceWeb/api/orders/cancel-unpaid-order", request);
        }

        public async Task<FawryAuthCancelResponse> CancelAuthTransactionAsync(FawryAuthCancelRequest request)
        {
            request.MerchantCode = _config.MerchantCode;
            request.MerchantRefNum = long.Parse(_config.MerchantRefNumber);
            var rawString = $"{(request.MerchantRefNum).ToString()}{request.MerchantCode}{_config.SecurityKey}";

            using var sha256 = System.Security.Cryptography.SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawString));
            request.RequestSignature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return await _httpClient.PostAsync<FawryAuthCancelRequest, FawryAuthCancelResponse>("ECommerceWeb/api/orders/cancel-unpaid-order", request);
        }

        public async Task<FawryRefundResponse> RefundTransactionAsync(FawryRefundRequest request)
        {
            // 1. Concatenate fields in EXACT order
            var signatureBase = new StringBuilder()
                .Append(_config.MerchantCode)
                .Append(_config.MerchantRefNumber)
                .Append(request.RefundAmount.ToString("0.00")) // Force 2 decimal places
                .Append(request.Reason ?? string.Empty)  // Empty if reason is null
                .Append(_config.SecurityKey);

            // 2. Compute SHA-256 hash
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(signatureBase.ToString()));
                request.Signature = BitConverter.ToString(bytes).Replace("-", "").ToLower(); // Lowercase hex
            }
            request.MerchantCode = _config.MerchantCode;
            request.ReferenceNumber = _config.MerchantRefNumber;
            return await _httpClient.PostAsync<FawryRefundRequest, FawryRefundResponse>("ECommerceWeb/Fawry/payments/refund", request);
        }

        public async Task<PaymentStatusResponse> GetPaymentStatus()
        {
            // 1. Concatenate the required values in exact order
            var signatureString = $"{_config.MerchantCode}{_config.MerchantRefNumber}{_config.SecurityKey}";

            // 2. Compute SHA-256 hash
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(signatureString));
                signatureString = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
            PaymentStatusRequest request = new PaymentStatusRequest()
            {
                MerchantCode = _config.MerchantCode,
                MerchantRefNumber = _config.MerchantRefNumber,
                Signature = signatureString

            };
            return await _httpClient.GetAsync<PaymentStatusRequest, PaymentStatusResponse>("ECommerceWeb/Fawry/payments/status/v2", request);
        }

        public async Task<string> FawryCallbackAsync(FawryCallbackResponse request)
        {
            //TODO: here we adding our code to save the response coming form fawry in our db
            return "";
        }
    }
}