using System.Text;
using UserApi.Repositories;
using Microsoft.IdentityModel.Tokens;
using UserApi.Repositories.Implementations;
using UserApi.Repositories.Interfaces;
using UserApi.Services.Interfaces;
using UserService = UserApi.Services.Implementations.UserService.UserService;

namespace UserApi.Configs;

public static class DependencyInjectionExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAuthentication().AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    configuration.GetSection("AppSettings:Token").Value!))
            };
        });
    }

    public static void RegisterServicesAndRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>().AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>();
        services.AddScoped<IUserRepository, UserRepository>().AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>();
    }
}