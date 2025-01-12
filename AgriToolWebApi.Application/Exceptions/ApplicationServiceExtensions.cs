using AgriToolWebApi.Application.Interfaces;
using AgriToolWebApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AgriToolWebApi.Application.Exceptions
{
    /// <summary>
    /// Application層のサービスを登録する拡張メソッド
    /// </summary>
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
