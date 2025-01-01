namespace AgriToolWebApi.Common.Interfaces.Security
{
    /// <summary>
    /// ソルト値を生成する
    /// </summary>
    public interface ISaltGenerator
    {
        /// <summary>
        /// ソルト値を生成する
        /// </summary>
        /// <returns>ソルト値</returns>
        string GenerateSalt();
    }
}
