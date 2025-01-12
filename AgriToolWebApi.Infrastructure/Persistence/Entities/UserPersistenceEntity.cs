using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriToolWebApi.Infrastructure.Persistence.Entities
{
    /// <summary>
    /// Usersテーブル
    /// </summary>
    [Table("users")]
    public class UserPersistenceEntity
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
        [Required]
        [StringLength(60)]
        public string LoginId { get; set; }

        /// <summary>
        /// ハッシュ化したパスワード
        /// </summary>
        [Column("user_password")]
        [Required]
        [StringLength(64, MinimumLength = 64)]
        public string PasswordHash { get; set; }

        /// <summary>
        /// ソルト値
        /// </summary>
        [Column("user_salt")]
        [Required]
        [StringLength(24, MinimumLength = 24)]
        public string Salt { get; set; }

        /// <summary>
        /// 氏名
        /// </summary>
        [Column("user_full_name")]
        [MaxLength(80)]
        public string? FullName { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        [Column("user_phone_number")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "電話番号は10桁または11桁の数字でなければなりません")]
        [StringLength(11)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// 権限ID
        /// </summary>
        [Column("privilege_id")]
        [Range(1, 3)]
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
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 最終更新日時
        /// </summary>
        [Column("last_updated_at")]
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
