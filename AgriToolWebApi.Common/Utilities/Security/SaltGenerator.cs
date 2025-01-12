using AgriToolWebApi.Common.Interfaces.Security;
using AgriToolWebApi.Common.Settings;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace AgriToolWebApi.Common.Utilities.Security
{
    /// <summary>
    /// ソルト値を生成する
    /// </summary>
    public class SaltGenerator : ISaltGenerator
    {
        private readonly SecuritySettings _settings;

        public SaltGenerator(IOptions<SecuritySettings> options)
        {
            _settings = options.Value;
        }

        /// <summary>
        /// ソルト値を生成する
        /// </summary>
        /// <returns>ソルト値</returns>
        public string GenerateSalt()
        {
            byte[] salt = new byte[_settings.SaltSize];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }
    }
}
