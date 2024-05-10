using BookMarketsAPI.RegisterExtensions;
using System.Text.Json.Serialization;

namespace BookMarketsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            _ = configuration.AddJsonFile("appsettings.json");

            builder.Services
                .AddSwaggerGen()
                .AddSwaggerConf()
                .AddConfiguredAutorization(configuration)
                .AddHandlers()
                .AddProcessors()
                .AddStorage(configuration)
                .AddEndpointsApiExplorer()
                .AddControllers().AddJsonOptions(opts =>
                {
                    var enumConverter = new JsonStringEnumConverter();
                    opts.JsonSerializerOptions.Converters.Add(enumConverter);
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
