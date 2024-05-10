using Microsoft.OpenApi.Models;

namespace BookMarketsAPI.RegisterExtensions;

public static class SwaggerConfExtension
{
    public static IServiceCollection AddSwaggerConf(this IServiceCollection services)
        => services
            .AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                c.CustomSchemaIds(x => x.FullName);

                c.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "full-date" });
                c.MapType<TimeOnly>(() => new OpenApiSchema { Type = "string", Format = "time" });

            });
}
