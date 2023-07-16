using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using FluentValidation.Results;

namespace Chargily.Epay.CSharp
{
    public class EpayPaymentResponse
    {
        public HttpStatusCode HttpStatusCode { get; internal set; }
        public JsonDocument ResponseMessage { get; internal set; }
        public bool IsSuccessful { get; internal set; } = false;
        public bool IsRequestValid { get; internal set; } = false;
        public JsonDocument Body { get; internal set; }
        public DateTime CreatedOn { get; internal set; } = DateTime.Now;

        public async Task CreatePaymentResponse(HttpResponseMessage httpResponse)
        {
            HttpStatusCode = httpResponse.StatusCode;
            if (httpResponse.IsSuccessStatusCode)
            {
                IsSuccessful = true;
                Body = JsonSerializer.Deserialize<JsonDocument>(await httpResponse.Content.ReadAsStringAsync());
                ResponseMessage = JsonSerializer.SerializeToDocument(new { Message = "Success" });
            }
            else
            {
                IsSuccessful = false;
                ResponseMessage =
                    JsonSerializer.Deserialize<JsonDocument>(await httpResponse.Content.ReadAsStringAsync());
                Body = null;
            }
        }

        public void ValidatePaymentRequest(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                IsRequestValid  = false;
                ResponseMessage = JsonSerializer.SerializeToDocument(validationResult.Errors);
            }
            else
            {
                IsRequestValid = true;
            }
        }
    }
}