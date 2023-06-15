using System;
using System.Text.Json.Serialization;

namespace Chargily.Epay.CSharp
{
    public class ChargilyWebhookRequest
    {
        public ChargilyWebhookInvoice Invoice { get; set; }
    }

    public enum InvoiceStatus
    {
        Paid,
        Failed,
        Canceled
    }

    public class ChargilyWebhookInvoice
    {
        public int Id { get; set; }
        public string Client { get; set; }
        [JsonPropertyName("client_email")] public string Email { get; set; }
        [JsonPropertyName("invoice_number")] public string InvoiceId { get; set; }
        public double Amount { get; set; }
        public double Discount { get; set; }
        [JsonPropertyName("due_amount")] public double DueAmount { get; set; }

        [JsonPropertyName("mode")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updated_at:")] public DateTime UpdatedAt { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public InvoiceStatus Status { get; set; }
    }
}