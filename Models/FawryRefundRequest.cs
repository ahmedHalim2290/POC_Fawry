using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace POC_Fawry.Models {
    public class FawryRefundRequest {
        /// <summary>
        /// The merchant code provided during account setup.
        /// </summary>
        [JsonProperty("merchantCode")]
        public string MerchantCode { get; set; }

        /// <summary>
        /// Fawry order reference number.
        /// </summary>
        [JsonProperty("referenceNumber")]
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// The required amount to be refunded (in decimal format with two decimal places).
        /// </summary>
        [JsonProperty("refundAmount")]
        [Required(ErrorMessage = "Refund amount is required")]
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// The reason for the refund (optional).
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// The SHA-256 digest for the concatenated string of:
        /// merchantCode + referenceNumber + refund amount in two decimal format (10.00) + 
        /// refund reason (if exists) + secureKey
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
