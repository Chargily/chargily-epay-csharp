using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Chargily.EpayGateway.NET
{
    public static class ChagilyEpay
    {
        private static ChargilyEpayClient _client = null;
        private static IServiceProvider _serviceProvider;

        public static ChargilyEpayClient CreateClient(string apiKey)
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = new ServiceCollection()
                    .AddChargilyEpayGateway(apiKey)
                    .BuildServiceProvider();
            }

            if (_client == null)
            {
                _client = _serviceProvider.GetService<ChargilyEpayClient>();
            }

            return _client;
        }
    }
}