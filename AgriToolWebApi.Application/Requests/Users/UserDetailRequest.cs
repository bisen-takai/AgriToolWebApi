using System.ComponentModel.DataAnnotations;

namespace AgriToolWebApi.Application.Requests.Users
{
    public class UserDetailRequest
    {
        /// <summary>
        /// ユーザID
        /// </summary>
        [Required(ErrorMessage = "ユーザを認識できません。")]
        public int Id { get; set; }
    }
}
