using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POC_Fawry.Models{
    public class FawryCallbackResponse {
        [Required]
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }

        [Required]
        [JsonPropertyName("fawryRefNumber")]
        public string FawryRefNumber { get; set; }

        [Required]
        [JsonPropertyName("merchantRefNumber")]
        public string MerchantRefNumber { get; set; }

        [Phone]
        [JsonPropertyName("customerMobile")]
        public string CustomerMobile { get; set; }

        [EmailAddress]
        [JsonPropertyName("customerMail")]
        public string CustomerMail { get; set; }

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; }

        [JsonPropertyName("customerMerchantId")]
        public string CustomerMerchantId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [JsonPropertyName("paymentAmount")]
        public decimal PaymentAmount { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [JsonPropertyName("orderAmount")]
        public decimal OrderAmount { get; set; }

        [Range(0, double.MaxValue)]
        [JsonPropertyName("fawryFees")]
        public decimal FawryFees { get; set; }

        [Range(0, double.MaxValue)]
        [JsonPropertyName("shippingFees")]
        public decimal? ShippingFees { get; set; }

        [Required]
        [JsonPropertyName("orderStatus")]
        public string OrderStatus { get; set; } // NEW, PAID, CANCELED, etc.

        [Required]
        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; } // PAYATFAWRY, CARD, etc.

        [JsonPropertyName("paymentTime")]
        public long? PaymentTime { get; set; } // Unix timestamp in milliseconds

        [JsonPropertyName("authNumber")]
        public string AuthNumber { get; set; }

        [JsonPropertyName("paymentRefrenceNumber")]
        public string PaymentReferenceNumber { get; set; }

        [JsonPropertyName("orderExpiryDate")]
        public long OrderExpiryDate { get; set; } // Unix timestamp in milliseconds

        [Required]
        [JsonPropertyName("orderItems")]
        public List<OrderItem> OrderItems { get; set; }

        [JsonPropertyName("failureErrorCode")]
        public int? FailureErrorCode { get; set; }

        [JsonPropertyName("failureReason")]
        public string FailureReason { get; set; }

        [Required]
        [JsonPropertyName("messageSignature")]
        public string MessageSignature { get; set; }

        [JsonPropertyName("threeDSInfo")]
        public ThreeDSInfo ThreeDSInfo { get; set; }

        [JsonPropertyName("invoiceInfo")]
        public InvoiceInfo InvoiceInfo { get; set; }

        [JsonPropertyName("installmentInterestAmount")]
        public decimal InstallmentInterestAmount { get; set; }

        [JsonPropertyName("installmentMonths")]
        public int InstallmentMonths { get; set; }
    }
        public class OrderItem {
            [Required]
            [JsonPropertyName("itemCode")]
            public string ItemCode { get; set; }

            [Required]
            [Range(0, double.MaxValue)]
            [JsonPropertyName("price")]
            public decimal Price { get; set; }

            [Required]
            [Range(1, int.MaxValue)]
            [JsonPropertyName("quantity")]
            public int Quantity { get; set; }
        }

        public class ThreeDSInfo {
            [JsonPropertyName("eci")]
            public string ECI { get; set; }

            [JsonPropertyName("xid")]
            public string Xid { get; set; }

            [JsonPropertyName("enrolled")]
            public string Enrolled { get; set; } // Y, N, U

            [JsonPropertyName("status")]
            public string Status { get; set; } // Y, N, A, U

            [JsonPropertyName("batchNumber")]
            public string BatchNumber { get; set; }

            [JsonPropertyName("command")]
            public string Command { get; set; }

            [JsonPropertyName("message")]
            public string Message { get; set; }

            [JsonPropertyName("verSecurityLevel")]
            public string VerSecurityLevel { get; set; }

            [JsonPropertyName("verStatus")]
            public string VerStatus { get; set; } // Y, E, N, U, etc.

            [JsonPropertyName("verType")]
            public string VerType { get; set; } // 3DS or SPA

            [JsonPropertyName("verToken")]
            public string VerToken { get; set; }

            [JsonPropertyName("version")]
            public string Version { get; set; }

            [JsonPropertyName("receiptNumber")]
            public string ReceiptNumber { get; set; }

            [JsonPropertyName("sessionId")]
            public string SessionId { get; set; }
        }

        public class InvoiceInfo {
            [JsonPropertyName("number")]
            public string Number { get; set; }

            [JsonPropertyName("businessRefNumber")]
            public string BusinessRefNumber { get; set; }

            [JsonPropertyName("dueDate")]
            public string DueDate { get; set; } // "YYYY-MM-DD" format

            [JsonPropertyName("expiryDate")]
            public long ExpiryDate { get; set; } // Unix timestamp in milliseconds
        }
 }