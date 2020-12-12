using Microsoft.Extensions.DependencyInjection;

namespace Rover.UnitTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Plateau>();
        }
    }
}