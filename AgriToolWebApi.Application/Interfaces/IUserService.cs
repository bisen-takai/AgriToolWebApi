using AgriToolWebApi.Application.DTOs;
using AgriToolWebApi.Application.Requests.Users;

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

        Task<UserDto> UpdateUserAsync(UserUpdateRequest request);

        Task<bool> DeleteUserAsync(UserDeleteRequest request);

        Task<UserDto> GetUserByIdAsync(UserDetailRequest request);

        Task<IEnumerable<UserDto>> GetUserListAsync(UserSearchRequest request);

        Task<UserDto> AuthenticateAsync(UserLoginRequest request);
    }
}
