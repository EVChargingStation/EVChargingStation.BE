using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot services
builder.Services.AddOcelot();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add health checks
builder.Services.AddHealthChecks();

// Configure Ocelot from ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", false, true);

var app = builder.Build();

app.UseCors("CorsPolicy");

// Add Ocelot middleware to pipeline
app.UseRouting();

// Map root endpoint
app.MapGet("/", () => "EV Charging Station API Gateway");

// Map health check endpoint
app.MapHealthChecks("/health");

// Configure Ocelot
await app.UseOcelot();

app.Run();