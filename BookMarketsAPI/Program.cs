using BookMarketsAPI.RegisterExtensions;

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
                .AddSecuritySwaggerGen()
                .AddConfiguredAutorization(configuration)
                .AddHandlers()
                .AddProcessors()
                .AddStorage(configuration)
                .AddEndpointsApiExplorer()
                .AddControllers(); ;

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
