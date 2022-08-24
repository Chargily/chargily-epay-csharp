using Chargily.Epay;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();


builder.Services
    .AddChargilyEpayGateway("api_KFWtdBczv0qnAMHNxGXCGVK93yEZahZwr4EgFa4xmfnLTIJkezPvW0LgqholrC7S");
builder.Services
    .AddChargilyValidatorMiddleware("secret_59e7e5159e887cf8c512e8a6de2dff1272e52ee7a68237d471df5fdabe97a1a1");


var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI();

app.MapPost("/invoice",
    async ([FromBody] EpayPaymentRequest request,
        [FromServices] IChargilyEpayClient<EpayPaymentResponse, EpayPaymentRequest> chargilyClient) =>
    {
        return await chargilyClient.CreatePayment(request);
    });

app.MapPost("/webhook-validator", ([FromServices] IWebHookValidator validator, HttpRequest request) =>
{
    var signature = request.Headers["Signature"].First();
    var validation = validator.Validate(signature, request.Body);

    return validation;
});

app.UseChargilyValidatorMiddleware();

app.Run();