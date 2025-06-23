using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POC_Fawry.Models {
    public class PaymentStatusResponse {

        /// <summary>
        /// UUID generated Request id
        /// </summary>
        /// <example>c72827d084ea4b88949d91dd2db4996e</example>
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }

        /// <summary>
        /// The reference number of this order in atFawry system
        /// </summary>
        /// <example>9990076204</example>
        [JsonPropertyName("fawryRefNumber")]
        public string FawryRefNumber { get; set; }

        /// <summary>
        /// The reference number of this order at merchant's system
        /// </summary>
        /// <example>9990076204</example>
        [JsonPropertyName("merchantRefNumber")]
        public string MerchantRefNumber { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        /// <example>FirstName LastName</example>
        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Cell Phone
        /// </summary>
        /// <example>01xxxxxxxxx</example>
        [JsonPropertyName("customerMobile")]
        public string CustomerMobile { get; set; }

        /// <summary>
        /// Customer email to which payment confirmation will be sent
        /// </summary>
        /// <example>example@domain.com</example>
        [EmailAddress]
        [JsonPropertyName("customerMail")]
        public string CustomerMail { get; set; }

        /// <summary>
        /// The Profile id for the customer in the merchant system
        /// </summary>
        /// <example>ACD23658</example>
        [JsonPropertyName("customerMerchantId")]
        public string CustomerMerchantId { get; set; }

        /// <summary>
        /// The amount value received from merchant to be paid by the customer
        /// </summary>
        /// <example>350.50</example>
        [Range(0.01, double.MaxValue)]
        [JsonPropertyName("paymentAmount")]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// The payment Amount without the fees
        /// </summary>
        /// <example>780.75</example>
        [Range(0.01, double.MaxValue)]
        [JsonPropertyName("orderAmount")]
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// The fees added by fawry for the order amount
        /// </summary>
        /// <example>10.00</example>
        [Range(0, double.MaxValue)]
        [JsonPropertyName("fawryFees")]
        public decimal FawryFees { get; set; }

        /// <summary>
        /// Shipping fees amount if applicable
        /// </summary>
        /// <example>5.50</example>
        [Range(0, double.MaxValue)]
        [JsonPropertyName("shippingFees")]
        public decimal ShippingFees { get; set; }

        /// <summary>
        /// The updated status of your transaction
        /// </summary>
        /// <example>PAID</example>
        [JsonPropertyName("orderStatus")]
        public string OrderStatus { get; set; }

        /// <summary>
        /// The configured payment method for the merchant
        /// </summary>
        /// <example>PAYATFAWRY</example>
        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// The actual time for the payment if the Order Status is PAID
        /// </summary>
        /// <example>19-05-2020 11:45:23</example>
        [JsonPropertyName("paymentTime")]
        public DateTime PaymentTime { get; set; }

        /// <summary>
        /// The transaction number in the bank
        /// </summary>
        /// <example>96322541122558</example>
        [JsonPropertyName("authNumber")]
        public long AuthNumber { get; set; }

        /// <summary>
        /// Unique number registered in FawryPay system to track the payment
        /// </summary>
        /// <example>369552233</example>
        [JsonPropertyName("paymentRefrenceNumber")]
        public string PaymentReferenceNumber { get; set; }

        /// <summary>
        /// The order expiry in hours for this order
        /// </summary>
        /// <example>3.5</example>
        [JsonPropertyName("orderExpiryDate")]
        public decimal OrderExpiryDate { get; set; }

        /// <summary>
        /// A list of order items associated to the order
        /// </summary>
        /// <example>123456</example>
        [JsonPropertyName("orderItems")]
        public string OrderItems { get; set; }

        /// <summary>
        /// Contains the error code in case of failure
        /// </summary>
        /// <example>9965</example>
        [JsonPropertyName("failureErrorCode")]
        public int? FailureErrorCode { get; set; }

        /// <summary>
        /// Contains an error message describing the failure reason
        /// </summary>
        /// <example>Wrong Signature</example>
        [JsonPropertyName("failureReason")]
        public string FailureReason { get; set; }

        /// <summary>
        /// The SHA-256 digest of concatenated string: 
        /// fawryRefNumber + merchantRefNum + Payment amount (in two decimal format) + 
        /// Order amount (in two decimal format) + Order Status + Payment method + 
        /// Payment reference number (if exists) + secureKey
        /// </summary>
        /// <example>ab34dcddfab34dcddfab34dcddfab34dcddfab34dcddfab34dcddfab34dcddf</example>
        [Required]
        [JsonPropertyName("messageSignature")]
        public string MessageSignature { get; set; }

        /// <summary>
        /// 3D Secure information
        /// </summary>
        [JsonPropertyName("threeDSInfo")]
        public ThreeDSecureInfo ThreeDSInfo { get; set; }

        /// <summary>
        /// Invoice information
        /// </summary>
        [JsonPropertyName("invoiceInfo")]
        public InvoiceInformation InvoiceInfo { get; set; }
    }

    /// <summary>
    /// Represents 3D Secure authentication information
    /// </summary>
    public class ThreeDSecureInfo {
        /// <summary>
        /// 3-D Secure Electronic Commerce Indicator
        /// </summary>
        /// <example>05</example>
        [JsonPropertyName("eci")]
        public string ECI { get; set; }

        /// <summary>
        /// Unique transaction identifier for 3DS transaction
        /// </summary>
        /// <example>VDj97t1qRJWM0ErrY2PtrBiSMQw=</example>
        [JsonPropertyName("xid")]
        public string Xid { get; set; }

        /// <summary>
        /// Whether the card is within an enrolled range (Y/N/U)
        /// </summary>
        /// <example>Y</example>
        [JsonPropertyName("enrolled")]
        public string Enrolled { get; set; }

        /// <summary>
        /// Authentication status (Y/N/A/U)
        /// </summary>
        /// <example>Y</example>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// Batch number for settlement
        /// </summary>
        /// <example>0</example>
        [JsonPropertyName("batchNumber")]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Transaction type (should be 'pay')
        /// </summary>
        /// <example>pay</example>
        [JsonPropertyName("command")]
        public string Command { get; set; }

        /// <summary>
        /// Approval message
        /// </summary>
        /// <example>Approved</example>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Verification Security Level
        /// </summary>
        /// <example>05</example>
        [JsonPropertyName("verSecurityLevel")]
        public string VerSecurityLevel { get; set; }

        /// <summary>
        /// Authentication status code
        /// </summary>
        /// <example>Y</example>
        [JsonPropertyName("verStatus")]
        public string VerStatus { get; set; }

        /// <summary>
        /// Authentication type (3DS/SPA)
        /// </summary>
        /// <example>3DS</example>
        [JsonPropertyName("verType")]
        public string VerType { get; set; }

        /// <summary>
        /// Authentication token from card issuer
        /// </summary>
        /// <example>gIGCg4SFhoeIiYqLjI2Oj5CRkpM=</example>
        [JsonPropertyName("verToken")]
        public string VerToken { get; set; }

        /// <summary>
        /// API version
        /// </summary>
        /// <example>1</example>
        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// Receipt number
        /// </summary>
        /// <example>1123456</example>
        [JsonPropertyName("receiptNumber")]
        public string ReceiptNumber { get; set; }

        /// <summary>
        /// Hosted checkout session ID
        /// </summary>
        /// <example>SESSION0002818019663G5075633E86</example>
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }
    }

    /// <summary>
    /// Represents invoice information
    /// </summary>
    public class InvoiceInformation {
        /// <summary>
        /// Number of the invoice
        /// </summary>
        /// <example>28176849</example>
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// Business Reference Number of the invoice
        /// </summary>
        /// <example>w0dd2fss41d2d2qs556</example>
        [JsonPropertyName("businessRefNumber")]
        public string BusinessRefNumber { get; set; }

        /// <summary>
        /// Due Date of the invoice
        /// </summary>
        /// <example>2021-06-19</example>
        [JsonPropertyName("dueDate")]
        public string DueDate { get; set; }

        /// <summary>
        /// Expiry Date of the invoice
        /// </summary>
        /// <example>1625062277000</example>
        [JsonPropertyName("expiryDate")]
        public string ExpiryDate { get; set; }

        /// <summary>
        /// Customer Paid amount as Interest fees
        /// </summary>
        /// <example>100.00</example>
        [JsonPropertyName("installmentInterestAmount")]
        public decimal InstallmentInterestAmount { get; set; }

        /// <summary>
        /// Number of Months for the instalment
        /// </summary>
        /// <example>6</example>
        [JsonPropertyName("installmentMonths")]
        public int InstallmentMonths { get; set; }
    }
}
