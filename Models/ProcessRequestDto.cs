using Newtonsoft.Json;
namespace POC_Fawry.Models {
    public class ProcessRequestDto {
        [JsonProperty("transaction_id")]
        public int TransactionId { get; set; }

        [JsonProperty("amount_cents")]
        public decimal AmountCents { get; set; }
    }
}