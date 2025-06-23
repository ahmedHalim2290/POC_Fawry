namespace POC_Fawry {
    public class FawryOptions {
        public const string Fawry= "Fawry";
        public  string authToken { get; set; }
        public  string PaymentTransactionURL { get; set; }
        public  string CaptureTransactionURL { get; set; }
        public  string VoidTransactionURL {  get; set; }
        public  int AuthIntegrationID {  get; set; }
        public  int StandAloneIntegrationID { get; set; }
        public string RefundTransactionURL {  get; set; }  
        public string AuthTokenURL { get;set;}
        public string TransacrionByMerchantOrderIdURL { get; set; }
        public string TransacrionByOrderIdURL { get; set; }
        public string TransacrionByTrxIdURL { get; set; }
        public  string  HMACSecret { get; set; }
        public string APIKey { get; set; }  
    }
}
