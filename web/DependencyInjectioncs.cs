using Morn_Agi.Extensions;

namespace Morn_Agi
{
    public static class DependencyInjectioncs
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddExceptionHandler<CustomExceptionHandler>();

            return services;
        }
    }
}
