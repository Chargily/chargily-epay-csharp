<img src="https://raw.githubusercontent.com/chargily/epay-gateway-csharp/master/assets/chargily.svg" width="300">

| Nuget Pacakge | Downloads |
|-|-|
|[![Latest version](https://img.shields.io/nuget/v/chargily.epay.csharp.svg)](https://www.nuget.org/packages/chargily.epay.csharp/)|![Downloads](https://img.shields.io/nuget/dt/chargily.epay.csharp.svg)|




# Chargily ePay Gateway C\# Package
## This package supports the following frameowrks and platforms:
| Framework | Support | Platform |
|-|-|-|
| Console | ✅|Windows, Linux, macOS|
| ASP.NET Core |✅|Windows, Linux, macOS|
| Blazor WASM |✅|Windows, Linux, macOS|
| Blazor Server |✅|Windows, Linux, macOS|
|.NET MAUI |✅|Windows, Linux, macOS, Android, iOS, Tizen|
| Xamarin | ✅ | Android, iOS |
| ASP.NET |✅|Windows|
| WPF |✅|Windows|
| AvaloniaUI | ✅| Windows, Linux, macOS |
| UWP | ✅ | Windows, Xbox OS |
| WinForms | ✅ | Windows |

Any C# application that uses `Microsoft.Extensions.DependencyInjection` can use this package
#
![Chargily ePay Gateway](https://raw.githubusercontent.com/Chargily/epay-gateway-php/main/assets/banner-1544x500.png "Chargily ePay Gateway")

Integrate ePayment gateway with Chargily easily.
- Currently support payment by **CIB / EDAHABIA** cards and soon by **Visa / Mastercard**
- This is a **C#.NET Nuget Package**, If you are using another programing language [Browse here](https://github.com/Chargily/) or look to [API documentation](https://github.com/Chargily/epay-gateway-php/blob/master/README.md)

# Installation
**First**, install the `chargily.epay.csharp` [NuGet package](https://www.nuget.org/packages/chargily.epay.csharp) into your app

#### __Using DotNet CLI :__
```powershell
dotnet add chargily.epay.csharp
```

#### __Using Visual Studio Dev Console:__
```powershell
Install-Package chargily.epay.csharp
```

#### Using Visual Studio IDE:
https://user-images.githubusercontent.com/35665161/186457000-dd4882d5-d683-4a58-a069-6f092f5a1498.mp4


# Requirements
1. Get your API Key/Secret from [ePay by Chargily](https://epay.chargily.com.dz) dashboard for free

# How to use
### Installation & Project Creation Video Guide
https://user-images.githubusercontent.com/35665161/186457062-26920590-3e62-4154-805f-95e3688afd40.mp4

#### Implemention of the code below:
https://user-images.githubusercontent.com/35665161/186457825-74178dbb-7a13-47df-9caa-cf2a90b2f50a.mp4



### __Usage with any generic C# Project:__
this package provide `ChargilyEpayClient` client, to create payment request use: 
```csharp
using Chargily.Epay;

var client = ChargilyEpay.CreateClient("[API_KEY]");

var payment = new EpayPaymentRequest()
{
    InvoiceNumber = "[INVOICE_NUMBER]",
    Name = "Ahmed",
    Email = "rainxh11@gmail.com",
    Amount = 1500,
    DiscountPercentage = 5.0,
    PaymentMethod = PaymentMethod.EDAHABIA,
    BackUrl = "https://yourapp.com/",
    WebhookUrl = "https://api.yourbackend.com/webhook-validator",
    ExtraInfo = "Product Purchase"
};

var response = await client.CreatePayment(payment);
```

# Usage with ASP.NET Core

###  Video Guide how to use with Minimal API

https://user-images.githubusercontent.com/35665161/186486826-22b04394-7210-466f-b5ed-58cddc930848.mp4

###  Video Guide how to use with ASP.NET Core WebAPI
*Soon*


## this applies to: 
- ASP.NET Core WebAPI
- ASP.NET Core Minimal WebAPI 
- Blazor Server 
- Blazor WASM 
- ASP.NET Core MVC
```csharp
using Chargily.Epay;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddChargilyEpayGateway("[API_KEY]");

var app = builder.Build();

app.MapPost("/invoice",
    async ([FromBody] EpayPaymentRequest request,
        [FromServices] IChargilyEpayClient<EpayPaymentResponse, EpayPaymentRequest> chargilyClient) =>
    {
        return await chargilyClient.CreatePayment(request);
    });

app.Run();
```
### Request:
```json
{
    "invoice_number" : "321616",
    "client" : "Ahmed",
    "client_email" : "rainxh11@gmail.com",
    "amount" : 1500,
    "discount" : 5.0,
    "mode" : "EDAHABIA",
    "back_url" : "https://example.com/",
    "webhook_url" : "https://shop.com/purchase",
    "comment" : "Product Purchase"
}
```
### Response:
```json
{
    "httpStatusCode": 201,
    "responseMessage": {
        "Message": "Success"
    },
    "isSuccessful": true,
    "isRequestValid": true,
    "body": {
        "checkout_url": "https://epay.chargily.com.dz/checkout/d00c1e652200798bbc35f688b2910fa9bc6c4c30d38b51e3f4142e407fa7c141"
    },
    "createdOn": "2022-05-06T03:55:49.6527862+01:00"
}
```
### WebHook Validation:
```csharp
using Chargily.Epay;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddChargilyWebHookValidator("[APP_SECRET]");

var app = builder.Build();
app.MapPost("/webhook_endpoint",
            ([FromServices] IWebHookValidator validator, HttpRequest request, [FromBody] ChargilyWebhookRequest body) =>
            {
                var signature = request.Headers["Signature"].First();
                var isValid   = validator.Validate(signature, request.Body);
                if (isValid) return Results.Ok(body.Invoice);
                return Results.Unauthorized();
            });

app.Run();
```

### Configuration:
`API_KEY` & `APP_SECRET` can be added directly in code or from `appsettings.json` configuration file
```csharp
builder.Services.AddChargilyWebHookValidator("[APP_SECRET]");
builder.Services.AddChargilyEpayGateway("[API_KEY]");

// OR

builder.Services.AddChargilyWebHookValidator(builder.Configuration["CHARGILY_APP_SECRET"]);
builder.Services.AddChargilyEpayGateway(builder.Configuration["CHARGILY_API_KEY"]);

// OR
// Same as previous but it will be loaded automatically from appsettings.json
builder.Services.AddChargilyWebHookValidator());
builder.Services.AddChargilyEpayGateway();
```
`appsettings.json` file:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "CHARGILY_APP_SECRET": "[APP_SECRET]", // <-- APP SECRET
  "CHARGILY_API_KEY": "[API_KEY]" // <-- API KEY
}
```

## ASP.NET Core Middleware
This package provide `WebHookValidatorMiddleware` ASP.NET Core Middleware, when registered every `POST` request that have a `Signature` Http Header will be validated automatically. 
How to register the Middleware:
```csharp
using Chargily.Epay;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddChargilyEpayGateway("[API_KEY]");

builder.Services
    .AddChargilyValidatorMiddleware("[APP_SECRET]"); // WebHookValidatorMiddleware have to be registered

var app = builder.Build();

app.UseChargilyValidatorMiddleware();

app.Run();
```

# Usage with .NET MAUI
```csharp
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Chargily.Epay;

namespace MyApp
{
  public static class MauiProgram
  {
    public static MauiApp CreateMauiApp()
    {
      var builder = MauiApp.CreateBuilder();
      builder.UseMauiApp<App>();
      builder.Services.AddChargilyEpayGateway("[API_KEY]");
      return builder.Build();
    }
  }
}
```
then you can add in `ViewModels`:
```csharp
public class MainViewModel : ViewModelBase  
{  
    private ChargilyEpayClient _chargilyClient;  
    private IWebHookValidator _webhookValidator;

    public MainViewModel(ChargilyEpayClient chargilyClient)  
    {  
        _chargilyClient = chargilyClient;  
    }  
    // With Validator
    public MainViewModel(ChargilyEpayClient chargilyClient, IWebHookValidator webhookValidator)  
    {  
        _chargilyClient = chargilyClient;  
        _webhookValidator = webhookValidator;
    }  
}
```
### Note when using .NET MAUI / Xamarin:
storing sensitive `APP_SECRET` in a frontend app is not a recommended approach, you'd be better off calling a backend api to handle payment, but it's doable.
if you decide to use it in the frontend, consider storing `APP_SECRET` with [`Akavache`](https://github.com/reactiveui/Akavache) [`BlobCache.Secure`](https://github.com/reactiveui/Akavache#choose-a-location)


### This package is using [`Microsoft.Extensions.DependencyInjection`](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection) dependancy injection, so it can be used with application or framework using it.

[api-keys]: https://epay.chargily.com.dz/secure/admin/epay-api
