using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POC_Fawry.Models{

    public class FawryChargeRequest {

        /// <summary>
        /// The merchant code provided by FawryPay
        /// </summary>
        /// <example>TEST12345</example>
        [StringLength(50, ErrorMessage = "Merchant code cannot exceed 50 characters")]
        [JsonPropertyName("merchantCode")]
        public string MerchantCode { get; set; }

        /// <summary>
        /// The merchant's transaction reference number
        /// </summary>
        /// <example>ORDER12345</example>
        [StringLength(50, ErrorMessage = "Reference number cannot exceed 50 characters")]
        [JsonPropertyName("merchantRefNum")]
        public string MerchantRefNum { get; set; }

        /// <summary>
        /// Customer mobile number (optional)
        /// </summary>
        /// <example>01234567890</example>
        [Phone(ErrorMessage = "Invalid phone number format")]
        [JsonPropertyName("customerMobile")]
        public string CustomerMobile { get; set; }

        /// <summary>
        /// Customer email address (optional)
        /// </summary>
        /// <example>customer@example.com</example>
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        [JsonPropertyName("customerEmail")]
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Customer name (optional)
        /// </summary>
        /// <example>John Doe</example>
        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Unique customer profile ID (optional)
        /// </summary>
        /// <example>12345</example>
        [JsonPropertyName("customerProfileId")]
        public int? CustomerProfileId { get; set; }

        /// <summary>
        /// Language for notifications
        /// </summary>
        /// <example>en-gb</example>
        [Required(ErrorMessage = "Language is required")]
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// Payment expiry timestamp in milliseconds (optional)
        /// </summary>
        /// <example>1631138400000</example>
        [JsonPropertyName("paymentExpiry")]
        public long? PaymentExpiry { get; set; }

        /// <summary>
        /// List of charge items
        /// </summary>
        [Required(ErrorMessage = "At least one charge item is required")]
        [MinLength(1, ErrorMessage = "At least one charge item is required")]
        [JsonPropertyName("chargeItems")]
        public List<FawryChargeItem> ChargeItems { get; set; } = new List<FawryChargeItem>();

        /// <summary>
        /// Selected shipping address (optional)
        /// </summary>
        [JsonPropertyName("selectedShippingAddress")]
        public ShippingAddress SelectedShippingAddress { get; set; }

        /// <summary>
        /// Payment method (optional)
        /// </summary>
        /// <example>CARD</example>
        [JsonPropertyName("paymentMethod")]
        public FawryPaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// Return URL after payment
        /// </summary>
        /// <example>https://yourdomain.com/return</example>
        [Required(ErrorMessage = "Return URL is required")]
        [Url(ErrorMessage = "Invalid URL format")]
        [JsonPropertyName("returnUrl")]
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Webhook URL for notifications (optional)
        /// </summary>
        /// <example>https://yourdomain.com/webhook</example>
        [Url(ErrorMessage = "Invalid URL format")]
        [JsonPropertyName("orderWebHookUrl")]
        public string OrderWebHookUrl { get; set; }

        /// <summary>
        /// Save card info for future payments (optional)
        /// </summary>
        /// <example>true</example>
        [JsonPropertyName("saveCardInfo")]
        public bool? SaveCardInfo { get; set; }

        /// <summary>
        /// Enable auth-capture payment (optional)
        /// </summary>
        /// <example>true</example>
        [JsonPropertyName("authCaptureModePayment")]
        public bool? AuthCaptureModePayment { get; set; }

        /// <summary>
        /// Request signature for security validation
        /// </summary>
        /// <example>a1b2c3d4e5f6g7h8i9j0...</example>
        [Required(ErrorMessage = "Signature is required")]
        [StringLength(64, MinimumLength = 64, ErrorMessage = "Signature must be 64 characters")]
        [JsonPropertyName("signature")]
        public string Signature { get; set; }

    }

    public class FawryChargeItem {
        /// <summary>
        /// Unique product identifier
        /// </summary>
        /// <example>PROD001</example>
        [Required(ErrorMessage = "Item ID is required")]
        [JsonPropertyName("itemId")]
        public string ItemId { get; set; }

        /// <summary>
        /// Product description (optional)
        /// </summary>
        /// <example>Premium Subscription</example>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Price per unit
        /// </summary>
        /// <example>99.99</example>
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Item quantity
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Product image URL (optional)
        /// </summary>
        /// <example>https://example.com/product.jpg</example>
        [Url(ErrorMessage = "Invalid URL format")]
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }
    }

    /// <summary>
    /// Represents a shipping address
    /// </summary>
    public class ShippingAddress {
    /// <summary>
    /// Governorate code
    /// </summary>
    /// <example>GOV01</example>
    [Required(ErrorMessage = "Governorate is required")]
    [JsonPropertyName("governorate")]
    public string Governorate { get; set; }

    /// <summary>
    /// City code
    /// </summary>
    /// <example>CITY01</example>
    [Required(ErrorMessage = "City is required")]
    [JsonPropertyName("city")]
    public string City { get; set; }

    /// <summary>
    /// Area code
    /// </summary>
    /// <example>AREA01</example>
    [Required(ErrorMessage = "Area is required")]
    [JsonPropertyName("area")]
    public string Area { get; set; }

    /// <summary>
    /// Receiver address
    /// </summary>
    /// <example>9th Ahmed Basha St., apartment number 8, 4th floor</example>
    [Required(ErrorMessage = "Address is required")]
    [JsonPropertyName("address")]
    public string Address { get; set; }

    /// <summary>
    /// Receiver name
    /// </summary>
    /// <example>Mohamed Ahmed</example>
    [Required(ErrorMessage = "Receiver name is required")]
    [JsonPropertyName("receiverName")]
    public string ReceiverName { get; set; }
}

}
