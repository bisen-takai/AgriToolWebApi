using System.ComponentModel.DataAnnotations;

namespace AgriToolWebApi.Application.Requests.Users
{
    public class UserUpdateRequest
    {
        /// <summary>
        /// ユーザID
        /// </summary>
        [Required(ErrorMessage = "ユーザを認識できません。")]
        public int Id { get; set; }

        /// <summary>
        /// ログインID
        /// </summary>
        [Required(ErrorMessage = "ログインIDは必須です。")]
        [StringLength(60)]
        public string LoginId { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        [Required(ErrorMessage = "パスワードは必須です。")]
        [MinLength(8, ErrorMessage = "パスワードは8文字以上必要です。")]
        [MaxLength(64, ErrorMessage = "パスワードは最大64文字までです。")]
        public string Password { get; set; }

        /// <summary>
        /// 氏名
        /// </summary>
        [StringLength(60)]
        public string FullName { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "電話番号は10桁または11桁の数字でなければなりません。")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 権限ID
        /// </summary>
        [Range(1, 3, ErrorMessage = "権限IDは1〜3の範囲で指定してください。")]
        public int PrivilegeId { get; set; }

        /// <summary>
        /// カラーID
        /// </summary>
        public int ColorId { get; set; }
    }
}
