namespace AgriToolWebApi.Common.Interfaces.Uuid
{
    /// <summary>
    /// UUIDを生成する
    /// </summary>
    public interface IUuidGenerator
    {
        /// <summary>
        /// UUIDを生成する
        /// </summary>
        /// <returns>UUID</returns>
        Guid GenerateUuid();
    }
}
