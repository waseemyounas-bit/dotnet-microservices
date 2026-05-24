using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Allow self-signed certificates for local HTTPS services
builder.Services.AddHttpClient("AllowSelfSigned")
    .ConfigurePrimaryHttpMessageHandler(() =>
        new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });

// Add Ocelot with SSL bypass
builder.Services.AddOcelot()
    .AddDelegatingHandler<IgnoreSslHandler>(true);

// Load ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.MapControllers();

// Run Ocelot middleware
await app.UseOcelot();

app.Run();

// Delegating handler to ignore SSL validation
public class IgnoreSslHandler : DelegatingHandler
{
    public IgnoreSslHandler()
    {
        InnerHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    }
}