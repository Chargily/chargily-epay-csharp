using System.Text.Json.Serialization;

namespace Chargily.Epay.CSharp
{
    public class EpayPaymentRequest
    {
        [JsonPropertyName("client")] public string Name { get; set; }

        [JsonPropertyName("client_email")] public string Email { get; set; }

        [JsonPropertyName("invoice_number")] public string InvoiceNumber { get; set; }

        [JsonPropertyName("amount")] public double Amount { get; set; }

        [JsonPropertyName("discount")] public double DiscountPercentage { get; set; }

        [JsonPropertyName("back_url")] public string BackUrl { get; set; }
        [JsonPropertyName("webhook_url")] public string WebhookUrl { get; set; }

        [JsonPropertyName("mode")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonPropertyName("comment")] public string ExtraInfo { get; set; }

        [JsonIgnore] public double AmountAfterDiscount => Amount - Amount * DiscountPercentage / 100;
    }
}