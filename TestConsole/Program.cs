using System;
using Microsoft.Extensions.DependencyInjection;
using Net5.JsonArrayDictionarySerialization;

namespace TestConsole
{
    class Program
    {
        static IServiceCollection services = new ServiceCollection();

        static void Main()
        {
            Net5.JsonArrayDictionarySerialization.Program.ConfigureServices(services, "test.settings.json");
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<DummyArray>();
            serviceProvider.GetService<DummyObject>();
        }
    }
}
