using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POC_Fawry.Models {
    public class FawryCaptureRequest {
        /// <summary>
        /// The merchant code provided by FawryPay during account setup
        /// </summary>
        /// <example>TEST12345</example>
        [StringLength(50, ErrorMessage = "Merchant code cannot exceed 50 characters")]
        [JsonPropertyName("merchantCode")]
        public string MerchantCode { get; set; }

        /// <summary>
        /// Unique reference number received during payment authorization
        /// </summary>
        /// <example>123456789</example>
        [JsonPropertyName("merchantRefNum")]
        public long MerchantRefNum { get; set; }

        /// <summary>
        /// Partial amount to capture (if not provided, captures full amount)
        /// </summary>
        /// <example>100.50</example>
        [Range(0.01, double.MaxValue, ErrorMessage = "Capture amount must be positive")]
        [JsonPropertyName("captureAmount")]
        public double? CaptureAmount { get; set; }

        /// <summary>
        /// SHA-256 signature for request validation
        /// </summary>
        /// <example>a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6q7r8s9t0</example>
        [StringLength(64, MinimumLength = 64, ErrorMessage = "Signature must be 64 characters")]
        [JsonPropertyName("requestSignature")]
        public string RequestSignature { get; set; }

    }
}
