using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace Chargily.EpayGateway.NET
{
    public class WebHookValidatorMiddleware : IMiddleware
    {
        private readonly IWebHookValidator _webhookValidator;
        private readonly ILogger<WebHookValidatorMiddleware> _logger;

        public WebHookValidatorMiddleware(IWebHookValidator validator, ILogger<WebHookValidatorMiddleware> logger)
        {
            _webhookValidator = validator;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                if (context.Request.Headers.ContainsKey("Signature") && HttpMethods.IsPost(context.Request.Method))
                {
                    var signature = context.Request.Headers["Signature"].First();
                    _logger.LogInformation(
                        $"[ChargilyEpay.NET Validation Middleware] Intercepted a request with 'Signature' Header: {signature}");

                    var isValid = _webhookValidator.Validate(signature, context.Request.Body);
                    if (!isValid)
                    {
                        var response = new EpayPaymentResponse()
                        {
                            Body = null,
                            HttpStatusCode = HttpStatusCode.BadRequest,
                            ResponseMessage = JsonSerializer.SerializeToDocument(new
                                { Message = "Signature Validation Failed!", ProvidedSignature = signature })
                        };
                        _logger.LogError(
                            $"[ChargilyEpay.NET Validation Middleware] Signature Validation Failed! Request with 'Signature' Header: {signature}");
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                await next(context);
            }
        }
    }
}