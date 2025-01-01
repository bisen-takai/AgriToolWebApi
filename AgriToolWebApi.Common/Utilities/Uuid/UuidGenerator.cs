using AgriToolWebApi.Common.Interfaces.Uuid;

namespace AgriToolWebApi.Common.Utilities.Uuid
{
    /// <summary>
    /// UUIDを生成する
    /// </summary>
    public class UuidGenerator : IUuidGenerator
    {
        /// <summary>
        /// UUIDを生成する
        /// </summary>
        /// <returns>UUID</returns>
        public Guid GenerateUuid()
        {
            try
            {
                return Guid.NewGuid();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("UUIDの生成に失敗しました。");
            }
        }
    }
}
