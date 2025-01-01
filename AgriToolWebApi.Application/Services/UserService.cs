using AgriToolWebApi.Application.DTOs;
using AgriToolWebApi.Application.Interfaces;
using AgriToolWebApi.Application.Requests;
using AgriToolWebApi.Common.Interfaces.Security;
using AgriToolWebApi.Common.Interfaces.Uuid;
using AgriToolWebApi.Infrastructure.Persistence.Contexts;
using AgriToolWebApi.Infrastructure.Persistence.Entities;

namespace AgriToolWebApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IUuidGenerator _uuidGenerator;
        private readonly ISaltGenerator _saltGenerator;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(AppDbContext context, IUuidGenerator uuidGenerator, ISaltGenerator saltGenerator, IPasswordHasher passwordHasher)
        {
            _context = context;
            _uuidGenerator = uuidGenerator;
            _saltGenerator = saltGenerator;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// ユーザ情報を登録する
        /// </summary>
        /// <param name="request">ユーザ情報のリクエストデータ</param>
        /// <returns>ユーザ情報のレスポンスデータ</returns>
        public async Task<UserDto> CreateUserAsync(UserCreateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.LoginId))
            {
                throw new ArgumentException("ログインIDは必須です。");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("パスワードは必須です。");
            }

            if ((request.PrivilegeId < 1) && (request.PrivilegeId > 3))
            {
                throw new AggregateException("権限が不正です。");
            }

            if ((request.ColorId < 1) && (request.ColorId > 3))
            {
                throw new AggregateException("カラーが不正です。");
            }

            try
            {
                var salt = _saltGenerator.GenerateSalt();
                var hashedPassword = _passwordHasher.HashPassword(request.Password, salt);
                var uuid = _uuidGenerator.GenerateUuid();
                var createdAt = DateTime.UtcNow;
                var lastUpdateAt = DateTime.UtcNow;

                var user = new UserEntity
                {
                    LoginId = request.LoginId,
                    PasswordHash = hashedPassword,
                    Salt = salt,
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    PrivilegeId = request.PrivilegeId,
                    ColorId = request.ColorId,
                    CreatedAt = createdAt,
                    LastUpdatedAt = lastUpdateAt
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new UserDto
                {
                    Id = user.Id,
                    LoginId = user.LoginId,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    PrivilegeId = user.PrivilegeId,
                    ColorId = user.ColorId,
                };
            }
            catch(ArgumentException ex)
            {
                throw;
            }
            catch(ApplicationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("システムエラー", ex);
            }

        }
    }
}
