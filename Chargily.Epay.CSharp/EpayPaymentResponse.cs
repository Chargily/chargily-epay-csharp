using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace chargily.epay.csharp
{
    public class EpayPaymentResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public JsonDocument ResponseMessage { get; set; }
        public bool IsSuccessful { get; set; } = false;
        public bool IsRequestValid { get; set; } = false;
        public JsonDocument Body { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public async Task CreatePaymentResponse(HttpResponseMessage httpResponse)
        {
            this.HttpStatusCode = httpResponse.StatusCode;
            if (httpResponse.IsSuccessStatusCode)
            {
                this.IsSuccessful = true;
                this.Body = JsonSerializer.Deserialize<JsonDocument>(await httpResponse.Content.ReadAsStringAsync());
                this.ResponseMessage = JsonSerializer.SerializeToDocument(new { Message = "Success" });
            }
            else
            {
                this.IsSuccessful = false;
                this.ResponseMessage =
                    JsonSerializer.Deserialize<JsonDocument>(await httpResponse.Content.ReadAsStringAsync());
                this.Body = null;
            }
        }

        public void ValidatePaymentRequest(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                this.IsRequestValid = false;
                this.ResponseMessage = JsonSerializer.SerializeToDocument(validationResult.Errors);
            }
            else
            {
                this.IsRequestValid = true;
            }
        }
    }
}