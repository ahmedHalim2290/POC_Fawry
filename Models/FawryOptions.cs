using System.Text.Json.Serialization;

namespace POC_Fawry.Models {
    public class FawryOptions {
        public const string Fawry = "Fawry";
        public string BaseUrl { get; set; }
        public string MerchantCode { get; set; }
        public string SecurityKey { get; set; }
        public string MerchantRefNumber { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))] // For proper JSON serialization
    public enum FawryPaymentMethod {
        /// <summary>
        /// Cash on Delivery payment method
        /// </summary>
        [JsonPropertyName("CashOnDelivery")]
        CashOnDelivery,

        /// <summary>
        /// Pay at Fawry branches/payment points
        /// </summary>
        [JsonPropertyName("PayAtFawry")]
        PayAtFawry,

        /// <summary>
        /// Mobile wallet payment
        /// </summary>
        [JsonPropertyName("MWALLET")]
        MWallet,

        /// <summary>
        /// Credit/Debit card payment
        /// </summary>
        [JsonPropertyName("CARD")]
        Card,

        /// <summary>
        /// Payment through VALU (installment service)
        /// </summary>
        [JsonPropertyName("VALU")]
        Valu
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FawryOrderStatus {
        /// <summary>
        /// Order was created but not yet paid
        /// </summary>
        [JsonPropertyName("CREATED")]
        Created,

        /// <summary>
        /// Payment was successfully completed
        /// </summary>
        [JsonPropertyName("PAID")]
        Paid,

        /// <summary>
        /// Order was cancelled
        /// </summary>
        [JsonPropertyName("CANCELLED")]
        Cancelled,

        /// <summary>
        /// Order expired before payment was completed
        /// </summary>
        [JsonPropertyName("EXPIRED")]
        Expired,

        /// <summary>
        /// Order has been shipped to customer
        /// </summary>
        [JsonPropertyName("SHIPPED")]
        Shipped,

        /// <summary>
        /// Order was delivered to customer
        /// </summary>
        [JsonPropertyName("DELIVERED")]
        Delivered
    }
}
