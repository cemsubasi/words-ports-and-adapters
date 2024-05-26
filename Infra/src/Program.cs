using System.Text.Json.Serialization;
using Domain.Account.Entity;
using Domain.SuperAccount.Entity;
using Infra;
using Infra.Configurations;
using Infra.Context;
using Infra.Health;
using Infra.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting.Server;
using Serilog;
using Serilog.Events;

var configuration = new ConfigurationBuilder()
  .AddCommandLine(args)
  .Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
  .AddCommandLine(args)
  .AddJsonFile("appsettings.json");

if (!string.IsNullOrEmpty(configuration["environment"])) {
  configurationBuilder.AddJsonFile($"appsettings.{configuration["environment"]}.json");
}

configuration = configurationBuilder.Build();

Log.Information("Environment is {0}", configuration["environment"]);

// var config = TypeAdapterConfig.GlobalSettings;
// config.AllowImplicitDestinationInheritance = true;

builder.Services.AddControllers(options => {
  options.Filters.Add<HttpResponseExceptionFilter>();
});

builder.Services.AddControllers()
  .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAuthentication(args, configuration);
builder.Services.AddAuthorization(options => {
  options.AddPolicy(nameof(SuperAccountEntity), policy => policy.RequireRole(typeof(SuperAccountEntity).Name));
  options.AddPolicy(nameof(AccountEntity), policy => policy.RequireRole(typeof(AccountEntity).Name));
});
builder.Services.Configure<Infra.Configurations.SentryOptions>(configuration.GetSection("Sentry"));
builder.Services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
builder.Services.Configure<DBOptions>(configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddUtils(ServiceLifetime.Singleton);
builder.Services.AddServices(ServiceLifetime.Scoped);
builder.Services.AddAccountUseCaseHandlers(ServiceLifetime.Scoped);
builder.Services.AddSuperAccountUseCaseHandlers(ServiceLifetime.Scoped);
builder.Services.AddCategoryUseCaseHandlers(ServiceLifetime.Scoped);
builder.Services.AddQuestionUseCaseHandlers(ServiceLifetime.Scoped);
builder.Services.AddFileUseCaseHandlers(ServiceLifetime.Scoped);
builder.Services.AddRateLimiter();
builder.Services.AddHealthCheck(ServiceLifetime.Singleton);
builder.Services.AddSwagger();
builder.Services.AddDbContext<MainDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Host.UseSerilog();
builder.WebHost.AddSentry(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();

  app.Lifetime.ApplicationStarted.Register(() => {
    var server = app.Services.GetRequiredService<IServer>();
    var addressesFeature = server.Features.Get<Microsoft.AspNetCore.Hosting.Server.Features.IServerAddressesFeature>();
    if (addressesFeature != null) {
      foreach (var address in addressesFeature.Addresses) {
        Log.Write(LogEventLevel.Information, "Swagger: {0}{1}", address, "/swagger/index.html");
      }
    }
  });
}

app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SessionMiddleware>();
// app.UseMiddleware<IdentityMiddleware>();

app.UseHttpsRedirection();
app.UseCors(x => {
  x.WithOrigins("https://words.localx.host").AllowCredentials();
  // x.WithOrigins(app.Environment.IsDevelopment() ? "localhost:3000" : "words.localx.host").AllowCredentials();
});


app.MapControllers().RequireRateLimiting("fixed");
app.MapHealthChecks("/health", new HealthCheckOptions {
  ResponseWriter = healthChecker.WriteResponse
});

app.Run();
