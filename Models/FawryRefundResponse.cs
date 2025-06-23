using Newtonsoft.Json;

namespace POC_Fawry.Models {
    public class FawryRefundResponse {
        /// <summary>
        /// Specific type of the response.
        /// </summary>
        /// <example>ChargeResponse</example>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The response status code.
        /// </summary>
        /// <example>200</example>
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        /// <summary>
        /// Exact description of the status of FawryPay response.
        /// </summary>
        /// <example>Operation done successfully</example>
        /// <example>Wrong Signature</example>
        [JsonProperty("statusDescription")]
        public string StatusDescription { get; set; }
    }
}
