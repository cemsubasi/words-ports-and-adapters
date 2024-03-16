using Domain.Account;
using Domain.Account.Port;
using Domain.Post;
using Domain.Post.Port;
using FluentValidation;
using Infra;
using Infra.Account;
using Infra.Account.Validator;
using Infra.Controllers.Account;
using Infra.Post.Adapter;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Infra.Configurations;

public static class ServiceExtension {
  public static IServiceCollection AddServices(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(IUserSession), typeof(UserSession), lifetime));
    services.Add(new ServiceDescriptor(typeof(AccountPort), typeof(AccountAdapter), lifetime));
    services.Add(new ServiceDescriptor(typeof(PostPort), typeof(PostAdapter), lifetime));

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

  public static IServiceCollection AddPostUseCaseHandlers(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(CreatePostUseCaseHandler), typeof(CreatePostUseCaseHandler), lifetime));
    services.Add(new ServiceDescriptor(typeof(RetrievePostUseCaseHandler), typeof(RetrievePostUseCaseHandler), lifetime));

    return services;
  }
  public static IServiceCollection AddUtils(this IServiceCollection services, ServiceLifetime lifetime) {
    services.Add(new ServiceDescriptor(typeof(IJwtProvider), typeof(JwtProvider), lifetime));
    services.Add(new ServiceDescriptor(typeof(JwtOptions), typeof(JwtOptions), lifetime));
    services.Add(new ServiceDescriptor(typeof(SentryOptions), typeof(SentryOptions), lifetime));
    services.Add(new ServiceDescriptor(typeof(DBOptions), typeof(DBOptions), lifetime));

    return services;
  }

  public static AuthenticationBuilder AddAuthentication(this IServiceCollection services, string[] args, IConfigurationRoot configuration) {
    return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options => {
        options.TokenValidationParameters = JwtProvider.GetValidationParameters(args, configuration);
        options.Events = new JwtBearerEvents {
          OnAuthenticationFailed = context => {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException)) {
              context.Response.Headers.Add("Token-Expired", "true");
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
}

