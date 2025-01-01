using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriToolWebApi.Application.DTOs
{
    /// <summary>
    /// ユーザ情報レスポンスデータ
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// ユーザ情報ID(主キー)
        /// </summary>
        public int Id;

        /// <summary>
        /// ログインID
        /// </summary>
        public string LoginId;

        /// <summary>
        /// 氏名
        /// </summary>
        public string FullName;

        /// <summary>
        /// 電話番号
        /// </summary>
        public string PhoneNumber;

        /// <summary>
        /// 権限ID
        /// </summary>
        public int PrivilegeId;

        /// <summary>
        /// カラーID
        /// </summary>
        public int ColorId;
    }
}
