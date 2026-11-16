using Microsoft.Extensions.DependencyInjection;
using Shelves.Services.ClientuseCases;

namespace Shelves.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IClientServce, ClientService>();

        return services;
    }
}
