using System.Net.Http;
using System.Threading.Tasks;

using Refit;

namespace Chargily.Epay.CSharp
{
    public interface IChargilyEpayAPI
    {
        [Post("/api/invoice")]
        [Headers("Accept: application/json")]
        Task<HttpResponseMessage> CreateInvoice(EpayPaymentRequest epayPaymentRequest,
                                                [Header("X-Authorization")] string apiKey);
    }
}