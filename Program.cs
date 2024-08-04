using Microsoft.AspNetCore.SignalR;
using signalRtc.hubs;

namespace signalRtc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            Console.WriteLine("gggg");
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200", "http://192.168.100.67:4200") // Adjust with your client's URL
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });
            builder.Services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();
            });


            builder.WebHost.ConfigureKestrel(options =>
            {
                // Specify the IP address and port here
                options.Listen(System.Net.IPAddress.Parse("192.168.100.67"), 7220); // Replace with your IP and port
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();

            // Use CORS before UseAuthorization and UseEndpoints
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapHub<SignalRtcHub>("/signalingHub");

            app.MapControllers();

            app.Run();
        }
    }
}