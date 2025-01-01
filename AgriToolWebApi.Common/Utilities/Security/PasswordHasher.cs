using AgriToolWebApi.Common.Interfaces.Security;
using System.Security.Cryptography;
using System.Text;

namespace AgriToolWebApi.Common.Utilities.Security
{
    /// <summary>
    /// ハッシュ値のパスワードに関するクラス
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// パスワードとソルト値を元にハッシュ値のパスワードを生成する
        /// </summary>
        /// <param name="password">パスワード</param>
        /// <param name="salt">ソルト値</param>
        /// <returns>ハッシュ値のパスワード</returns>
        public string HashPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("パスワードが指定されていません。");
            }

            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new ArgumentException("ソルトが指定されていません。");
            }

            try
            {
                using (var sha256 = SHA256.Create())
                {
                    var combined = password + salt;
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                    return Convert.ToBase64String(hashBytes);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("パスワードのハッシュ化に失敗しました。");
            }
        }

        /// <summary>
        /// パスワードとソルト値から生成したハッシュ値と引数のハッシュ値が一致しているかを確認する
        /// </summary>
        /// <param name="password">パスワード</param>
        /// <param name="salt">ソルト値</param>
        /// <param name="hash">ハッシュ値のパスワード</param>
        /// <returns>true：一致、false：不一致</returns>
        public bool VerifyPassword(string password, string salt, string hash)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("パスワードが指定されていません。");
            }

            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new ArgumentException("ソルトが指定されていません。");
            }

            if (string.IsNullOrWhiteSpace(hash))
            {
                throw new ArgumentException("ハッシュ値が指定されていません。");
            }

            var computedHash = HashPassword(password, salt);
            return computedHash == hash;
        }
    }
}
