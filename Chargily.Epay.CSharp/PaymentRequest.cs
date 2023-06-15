using System.Text.Json.Serialization;

namespace Chargily.Epay.CSharp
{
    public class PaymentRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string InvoiceNumber { get; set; }

        public double Amount { get; set; }

        public double DiscountPercentage { get; set; }

        public string BackUrl { get; set; }
        public string WebhookUrl { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }

        public string ExtraInfo { get; set; }

        [JsonIgnore] public double AmountAfterDiscount => Amount - Amount * DiscountPercentage / 100;
    }
}