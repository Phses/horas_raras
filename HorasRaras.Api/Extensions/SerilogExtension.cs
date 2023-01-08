using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace HorasRaras.Api.Logs
{
    public static class SerilogExtension
    {
   
        public static void AddSerilogApi(IConfiguration configuration)
        {
            var configuration2 = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("horas_raras_04");
            const string nomeTabela = "Logs";
            const int limite = 50;

            var options = new ColumnOptions();

                options.Store.Remove(StandardColumn.Properties);
                options.Store.Remove(StandardColumn.MessageTemplate);
                options.Store.Remove(StandardColumn.Level);

            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information)
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .ReadFrom.Configuration(configuration)
                    .Enrich.WithProperty("ApplicationName", $"API Horas_Raras - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Async(wt => wt.Console(
                                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}\n"))
                    .WriteTo.Async(wt => wt.File(@".\Log\log.txt", rollingInterval: RollingInterval.Month))
                    .WriteTo.Async(wt => wt.MSSqlServer(
                                  connectionString: connectionString,
                                  sinkOptions: new MSSqlServerSinkOptions
                                  {
                                      AutoCreateSqlTable = true,
                                      TableName = nomeTabela,
                                      BatchPostingLimit = limite
                                  },
                                  columnOptions: options,                    
                                  appConfiguration: configuration2
                                  ))
                        .CreateLogger();


        }
    }
}
