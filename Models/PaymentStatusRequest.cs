using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POC_Fawry.Models {
    public class PaymentStatusRequest {

        /// <summary>
        /// The merchant code provided by FawryPay team during the account setup
        /// </summary>
        /// <example>is0N+YQzlE4==</example>
        [Required(ErrorMessage = "Merchant code is required")]
        [JsonPropertyName("merchantCode")]
        public string MerchantCode { get; set; }

        /// <summary>
        /// The merchant reference number for the required order
        /// </summary>
        /// <example>9990076204</example>
        [Required(ErrorMessage = "Merchant reference number is required")]
        [StringLength(50, ErrorMessage = "Reference number cannot exceed 50 characters")]
        [JsonPropertyName("merchantRefNumber")]
        public string MerchantRefNumber { get; set; }

        /// <summary>
        /// The request signature containing SHA-256 digest of:
        /// merchantCode + merchantRefNumber + secureKey
        /// </summary>
        /// <example>a5701a2e1e865bf863f0c781829f709ea...</example>
        [Required(ErrorMessage = "Signature is required")]
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}
