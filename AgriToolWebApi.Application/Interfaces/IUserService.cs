using AgriToolWebApi.Application.DTOs;
using AgriToolWebApi.Application.Requests;

namespace AgriToolWebApi.Application.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// ユーザ情報を登録する
        /// </summary>
        /// <param name="request">ユーザ情報のリクエストデータ</param>
        /// <returns>ユーザ情報のレスポンスデータ</returns>
        Task<UserDto> CreateUserAsync(UserCreateRequest request);
    }
}
