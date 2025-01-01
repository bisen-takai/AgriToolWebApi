using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriToolWebApi.Infrastructure.Persistence.Entities
{
    /// <summary>
    /// Usersテーブル
    /// </summary>
    [Table("users")]
    public class UserEntity
    {
        /// <summary>
        /// 自動インクリメントID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int Id { get; set; }

        /// <summary>
        /// ユーザUUID
        /// </summary>
        [Column("user_uuid")]
        public Guid Uuid { get; set; }

        /// <summary>
        /// ログインID
        /// </summary>
        [Column("user_login_id")]
        public string LoginId { get; set; }

        /// <summary>
        /// ハッシュ化したパスワード
        /// </summary>
        [Column("user_password")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// ソルト値
        /// </summary>
        [Column("user_salt")]
        public string Salt { get; set; }

        /// <summary>
        /// 氏名
        /// </summary>
        [Column("user_full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        [Column("user_phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 権限ID
        /// </summary>
        [Column("privilege_id")]
        public int PrivilegeId { get; set; }

        /// <summary>
        /// カラーID
        /// </summary>
        [Column("color_id")]
        public int ColorId { get; set; }

        /// <summary>
        /// 登録日時
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 最終更新日時
        /// </summary>
        [Column("last_updated_at")]
        public DateTime LastUpdatedAt { get; set; }
    }
}
