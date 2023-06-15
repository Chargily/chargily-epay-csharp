using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Chargily.Epay.CSharp
{
    public class WebHookValidator : IWebHookValidator
    {
        private readonly ILogger<WebHookValidator> _logger;
        private readonly string _appSecret;
        private readonly HMACSHA256 _hmac;

        public WebHookValidator(IConfiguration configuration, ILogger<WebHookValidator> logger)
        {
            _logger    = logger;
            _appSecret = configuration["CHARGILY_APP_SECRET"];
            _hmac      = new HMACSHA256(Encoding.UTF8.GetBytes(_appSecret));
        }

        public WebHookValidator(string appSecret, ILogger<WebHookValidator> logger)
        {
            _logger    = logger;
            _appSecret = appSecret;
            _hmac      = new HMACSHA256(Encoding.UTF8.GetBytes(_appSecret));
        }

        public bool Validate(string signature, string responseBodyJson)
        {
            var body     = Encoding.UTF8.GetBytes(responseBodyJson);
            var computed = _hmac.ComputeHash(body);

            var computedHex = BitConverter.ToString(computed).Replace("-", "");
            var validation  = signature.Equals(computedHex, StringComparison.OrdinalIgnoreCase);

            _logger.LogInformation($"[ChargilyEpay.NET] Signature: '{signature}' IsValid?: {validation}");
            return validation;
        }

        public bool Validate(string signature, Stream body)
        {
            var stream = new MemoryStream();
            body.CopyTo(stream);
            var computed    = _hmac.ComputeHash(stream.ToArray());
            var computedHex = BitConverter.ToString(computed).Replace("-", "");
            var validation  = signature.Equals(computedHex, StringComparison.OrdinalIgnoreCase);

            _logger.LogInformation($"[ChargilyEpay.NET] Signature: '{signature}' IsValid?: {validation}");
            return validation;
        }
    }
}