using Blog.Application.Mapping;
using Blog.Contracts.DTO.User;
using Blog.Core.Abstractions.Auth;
using Blog.Core.Abstractions.Repository;
using Blog.Core.Abstractions.Service;
using Blog.Core.Abstractions.Service.User;
using Microsoft.AspNetCore.Http;
using UserEntity = Blog.DataAccess.Models.User.Entity.User;

namespace Blog.Application.Services.User {
    public class IdentityService : IIdentityService {
        private readonly IUserQueryService _userQuery;
        private readonly IPasswordHasher _hasher;
        private readonly ICreateRepository<UserEntity> _createRepo;
        private readonly IGetByEmailRepository<UserEntity> _emailRepo;
        private readonly IGetByIdRepository<UserEntity> _idRepo;
        private readonly IUpDateService<UserEntity> _updateRepo;
        private readonly IJwtProvaider _jwtProvaider;

        public IdentityService (
            IUserQueryService userQuery,
            IPasswordHasher hasher,
            ICreateRepository<UserEntity> createRepo,
            IGetByEmailRepository<UserEntity> emailRepo,
            IGetByIdRepository<UserEntity> getByIdRepo,
            IUpDateService<UserEntity> updateRepo,
            IJwtProvaider jwtProvaider ) {
            _userQuery = userQuery;
            _hasher = hasher;
            _createRepo = createRepo;
            _emailRepo = emailRepo;
            _idRepo = getByIdRepo;
            _updateRepo = updateRepo;
            _jwtProvaider = jwtProvaider;
        }
        public async Task<bool> RegisterAsync ( RegisterUserRequest request ) {
            // 1. проверяем уникальность
            if (!await _userQuery.IsEmailUniqueAsync(request.Email)) {
                throw new Exception("Email уже занят.");
            }

            // 2. Хешируем пароль
            var hash = _hasher.Generate(request.Password);

            // 3. Создаём Entity для бд
            UserEntity newUser = new UserEntity {
                Email = request.Email,
                PasswordHash = hash,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Surname = request.Surname,
                Phone = request.Phone,
                Birthday = request.Birthday,
            };

            // 4. Сохраняем в бд
            return await _createRepo.Create(newUser);
        }


        public async Task<string> LoginAsync ( LoginUserRequest request ) {
            var user = await _emailRepo.GetByEmailAsync(request.Email);

            if (user == null || !_hasher.VerifyPassword(request.Password, user.PasswordHash)) {
                throw new Exception("Неверный логин или пароль.");
            }
            var userDomain = user.ToDomain();

            return _jwtProvaider.GenerateToken(userDomain);
        }

        public async Task<string> SaveAvatarAsync ( int userId, IFormFile file ) {
            // 1. Получаем пользователя из базы
            var user = await _idRepo.GetById(userId);
            if (user == null)
                throw new Exception("Пользователь не найден");

            // 2. сохраняем файл на диск
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = $"avatar_{userId}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create)) {
                await file.CopyToAsync(stream);
            }

            // 3. Создаем объект UserImages
            user.Avatar = new DataAccess.Models.User.Entity.UserImages { StoredName = fileName };

            // 4. Сохраняем
            await _updateRepo.UpData(user);
            return $"/uploads/{fileName}";
        }

        public async Task<bool> UpdateProfileAsync ( int userId, UpdateUserRequest request ) {
            // 1. получаем пользователя
            var user = await _idRepo.GetById (userId);
            if (user == null)
                return false;

            // 2. Если прилетел новый файл - сохраняем его
            if (request.AvatarFile != null && request.AvatarFile.Length > 0) {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var fileName = $"avatar_{userId}_{Guid.NewGuid()}{Path.GetExtension(request.AvatarFile.FileName)}";
                var fullPath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create)) {
                    await request.AvatarFile.CopyToAsync(stream);
                }

                // Привязываем новую аватарку
                user.Avatar = new DataAccess.Models.User.Entity.UserImages { StoredName = fileName };
            }

            // 3. Обновляем текстовые поля
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Surname = request.Surname;
            user.Phone = request.Phone;
            user.Birthday = request.Birthday;
            user.UpdatedAt = DateTime.UtcNow;

            // 4. Сохраняем всё одним вызовом UpData
            return await _updateRepo.UpData(user);
        }
    }

}

