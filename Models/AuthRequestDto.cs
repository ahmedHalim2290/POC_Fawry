using Newtonsoft.Json;

namespace POC_Fawry.Models {
    public class AuthRequestDto {
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }
    }
}
