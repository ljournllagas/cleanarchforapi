using Application.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Infrastructure.Logging
{
    public class UseSerilog
    {
        public UseSerilog(IConfiguration configuration)
        {
            Configuration = configuration;

            Set();
        }

        private void Set()
        {
            LogEventLevel minLevel = GetLogEventLevel();

            Trace.TraceInformation("Logging: minimal level is {0}.", minLevel);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(minLevel)
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .Filter
                   .ByExcluding(logEvent =>
                       logEvent.Exception != null &&
                       logEvent.Exception.GetType() == typeof(RestException)
                    )
                .WriteTo.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                .WriteTo.File(GetSerilogFileConfig(), rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public IConfiguration Configuration{ get; set; }

        private static LogEventLevel GetLogEventLevel()
        {
            LogEventLevel defaultLevel = LogEventLevel.Information;
            var logEventLevel = Environment.GetEnvironmentVariable("LOG_LEVEL");

            if (!string.IsNullOrEmpty(logEventLevel))
            {
                LogEventLevel level;
                if (!Enum.TryParse(logEventLevel, out level))
                {
                    Trace.TraceWarning("Error parsing Serilog.LogEventLevel. Defaulting to {0}", defaultLevel);
                    level = defaultLevel;
                }

                return level;
            }

            return defaultLevel;
        }

        private  string GetSerilogFileConfig()
        {
            return $"{Configuration["Serilog:TextFileDir"]}{Configuration["Serilog:LogName"]}.txt";
        }
    }
}
