using System.Text.Json;

using Chargily.Epay;
using Chargily.Epay.CSharp;

Console.Write($"Provide Chargily API_KEY : ");
var apiKey = Console.ReadLine();

var client = ChargilyEpay.CreateClient(apiKey);

var payment = new EpayPaymentRequest()
              {
                  InvoiceNumber      = $"{Random.Shared.NextInt64(100_000, int.MaxValue)}",
                  Name               = "Ahmed",
                  Email              = "rainxh11@gmail.com",
                  Amount             = 1500,
                  DiscountPercentage = 5.0,
                  PaymentMethod      = PaymentMethod.EDAHABIA,
                  BackUrl            = "https://yourapp.com/",
                  WebhookUrl         = "https://api.yourbackend.com/webhook-validator",
                  ExtraInfo          = "Product Purchase"
              };

var response = await client.CreatePayment(payment);


Console.WriteLine("Response: {0}",
                  JsonSerializer.Serialize(response, new JsonSerializerOptions() { WriteIndented = true }));