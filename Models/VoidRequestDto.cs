using Newtonsoft.Json;
namespace POC_Fawry.Models {
    public class VoidRequestDto {
        [JsonProperty("transaction_id")]
        public int TransactionId { get; set; }
    }
}