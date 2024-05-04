using Logic.Abstractions.Autorization;
using Logic.Autorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Autorization;
using System.Text;

namespace BookMarketsAPI.RegisterExtensions;

public static class AutorizationExtension
{
    public static IServiceCollection AddConfiguredAutorization(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        _ = services
            .AddSingleton<
                IJwtTokenConstructor<CustomerAutorizationData>, 
                CustomerJwtTokenConstructor>()

            .AddSingleton<
                IJwtTokenConstructor<EmployeeAutorizationData>,
                EmployeeJwtTokenConstructor>()

            .AddSingleton<IJwtTokenDeconstructor, JwtTokenDeconstructor>()
            .AddScoped<IRuleChecker, RuleChecker>()

            .Configure<AuthConfiguration>(
                configuration.GetSection(nameof(AuthConfiguration)))

            .AddSingleton<
                IValidateOptions<AuthConfiguration>,
                AuthConfigurationValidator>()

            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer =
                        configuration.GetSection(
                            nameof(AuthConfiguration)).GetSection("Issuer").Value
                            ?? throw new ArgumentException("Issuer"),
                    ValidateAudience = true,
                    ValidAudience =
                        configuration.GetSection(
                            nameof(AuthConfiguration)).GetSection("Audience").Value
                            ?? throw new ArgumentException("Audience"),
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(
                            configuration.GetSection(
                                nameof(AuthConfiguration)).GetSection("Key").Value
                                ?? throw new ArgumentException("Key"))),
                    ValidateIssuerSigningKey = true
                };
            });

        return services;
    }
}
