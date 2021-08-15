using System;
using Microsoft.Extensions.Configuration;

namespace BPDotNet.Scripts
{
    public static class ConfigurationFactory
    {
        public static IConfigurationRoot Create()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .AddEnvironmentVariables();

            IConfigurationRoot configurationRoot = builder.Build();
            
            return configurationRoot;
        }
    }
}