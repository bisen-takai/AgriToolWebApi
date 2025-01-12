using AgriToolWebApi.Application.DTOs;
using AgriToolWebApi.Application.Exceptions;
using AgriToolWebApi.Application.Interfaces;
using AgriToolWebApi.Application.Requests.Users;
using AgriToolWebApi.Common.Interfaces.Security;
using AgriToolWebApi.Common.Interfaces.Uuid;
using AgriToolWebApi.Common.Utilities.Security;
using AgriToolWebApi.Infrastructure.Persistence.Contexts;
using AgriToolWebApi.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;

namespace AgriToolWebApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IUuidGenerator _uuidGenerator;
        private readonly ISaltGenerator _saltGenerator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext context, IUuidGenerator uuidGenerator, ISaltGenerator saltGenerator, IPasswordHasher passwordHasher, ILogger<UserService> logger)
        {
            _context = context;
            _uuidGenerator = uuidGenerator;
            _saltGenerator = saltGenerator;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        /// <summary>
        /// ユーザ情報を登録する
        /// </summary>
        /// <param name="request">ユーザ情報のリクエストデータ</param>
        /// <returns>ユーザ情報のレスポンスデータ</returns>
        public async Task<UserDto> CreateUserAsync(UserCreateRequest request)
        {
            _logger.LogInformation("ユーザ作成処理を開始します。");

            var exists = await _context.Users.AnyAsync(u => u.LoginId != null && u.LoginId == request.LoginId);

            if (exists)
            {
                _logger.LogError($"{request.LoginId} は既に利用されています。");
                throw new UserCreationException($"{request.LoginId} は既に利用されています。");
            }

            try
            {
                var salt = _saltGenerator.GenerateSalt();
                var hashedPassword = _passwordHasher.HashPassword(request.Password, salt);
                var uuid = _uuidGenerator.GenerateUuid();
                var createdAt = DateTime.UtcNow;
                var lastUpdateAt = DateTime.UtcNow;

                var user = new UserPersistenceEntity
                {
                    LoginId = request.LoginId,
                    Uuid = uuid,
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

                _logger.LogInformation("ユーザ作成処理を完了しました。");

                return MapToDto(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ユーザ登録中にシステムエラーが発生しました。");
                throw new UserCreationException("システムエラーが発生しました。後ほど再試行してください。", ex);
            }
        }

        public async Task<UserDto> UpdateUserAsync(UserUpdateRequest request)
        {
            _logger.LogInformation("ユーザ更新処理を開始します。");

            var user = await _context.Users.FindAsync(request.Id);

            if (user == null)
            {
                _logger.LogError($"{request.Id} は存在しません。");
                throw new InvalidOperationException("ユーザが見つかりません。");
            }

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                var salt = _saltGenerator.GenerateSalt();
                var hashedPassword = _passwordHasher.HashPassword(request.Password, salt);

                user.PasswordHash = hashedPassword;
                user.Salt = salt;
            }

            if (!string.IsNullOrEmpty(request.LoginId))
            {
                user.LoginId = request.LoginId;
            }

            if (!string.IsNullOrEmpty(request.FullName))
            {
                user.FullName = request.FullName;
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                user.PhoneNumber = request.PhoneNumber;
            }

            if (request.PrivilegeId != 0)
            {
                user.PrivilegeId = request.PrivilegeId;
            }

            if (request.ColorId != 0)
            {
                user.ColorId = request.ColorId;
            }

            user.LastUpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("ユーザ更新処理を完了しました。");

            return MapToDto(user);
        }

        public async Task<bool> DeleteUserAsync(UserDeleteRequest request)
        {
            _logger.LogInformation("ユーザ削除処理を開始します。");

            var user = await _context.Users.FindAsync(request.Id);

            if (user == null)
            {
                _logger.LogError("ユーザは存在しません。");
                throw new InvalidOperationException("ユーザが見つかりません。");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("ユーザ削除処理を完了しました。");

            return true;
        }

        public async Task<UserDto> GetUserByIdAsync(UserDetailRequest request)
        {
            _logger.LogInformation($"ユーザ詳細取得処理を開始します。Id:{request.Id}");

            var user = await _context.Users.FindAsync(request.Id);

            if (user == null)
            {
                _logger.LogError($"Id: {request.Id} は存在しません。");
                throw new InvalidOperationException("ユーザが見つかりません。");
            }

            _logger.LogInformation("ユーザ詳細取得処理を完了しました。");

            return MapToDto(user);
        }

        public async Task<IEnumerable<UserDto>> GetUserListAsync(UserSearchRequest request)
        {
            _logger.LogInformation($"ユーザ一覧取得処理を開始します。Page:{request.PageNumber}、PageSize:{request.PageSize}");

            // クエリの初期化
            var query = _context.Users.AsQueryable();

            var users = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            _logger.LogInformation("ユーザ一覧取得処理が完了しました。");

            return users.Select(MapToDto);
        }

        public async Task<UserDto> AuthenticateAsync(UserLoginRequest request)
        {
            _logger.LogInformation("ログイン認証を開始します。");

            // 1. ログインIDでユーザーを検索
            var user = await _context.Users
                .Where(u => u.LoginId == request.LoginId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                _logger.LogError("ログインIDが存在しません。");
                throw new InvalidOperationException("無効なログインIDです。");
            }

            // 2. パスワードのハッシュ化と照合
            var hashedPassword = _passwordHasher.HashPassword(request.Password, user.Salt);
            if (user.PasswordHash != hashedPassword)
            {
                _logger.LogError("パスワードが一致しません。");
                throw new InvalidOperationException("無効なパスワードです。");
            }

            _logger.LogInformation("ログイン認証に成功しました。");

            // 3. 認証成功時に DTO を返す
            return MapToDto(user);
        }

        private UserDto MapToDto(UserPersistenceEntity user)
        {
            return new UserDto
            {
                Id = user.Id,
                LoginId = user.LoginId,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                PrivilegeId = user.PrivilegeId,
                ColorId = user.ColorId
            };
        }
    }
}
