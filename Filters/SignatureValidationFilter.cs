using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using POC_Fawry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace POC_Fawry.Filters {

    public class SignatureValidationFilter : IAsyncActionFilter {

        private readonly FawryOptions _options;

        public SignatureValidationFilter(IOptions<FawryOptions> options)
        {
            _options = options.Value;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var currentRequest = context.HttpContext.Request;
            // Step 1: Read the request body
            var requestBody = await ReadRequestBodyAsync(context.HttpContext.Request);

            // Step 2: Parse the request body to extract the Signature
            var receivedSignature = currentRequest.Query["messageSignature"].ToString();
            if (string.IsNullOrEmpty(receivedSignature))
            {
                throw new Exception("Signature not found in the request body.");
            }
            // Step 3: Compute the Signature using the secret key and request body
            var computedSignature = ComputeSignature(_options.SecurityKey, requestBody);

            // Step 4: Compare the computed Signature with the received Signature
            if (!string.Equals(computedSignature, receivedSignature, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            // Signature validation successful, proceed to the action
            await next();


        }

        private string ComputeSignature(string securityKey, string requestBody)
        {
            //  Deserialize the request body
            FawryCallbackResponse FawryCallbackData = JsonConvert.DeserializeObject<FawryCallbackResponse>(requestBody);
            // Build the concatenated string in EXACT required order
            var signatureBase = new StringBuilder()
                .Append(FawryCallbackData.FawryRefNumber)
                .Append(FawryCallbackData.MerchantRefNumber)
                .Append(FawryCallbackData.PaymentAmount.ToString("0.00")) // 2 decimal places
                .Append(FawryCallbackData.OrderAmount.ToString("0.00"))   // 2 decimal places
                .Append(FawryCallbackData.OrderStatus.ToString())         // Enum string value
                .Append(FawryCallbackData.PaymentMethod.ToString())       // Enum string value
                .Append(FawryCallbackData.PaymentReferenceNumber ?? string.Empty) // Empty if null
                .Append(securityKey);

            // Compute SHA-256 hash
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(signatureBase.ToString()));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower(); // lowercase hex
            }

        }
       
        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {

            request.EnableBuffering(); // Enable rewinding the request body stream
            request.Body.Position = 0;
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var requestBody = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return requestBody;
            }
        }


    }

}
