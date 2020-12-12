using System;
using Microsoft.Extensions.DependencyInjection;

namespace Rover
{
    public class App
    {
        public void Run(IServiceProvider services)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            
            var plato =  provider.GetRequiredService<Plateau>();
            var resp = plato.Initialize("4 4\n1 1 E\nLLMM\n2 2 W\nRRMRM").RunScenario();
            Console.WriteLine(resp);
        }
    }
}