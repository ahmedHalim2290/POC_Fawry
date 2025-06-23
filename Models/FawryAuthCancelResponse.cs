using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POC_Fawry.Models {
    public class FawryAuthCancelResponse {
       
            /// <summary>
            /// Specific type of the response
            /// </summary>
            /// <example>PaymentStatusResponse</example>
            [JsonPropertyName("type")]
            public string Type { get; set; } = "PaymentStatusResponse";

            /// <summary>
            /// The reference number of the order on FawryPay system
            /// </summary>
            /// <example>23124654641</example>
            [JsonPropertyName("fawryRefNumber")]
            public string FawryRefNumber { get; set; }

            /// <summary>
            /// The merchant ID on FawryPay system
            /// </summary>
            /// <example>23124654641</example>
            [JsonPropertyName("merchantCode")]
            public string MerchantCode { get; set; }

            /// <summary>
            /// The reference number of the order on merchant's system
            /// </summary>
            /// <example>100162801</example>
            [JsonPropertyName("merchantRefNumber")]
            public string MerchantRefNumber { get; set; }

            /// <summary>
            /// The current status of the order
            /// </summary>
            /// <example>PAID</example>
            [JsonPropertyName("orderStatus")]
            public string OrderStatus { get; set; }

            /// <summary>
            /// The response status code
            /// </summary>
            /// <example>200</example>
            [JsonPropertyName("statusCode")]
            public int StatusCode { get; set; }

            /// <summary>
            /// Detailed description of the response status
            /// </summary>
            /// <example>Operation done successfully</example>
            [JsonPropertyName("statusDescription")]
            public string StatusDescription { get; set; }
        }
    }

