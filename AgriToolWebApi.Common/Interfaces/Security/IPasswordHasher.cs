namespace AgriToolWebApi.Common.Interfaces.Security
{
    /// <summary>
    /// ハッシュ値のパスワードに関するクラス
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// パスワードとソルト値を元にハッシュ値のパスワードを生成する
        /// </summary>
        /// <param name="password">パスワード</param>
        /// <param name="salt">ソルト値</param>
        /// <returns>ハッシュ値のパスワード</returns>
        string HashPassword(string password, string salt);

        /// <summary>
        /// パスワードとソルト値から生成したハッシュ値と引数のハッシュ値が一致しているかを確認する
        /// </summary>
        /// <param name="password">パスワード</param>
        /// <param name="salt">ソルト値</param>
        /// <param name="hash">ハッシュ値のパスワード</param>
        /// <returns>true：一致、false：不一致</returns>
        bool VerifyPassword(string password, string salt, string hash);
    }
}
