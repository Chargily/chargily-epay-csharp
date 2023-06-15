using System;
using System.Net.Http;

using Chargily.Epay.CSharp.Validations;

using FluentValidation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Refit;

namespace Chargily.Epay.CSharp
{
    public static partial class ChargilyEpayService
    {
        /// <summary>
        /// Add Chargily Epay Gateway Service, API KEY is loaded automatically from 'appsettings.json' 'CHARGILY_API_KEY' field only if using ASP.NET Core or .NET MAUI otherwise it throws an exception
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddChargilyEpayGateway(this IServiceCollection services)
        {
            return services
                  .AddLogging()
                  .AddHttpClient()
                  .AddRefitClient<IChargilyEpayAPI>()
                  .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://epay.chargily.com.dz"))
                  .Services
                  .AddSingleton<IValidator<EpayPaymentRequest>, PaymentRequestValidator>()
                  .AddSingleton<IChargilyEpayClient<EpayPaymentResponse, EpayPaymentRequest>, ChargilyEpayClient>(
                    provider =>
                    {
                        var logger        = provider.GetService<ILogger<ChargilyEpayClient>>();
                        var validator     = provider.GetService<IValidator<EpayPaymentRequest>>();
                        var apiClient     = provider.GetService<IChargilyEpayAPI>();
                        var configuration = provider.GetService<IConfiguration>();

                        return new ChargilyEpayClient(configuration, logger, validator, apiClient);
                    });
        }

        /// <summary>
        /// Add Chargily Epay Gateway Service, API KEY is loaded automatically from 'appsettings.json' 'CHARGILY_API_KEY' field only if using ASP.NET Core or .NET MAUI otherwise it throws an exception
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureHttpClient">Configure HttpClient instance and BaseAddress</param>
        /// <returns></returns>
        public static IServiceCollection AddChargilyEpayGateway(this IServiceCollection services,
                                                                Action<HttpClient> configureHttpClient)
        {
            return services
                  .AddLogging()
                  .AddHttpClient()
                  .AddRefitClient<IChargilyEpayAPI>()
                  .ConfigureHttpClient(configureHttpClient)
                  .Services
                  .AddSingleton<IValidator<EpayPaymentRequest>, PaymentRequestValidator>()
                  .AddSingleton<IChargilyEpayClient<EpayPaymentResponse, EpayPaymentRequest>, ChargilyEpayClient>(
                    provider =>
                    {
                        var logger        = provider.GetService<ILogger<ChargilyEpayClient>>();
                        var validator     = provider.GetService<IValidator<EpayPaymentRequest>>();
                        var apiClient     = provider.GetService<IChargilyEpayAPI>();
                        var configuration = provider.GetService<IConfiguration>();

                        return new ChargilyEpayClient(configuration, logger, validator, apiClient);
                    });
        }

        /// <summary>
        /// Add Chargily Epay Gateway Service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="apiKey">Chargily API KEY, visit Chargily dashboard to get one https://epay.chargily.com.dz/secure/admin/epay-api</param>
        /// <returns></returns>
        public static IServiceCollection AddChargilyEpayGateway(this IServiceCollection services, string apiKey)
        {
            return services
                  .AddLogging()
                  .AddHttpClient()
                  .AddRefitClient<IChargilyEpayAPI>()
                  .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://epay.chargily.com.dz"))
                  .Services
                  .AddSingleton<IValidator<EpayPaymentRequest>, PaymentRequestValidator>()
                  .AddSingleton<IChargilyEpayClient<EpayPaymentResponse, EpayPaymentRequest>, ChargilyEpayClient>(
                    provider =>
                    {
                        var logger    = provider.GetService<ILogger<ChargilyEpayClient>>();
                        var validator = provider.GetService<IValidator<EpayPaymentRequest>>();
                        var apiClient = provider.GetService<IChargilyEpayAPI>();

                        return new ChargilyEpayClient(apiKey, logger, validator, apiClient);
                    });
        }

        /// <summary>
        /// Add Chargily Epay Gateway Service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="apiKey">Chargily API KEY, visit Chargily dashboard to get one https://epay.chargily.com.dz/secure/admin/epay-api</param>
        /// <param name="configureHttpClient">Configure HttpClient instance and BaseAddress</param>
        /// <returns></returns>
        public static IServiceCollection AddChargilyEpayGateway(this IServiceCollection services, string apiKey,
                                                                Action<HttpClient> configureHttpClient)
        {
            return services
                  .AddLogging()
                  .AddHttpClient()
                  .AddRefitClient<IChargilyEpayAPI>()
                  .ConfigureHttpClient(configureHttpClient)
                  .Services
                  .AddSingleton<IValidator<EpayPaymentRequest>, PaymentRequestValidator>()
                  .AddSingleton<IChargilyEpayClient<EpayPaymentResponse, EpayPaymentRequest>, ChargilyEpayClient>(
                    provider =>
                    {
                        var logger    = provider.GetService<ILogger<ChargilyEpayClient>>();
                        var validator = provider.GetService<IValidator<EpayPaymentRequest>>();
                        var apiClient = provider.GetService<IChargilyEpayAPI>();

                        return new ChargilyEpayClient(apiKey, logger, validator, apiClient);
                    });
        }

        /// <summary>
        /// Add Chargily Epay Gateway Service
        /// </summary>
        /// <typeparam name="TResponse">Payment Response Type</typeparam>
        /// <typeparam name="TRequest">Payment Request Type</typeparam>
        /// <param name="services"></param>
        /// <param name="client">custom IChargilyEpayClient instance instead of the default one</param>
        /// <returns></returns>
        public static IServiceCollection AddChargilyEpayGateway<TResponse, TRequest>(this IServiceCollection services,
            IChargilyEpayClient<TResponse, TRequest> client)
        {
            return services
                  .AddLogging()
                  .AddHttpClient()
                  .AddRefitClient<IChargilyEpayAPI>()
                  .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://epay.chargily.com.dz"))
                  .Services
                  .AddSingleton<IValidator<EpayPaymentRequest>, PaymentRequestValidator>()
                  .AddSingleton(client);
        }

        /// <summary>
        /// Add Chargily Epay Gateway Service
        /// </summary>
        /// <typeparam name="TResponse">Payment Response Type</typeparam>
        /// <typeparam name="TRequest">Payment Request Type</typeparam>
        /// <param name="services"></param>
        /// <param name="client">custom IChargilyEpayClient instance instead of the default one</param>
        /// <param name="configureHttpClient">Configure HttpClient instance and BaseAddress</param>
        /// <returns></returns>
        public static IServiceCollection AddChargilyEpayGateway<TResponse, TRequest>(this IServiceCollection services,
            IChargilyEpayClient<TResponse, TRequest> client, Action<HttpClient> configureHttpClient)
        {
            return services
                  .AddLogging()
                  .AddHttpClient()
                  .AddRefitClient<IChargilyEpayAPI>()
                  .ConfigureHttpClient(configureHttpClient)
                  .Services
                  .AddSingleton<IValidator<EpayPaymentRequest>, PaymentRequestValidator>()
                  .AddSingleton(client);
        }
    }
}