using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POC_Fawry.Models{

    public class FawryChargeAuthRequest {
        /// <summary>
        /// The merchant code provided by FawryPay team during account setup
        /// </summary>
        /// <example>TEST12345</example>
        [StringLength(50, ErrorMessage = "Merchant code cannot exceed 50 characters")]
        [JsonPropertyName("merchantCode")]
        public string MerchantCode { get; set; }

        /// <summary>
        /// Unique reference number for the charge request in merchant system
        /// </summary>
        /// <example>123456789</example>
        [JsonPropertyName("merchantRefNum")]
        public long MerchantRefNum { get; set; }

        /// <summary>
        /// Unique customer profile ID in merchant system (optional)
        /// </summary>
        /// <example>1001</example>
        [JsonPropertyName("customerProfileId")]
        public long? CustomerProfileId { get; set; }

        /// <summary>
        /// Payment method to be used
        /// </summary>
        /// <example>CARD</example>
        [Required(ErrorMessage = "Payment method is required")]
        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Card number for payment
        /// </summary>
        /// <example>4111111111111111</example>
        [Required(ErrorMessage = "Card number is required")]
        [CreditCard(ErrorMessage = "Invalid card number")]
        [JsonPropertyName("cardNumber")]
        public string CardNumber { get; set; }

        /// <summary>
        /// Card expiry year (two digits format)
        /// </summary>
        /// <example>25</example>
        [Required(ErrorMessage = "Card expiry year is required")]
        [Range(0, 99, ErrorMessage = "Invalid expiry year")]
        [JsonPropertyName("cardExpiryYear")]
        public int CardExpiryYear { get; set; }

        /// <summary>
        /// Card expiry month (two digits format)
        /// </summary>
        /// <example>12</example>
        [Required(ErrorMessage = "Card expiry month is required")]
        [Range(1, 12, ErrorMessage = "Invalid expiry month")]
        [JsonPropertyName("cardExpiryMonth")]
        public int CardExpiryMonth { get; set; }

        /// <summary>
        /// Card security code
        /// </summary>
        /// <example>123</example>
        [Required(ErrorMessage = "CVV is required")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "Invalid CVV length")]
        [JsonPropertyName("cvv")]
        public string Cvv { get; set; }

        /// <summary>
        /// Customer name (optional)
        /// </summary>
        /// <example>John Doe</example>
        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer mobile number
        /// </summary>
        /// <example>01234567890</example>
        [Required(ErrorMessage = "Customer mobile is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [JsonPropertyName("customerMobile")]
        public string CustomerMobile { get; set; }

        /// <summary>
        /// Customer email address
        /// </summary>
        /// <example>test@example.com</example>
        [Required(ErrorMessage = "Customer email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [JsonPropertyName("customerEmail")]
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Charge amount (format: xx.xx)
        /// </summary>
        /// <example>100.50</example>
        [Required(ErrorMessage = "Amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Invalid amount")]
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "CurrencyCode is required")]
        [JsonPropertyName("currencyCode")]
        public string CurrencyCode {  get; set; }
        /// <summary>
        /// Transaction description
        /// </summary>
        /// <example>Order #12345</example>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Language for notifications
        /// </summary>
        /// <example>en-gb</example>
        [Required(ErrorMessage = "Language is required")]
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// List of charge items
        /// </summary>
        [Required(ErrorMessage = "Charge items are required")]
        [MinLength(1, ErrorMessage = "At least one charge item is required")]
        [JsonPropertyName("chargeItems")]
        public List<FawryChargeItem> ChargeItems { get; set; }

        /// <summary>
        /// Authentication capture mode
        /// </summary>
        /// <example>true</example>
        [Required(ErrorMessage = "Auth capture mode is required")]
        [JsonPropertyName("authCaptureModePayment")]
        public bool AuthCaptureModePayment { get; set; }

        /// <summary>
        /// SHA-256 signature for request validation
        /// </summary>
        /// <example>a1b2c3d4e5f6g7h8i9j0...</example>
        [StringLength(64, MinimumLength = 64, ErrorMessage = "Invalid signature length")]
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

    }

}
