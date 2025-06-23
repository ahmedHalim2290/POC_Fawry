using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POC_Fawry.Models {
    public class FawryAuthCancelRequest {

        /// <summary>
        /// The merchant code provided during FawryPay account setup
        /// </summary>
        /// <example>TEST12345</example>
        [StringLength(50, ErrorMessage = "Merchant code cannot exceed 50 characters")]
        [JsonPropertyName("merchantCode")]
        public string MerchantCode { get; set; }

        /// <summary>
        /// Unique reference number identifying the charge request in merchant's system
        /// </summary>
        /// <example>123456789</example>
        [Range(1, long.MaxValue, ErrorMessage = "Reference number must be positive")]
        [JsonPropertyName("merchantRefNum")]
        public long MerchantRefNum { get; set; }

        /// <summary>
        /// Secure hash generated using SHA-256 algorithm from concatenated string: 
        /// "merchantRefNum + merchantCode + secureKey"
        /// </summary>
        /// <example>a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6q7r8s9t0u1v2w3x4y5z6a7b8c9d0e1f</example>
        [StringLength(64, MinimumLength = 64, ErrorMessage = "Signature must be 64 characters")]
        [JsonPropertyName("requestSignature")]
        public string RequestSignature { get; set; }
    }
}
