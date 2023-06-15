using System;

using Chargily.Epay.CSharp.Validations;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Refit;

namespace Chargily.Epay.CSharp
{
    public static class ChargilyEpay
    {
        private static ChargilyEpayClient _client = null;
        private static IServiceProvider _provider;


        public static ChargilyEpayClient CreateClient(string apiKey)
        {
            if (_provider == null)
                _provider = new ServiceCollection()
                           .AddLogging()
                           .AddHttpClient()
                           .AddRefitClient<IChargilyEpayAPI>()
                           .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://epay.chargily.com.dz"))
                           .Services
                           .AddSingleton<IValidator<EpayPaymentRequest>, PaymentRequestValidator>()
                           .BuildServiceProvider();

            if (_client == null)
            {
                var logger    = _provider.GetService<ILogger<ChargilyEpayClient>>();
                var validator = _provider.GetService<IValidator<EpayPaymentRequest>>();
                var apiClient = _provider.GetService<IChargilyEpayAPI>();

                _client = new ChargilyEpayClient(apiKey, logger, validator, apiClient);
            }

            return _client;
        }
    }
}