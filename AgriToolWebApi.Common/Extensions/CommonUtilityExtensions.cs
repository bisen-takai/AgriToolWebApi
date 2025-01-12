using AgriToolWebApi.Common.Interfaces.Security;
using AgriToolWebApi.Common.Interfaces.Uuid;
using AgriToolWebApi.Common.Utilities.Security;
using AgriToolWebApi.Common.Utilities.Uuid;
using Microsoft.Extensions.DependencyInjection;

namespace AgriToolWebApi.Common.Extensions
{
    /// <summary>
    /// Common層のサービスを登録する拡張メソッド
    /// </summary>
    public static class CommonUtilityExtensions
    {
        public static IServiceCollection AddCommonUtilities(this IServiceCollection services)
        {
            services.AddScoped<IUuidGenerator, UuidGenerator>();
            services.AddScoped<ISaltGenerator, SaltGenerator>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            return services;
        }

    }
}
