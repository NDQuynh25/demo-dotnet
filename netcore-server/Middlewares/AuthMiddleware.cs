using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace netcore_server.Middlewares;

public static class AuthMiddleware
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var secret = configuration["Jwt:Secret"] ?? "THIS_IS_MY_SUPER_SECRET_KEY_123456";
        var key = Encoding.UTF8.GetBytes(secret);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),

                // map claim name
                NameClaimType = ClaimTypes.NameIdentifier
            };

            options.Events = new JwtBearerEvents
            {
                // Lấy token từ cookie
                OnMessageReceived = context =>
                {
                    var token = context.Request.Cookies["token"];

                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Token = token;
                    }

                    return Task.CompletedTask;
                },

             
                OnTokenValidated = context =>
                {
                    var userId = context.Principal?.FindFirst("id")?.Value;

                    if (string.IsNullOrEmpty(userId))
                    {
                        context.Fail("Token không hợp lệ");
                    }

                    return Task.CompletedTask;
                },

                // Khi authentication fail
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("JWT Error: " + context.Exception.Message);
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization();

        return services;
    }
}