using System;
using System.Text.Json;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Chargily.Epay.CSharp
{
    public class ChargilyEpayClient : IChargilyEpayClient<EpayPaymentResponse, EpayPaymentRequest>
    {
        private readonly string _apiKey;
        private readonly IChargilyEpayAPI _apiClient;
        private readonly IValidator<EpayPaymentRequest> _validator;
#nullable enable
        private readonly ILogger<ChargilyEpayClient>? _logger;
#nullable disable
        public ChargilyEpayClient(string apiKey,
                                  ILogger<ChargilyEpayClient> logger,
                                  IValidator<EpayPaymentRequest> validator,
                                  IChargilyEpayAPI apiClient)
        {
            _apiKey    = apiKey;
            _logger    = logger;
            _validator = validator;
            _apiClient = apiClient;
        }

        public ChargilyEpayClient(IConfiguration configuration,
                                  ILogger<ChargilyEpayClient> logger,
                                  IValidator<EpayPaymentRequest> validator,
                                  IChargilyEpayAPI apiClient)
        {
            _logger    = logger;
            _validator = validator;
            _apiClient = apiClient;
            _apiKey    = configuration["CHARGILY_API_KEY"];
        }

        public async Task<EpayPaymentResponse> CreatePayment(EpayPaymentRequest request)
        {
            try
            {
                _logger?.LogInformation($"[ChargilyEpay.NET] New Payment Request:" +
                                        $"{Environment.NewLine}{JsonSerializer.Serialize(request)}");

                var response   = new EpayPaymentResponse();
                var validation = await _validator.ValidateAsync(request);
                response.ValidatePaymentRequest(validation);
                if (!validation.IsValid)
                {
                    _logger?.LogError($"[ChargilyEpay.NET] Payment Request Validation Error");
                    return response;
                }

                var apiResponse = await _apiClient.CreateInvoice(request, _apiKey);
                await response.CreatePaymentResponse(apiResponse);
                _logger?.LogInformation($"[ChargilyEpay.NET] Payment Response for Invoice '{request.InvoiceNumber}'" +
                                        $":{Environment.NewLine}{JsonSerializer.Serialize(response)}");

                return response;
            }
            catch (Exception ex)
            {
                _logger?.LogError($"[ChargilyEpay.NET] Exception Thrown: {0}", ex.Message);
                throw new Exception($"Create Payment Request Failed!. {ex.Message}", ex);
            }
        }
    }
}