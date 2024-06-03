
using HolyQuran.Infrastructure.Services;
using HolyQuran.Infrastructure.Services.Interfaces;
using HolyQuran.Infrastructure.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace HolyQuran.infrastructure.Extentions
{
    public static class ContainerRegistryExtension
    {
        public static IServiceCollection Registerservices(this IServiceCollection services)
        {
            services.AddScoped<IInterfaceServices, InterfaceServices>();

            return services;
        }
    }
}
