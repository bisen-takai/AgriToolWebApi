namespace AgriToolWebApi.Application.Requests
{
    /// <summary>
    /// ユーザ情報登録リクエストデータ
    /// </summary>
    public class UserCreateRequest
    {
        /// <summary>
        /// ログインID
        /// </summary>
        public string LoginId { get; set; }
        
        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 氏名
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 権限ID
        /// </summary>
        public int PrivilegeId { get; set; }

        /// <summary>
        /// カラーID
        /// </summary>
        public int ColorId { get; set; }
    }
}
