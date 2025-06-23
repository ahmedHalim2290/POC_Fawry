using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace POC_Fawry.Models {
    public class FawryCancelRequest {
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
        /// The required amount to be Canceled (in decimal format with two decimal places).
        /// </summary>
        [JsonProperty("CancelAmount")]
        [Required(ErrorMessage = "Cancel amount is required")]
        public decimal CancelAmount { get; set; }

        /// <summary>
        /// The reason for the Cancel (optional).
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// The SHA-256 digest for the concatenated string of:
        /// merchantCode + referenceNumber + Cancel amount in two decimal format (10.00) + 
        /// Cancel reason (if exists) + secureKey
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
