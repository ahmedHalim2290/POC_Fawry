using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POC_Fawry.Models{
    public class FawryChargeAuthResponse {
        /// <summary>
        /// Type of response
        /// </summary>
        /// <example>ChargeResponse</example>
        [JsonPropertyName("type")]
        public string Type { get; set; } = "ChargeResponse";

        /// <summary>
        /// FawryPay issued transaction reference number
        /// </summary>
        /// <example>963455678</example>
        [JsonPropertyName("referenceNumber")]
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Merchant issued transaction reference number
        /// </summary>
        /// <example>9990d0642040</example>
        [JsonPropertyName("merchantRefNumber")]
        public string MerchantRefNumber { get; set; }

        /// <summary>
        /// Order amount in two decimal places format
        /// </summary>
        /// <example>20.00</example>
        [JsonPropertyName("orderAmount")]
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// The paid amount in two decimal places format
        /// </summary>
        /// <example>20.00</example>
        [JsonPropertyName("paymentAmount")]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// The payment processing fees
        /// </summary>
        /// <example>1.00</example>
        [JsonPropertyName("fawryFees")]
        public decimal FawryFees { get; set; }

        /// <summary>
        /// Payment Method Selected by your client
        /// </summary>
        /// <example>CARD</example>
        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Order Status
        /// </summary>
        /// <example>PAID</example>
        [JsonPropertyName("orderStatus")]
        public string OrderStatus { get; set; }

        /// <summary>
        /// Timestamp of when payment was processed
        /// </summary>
        /// <example>1607879720568</example>
        [JsonPropertyName("paymentTime")]
        public long PaymentTime { get; set; }

        /// <summary>
        /// Customer Mobile Number
        /// </summary>
        /// <example>01234567891</example>
        [JsonPropertyName("customerMobile")]
        public string CustomerMobile { get; set; }

        /// <summary>
        /// Customer Email address
        /// </summary>
        /// <example>example@email.com</example>
        [JsonPropertyName("customerMail")]
        public string CustomerMail { get; set; }

        /// <summary>
        /// Customer Profile ID in merchant's system
        /// </summary>
        /// <example>1212</example>
        [JsonPropertyName("customerProfileId")]
        public string CustomerProfileId { get; set; }

        /// <summary>
        /// Response Signature (SHA-256 hash)
        /// </summary>
        /// <example>2df2943c6704176809ba6d559e2906b3d4df14916d6</example>
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        /// <summary>
        /// Response status code
        /// </summary>
        /// <example>200</example>
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        /// <summary>
        /// Response status description
        /// </summary>
        /// <example>Operation done successfully</example>
        [JsonPropertyName("statusDescription")]
        public string StatusDescription { get; set; }

    }
}
