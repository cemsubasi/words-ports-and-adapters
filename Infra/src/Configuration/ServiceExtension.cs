using System.Threading.RateLimiting;
using Domain.Account;
using Domain.Account.Port;
using Domain.Category.Port;
using Domain.Comment;
using Domain.File;
using Domain.File.Port;
using Domain.Post;
using Domain.Post.Port;
using Domain.SuperAccount;
using Domain.SuperAccount.Port;
using Infra.Account;
using Infra.Category.Adapter;
using Infra.File.Adapter;
using Infra.Post.Adapter;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Infra.Health;

using Domain.Identity.Port;
using Infra.Identity.Adapter;

namespace Infra.Configurations;

public static class ServiceExtension {
  public static IServiceCollection AddServices(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(IUserSession), typeof(UserSession), lifetime));
    services.Add(new ServiceDescriptor(typeof(AccountPort), typeof(AccountAdapter), lifetime));
    services.Add(new ServiceDescriptor(typeof(SuperAccountPort), typeof(SuperAccountAdapter), lifetime));
    services.Add(new ServiceDescriptor(typeof(PostPort), typeof(PostAdapter), lifetime));
    services.Add(new ServiceDescriptor(typeof(CategoryPort), typeof(CategoryAdapter), lifetime));
    services.Add(new ServiceDescriptor(typeof(FilePort), typeof(FileAdapter), lifetime));
    services.Add(new ServiceDescriptor(typeof(IdentityPort), typeof(IdentityAdapter), lifetime));

    return services;
  }

  public static IServiceCollection AddValidations(this IServiceCollection services, ServiceLifetime lifetime) {
    /* services.Add(new ServiceDescriptor(typeof(IValidator<AccountCreateRequest>), typeof(AccountCreateRequestValidator), lifetime)); */
    /* services.Add(new ServiceDescriptor(typeof(IValidator<WordModel>), typeof(WordValidator), lifetime)); */
    /* services.Add(new ServiceDescriptor(typeof(IValidator<AnswerModel>), typeof(AnswerValidator), lifetime)); */
    /* services.Add(new ServiceDescriptor(typeof(IValidator<CategoryModel>), typeof(CategoryValidator), lifetime)); */

    return services;
  }

  public static IServiceCollection AddAccountUseCaseHandlers(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(AccountCreateUseCaseHandler), typeof(AccountCreateUseCaseHandler), lifetime));
    services.Add(new ServiceDescriptor(typeof(AccountRetrieveUseCaseHandler), typeof(AccountRetrieveUseCaseHandler), lifetime));
    services.Add(new ServiceDescriptor(typeof(AccountAuthenticateUseCaseHandler), typeof(AccountAuthenticateUseCaseHandler), lifetime));

    return services;
  }

  public static IServiceCollection AddSuperAccountUseCaseHandlers(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(SuperAccountCreateUseCaseHandler), typeof(SuperAccountCreateUseCaseHandler), lifetime));
    services.Add(new ServiceDescriptor(typeof(SuperAccountRetrieveUseCaseHandler), typeof(SuperAccountRetrieveUseCaseHandler), lifetime));
    services.Add(new ServiceDescriptor(typeof(SuperAccountAuthenticateUseCaseHandler), typeof(SuperAccountAuthenticateUseCaseHandler), lifetime));

    return services;
  }

  public static IServiceCollection AddPostUseCaseHandlers(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(CreatePostUseCaseHandler), typeof(CreatePostUseCaseHandler), lifetime));
    services.Add(new ServiceDescriptor(typeof(RetrievePostUseCaseHandler), typeof(RetrievePostUseCaseHandler), lifetime));
    services.Add(new ServiceDescriptor(typeof(AddCommentUseCaseHandler), typeof(AddCommentUseCaseHandler), lifetime));
    services.Add(new ServiceDescriptor(typeof(RetrieveCommentUseCaseHandler), typeof(RetrieveCommentUseCaseHandler), lifetime));

    return services;
  }

  public static IServiceCollection AddFileUseCaseHandlers(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(CreateFileUseCaseHandler), typeof(CreateFileUseCaseHandler), lifetime));
    services.Add(new ServiceDescriptor(typeof(RetrieveFileUseCaseHandler), typeof(RetrieveFileUseCaseHandler), lifetime));

    return services;
  }

  public static IServiceCollection AddUtils(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(IJwtProvider), typeof(JwtProvider), lifetime));
    services.Add(new ServiceDescriptor(typeof(JwtOptions), typeof(JwtOptions), lifetime));
    services.Add(new ServiceDescriptor(typeof(SentryOptions), typeof(SentryOptions), lifetime));
    services.Add(new ServiceDescriptor(typeof(DBOptions), typeof(DBOptions), lifetime));

    return services;
  }

  public static IServiceCollection AddRateLimiter(this IServiceCollection services) {
    services.AddRateLimiter(_ => _
      .AddFixedWindowLimiter(policyName: "fixed", options => {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
      }));

    return services;
  }

  public static AuthenticationBuilder AddAuthentication(this IServiceCollection services, string[] args, IConfigurationRoot configuration) {
    return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options => {
        options.TokenValidationParameters = JwtProvider.GetValidationParameters(args, configuration);
        options.Events = new JwtBearerEvents {
          OnAuthenticationFailed = context => {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException)) {
              context.Response.Headers.Append("Token-Expired", "true");
            }

            return Task.CompletedTask;
          }
        };
      });
  }

  public static IWebHostBuilder AddSentry(this IWebHostBuilder builder, IConfigurationRoot configuration) {
    return builder.UseSentry(o => {
      var options = configuration.GetSection("Sentry").Get<Infra.Configurations.SentryOptions>();
      o.Dsn = options.Dsn;
      o.Debug = options.Debug;
      o.TracesSampleRate = options.TracesSampleRate;
    });
  }

  public static IServiceCollection AddSwagger(this IServiceCollection services) {
    services.AddEndpointsApiExplorer();

    services.AddSwaggerGen(options => {
      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
      });
      options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
      });
    });

    return services;
  }


  public static IServiceCollection AddHealthCheck(this IServiceCollection services, ServiceLifetime lifetime) {
    services.AddHealthChecks()
    .AddCheck<MySqlHealthCheck>("MySqlHealthCheck", HealthStatus.Unhealthy, ["mysql"]);

    return services;
  }
}

