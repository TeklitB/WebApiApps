
using AutoMapper;
using CoinDeskWebApiApp.interfaces;
using CoinDeskWebApiApp.Mapper;
using CoinDeskWebApiApp.Models;
using CoinDeskWebApiApp.Services;
using Microsoft.Extensions.Options;

namespace CoinDeskWebApiApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.Configure<CoinDeskSettings>(
                builder.Configuration.GetSection(nameof(CoinDeskSettings)));

            builder.Services.AddSingleton<ICoinDeskSettings>(sp =>
                sp.GetRequiredService<IOptions<CoinDeskSettings>>().Value);

            builder.Services.AddScoped<IHttpClientService, HttpClientService>();
            builder.Services.AddScoped<IHttpClientFactoryService, HttpClientFactoryService>();

            // This is necessary to use IHttpClientFactory;
            builder.Services.AddHttpClient();

            // Named client configuration;
            // It’s also possible to have named clients, you can do that by registering it via DI (you can register how many you want).
            // This is how you can configure.
            builder.Services.AddHttpClient("CoinDeskSettings", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("CoinDeskSettings:Url"));
            });           


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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