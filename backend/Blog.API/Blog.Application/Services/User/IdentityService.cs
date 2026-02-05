using Blog.Contracts.DTO.User;
using Blog.Core.Abstractions.Auth;
using Blog.Core.Abstractions.Repository;
using Blog.Core.Abstractions.Service;
using Blog.Core.Abstractions.Service.User;
using UserEntity = Blog.DataAccess.Models.User.Entity.User;

namespace Blog.Application.Services.User {
    public class IdentityService : IIdentityService {
        private readonly IUserQueryService _userQuery;
        private readonly IPasswordHasher _hasher;
        private readonly ICreateRepository<UserEntity> _createRepo;
        private readonly IGetByEmailRepository<UserEntity> _emailRepo;

        public IdentityService (
            IUserQueryService userQuery,
            IPasswordHasher hasher,
            ICreateRepository<UserEntity> createRepo,
            IGetByEmailRepository<UserEntity> emailRepo ) {
            _userQuery = userQuery;
            _hasher = hasher;
            _createRepo = createRepo;
            _emailRepo = emailRepo;
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

        // Сделаем, когда подключим JWT
        public async Task<string> LoginAsync ( LoginUserRequest request ) {
            // 1. Ищем пользователя в базе через QueryService
            // Нам нужен доменный пользователь, но чтобы проверить пароль,
            // нам нужен доступ к Entity (где лежит хеш)
            // Поэтому воспользуемся репозиторием напрямую или расширим QueryService
            var userEntity = await _emailRepo.GetByEmailAsync(request.Email); // убедимся что он возвращает сущность с хешом
            if (userEntity == null) {
                throw new Exception("Пользователь с таким Email не найден.");
            }

            // 2. Проверяем пароль
            // requst.Password - "чистый" пароль от пользователя
            // userEntity.PasswordHash из бд
            var isPasswordValid = _hasher.VerifyPassword(request.Password, userEntity.PasswordHash);

            if (( !isPasswordValid ) {
                throw new Exception("Неверный пароль");
            }

            return "Успешный вход";

        }
    }

}
}
