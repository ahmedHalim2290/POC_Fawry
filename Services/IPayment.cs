using POC_Fawry.Models;
using System.Threading.Tasks;

namespace POC_Fawry.Services {
    public interface IPayment {
        Task<FawryCancelResponse> CancelTransactionAsync(FawryCancelRequest request);
        Task<string> FawryCallbackAsync(FawryCallbackResponse request);
        Task<FawryChargeResponse> FawryChargeAsync(FawryChargeRequest request);
        Task<FawryChargeAuthResponse> FawryAuthChargeAsync(FawryChargeAuthRequest request);
        Task<FawryCaptureResponse> FawryCaptureAsync(FawryCaptureRequest request);
        Task<FawryRefundResponse> RefundTransactionAsync(FawryRefundRequest request);
        Task<FawryAuthCancelResponse> CancelAuthTransactionAsync(FawryAuthCancelRequest request);
        Task<PaymentStatusResponse> GetPaymentStatus();
        
    }
}
