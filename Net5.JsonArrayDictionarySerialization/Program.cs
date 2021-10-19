﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Net5.ConfigurationExtension.JsonArrayDictionarySerialization
{
    public class Program
    {
        static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(s =>
                {
                    ConfigureServices(s, "local.settings.json");
                })
                .Build();

            host.Run();
            Main();
        }

        public static void ConfigureServices(IServiceCollection s, string settingsFileName)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(settingsFileName, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var jsonArray = config.GetSection("Array").Get<Dictionary<string, string>>();
            s.AddSingleton(new DummyArray
            {
                Dictionary = jsonArray
            });

            var jsonObject = config.GetSection("Object").Get<Dictionary<string, string>>();
            s.AddSingleton(new DummyObject
            {
                Dictionary = jsonObject
            });
        }
    }

    public class DummyArray
    {
        public Dictionary<string, string> Dictionary { get; set; }
    }

    public class DummyObject
    {
        public Dictionary<string, string> Dictionary { get; set; }
    }
}