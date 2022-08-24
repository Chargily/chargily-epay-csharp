using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace Chargily.Epay
{
    public interface IChargilyEpayAPI
    {
        [Post("/api/invoice")]
        [Headers("Accept: application/json")]
        Task<HttpResponseMessage> CreateInvoice(EpayPaymentRequest epayPaymentRequest,
            [Header("X-Authorization")] string apiKey);
    }
}