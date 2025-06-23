using POC_Fawry.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace POC_Fawry.Services {
    public class FawryHttpService {
        private readonly HttpClient _httpClient;
        private readonly FawryOptions _config;
        private readonly ILogger<FawryHttpService> _logger;
        public string GetMerchantCode() => _config.MerchantCode;
        public string GetSecurityKey() => _config.SecurityKey;
        public string GetMerchantRefNumber() => _config.MerchantRefNumber;
        public FawryHttpService(HttpClient httpClient, IOptions<FawryOptions> config, ILogger<FawryHttpService> logger)
        {
            _httpClient = httpClient;
            _config = config.Value;
            _httpClient.BaseAddress = new Uri(_config.BaseUrl);
            _logger = logger;
        }
      /*  public void SetAuthorizationHeader(string token)
        {
            // Remove existing Authorization header if any
            _httpClient.DefaultRequestHeaders.Remove("Authorization");

            // Add the new Bearer token
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }*/
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest request)//, string? token = null)
        {
           
            var requestJson = JsonConvert.SerializeObject(request);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            _logger.LogInformation($"Request to {_httpClient.BaseAddress+endpoint}: {requestJson}");
            var response = await _httpClient.PostAsync(_httpClient.BaseAddress+endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Response from {_httpClient.BaseAddress + endpoint}: {responseJson}");

            return JsonConvert.DeserializeObject<TResponse>(responseJson);
        }
        public async Task<TResponse> GetAsync<TRequest, TResponse>(string endpoint, TRequest request)//, string? token = null)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var HttpRequest = new HttpRequestMessage(HttpMethod.Get, endpoint);
            HttpRequest.Content = content;
            var response = await _httpClient.SendAsync(HttpRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }
    }
}
