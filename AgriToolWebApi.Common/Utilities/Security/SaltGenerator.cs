using AgriToolWebApi.Common.Interfaces.Security;
using System.Security.Cryptography;

namespace AgriToolWebApi.Common.Utilities.Security
{
    /// <summary>
    /// ソルト値を生成する
    /// </summary>
    public class SaltGenerator : ISaltGenerator
    {
        /// <summary>
        /// ソルト値を生成する
        /// </summary>
        /// <returns>ソルト値</returns>
        public string GenerateSalt()
        {
            byte[] salt = new byte[16];

            try
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                return Convert.ToBase64String(salt);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentException("ソルト生成エラー");
            }
        }
    }
}
