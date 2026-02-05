using Blog.Core.Abstractions.Service.User;
using Blog.Core.Abstractions.Repository;
// чтобы заработал .ToDomain()
using Blog.Application.Mapping;

using UserEntity = Blog.DataAccess.Models.User.Entity.User;
using UserDomain = Blog.Core.Models.User;
using Blog.Core.Abstractions.Service;
using Blog.Contracts.DTO.User;

namespace Blog.Application.Services.User {
    public class UserQueryService : IUserQueryService {
        private readonly IGetByIdRepository<UserEntity> _idRepo;
        private readonly IGetByEmailRepository<UserEntity> _emailRepo;

        public UserQueryService (
            IGetByIdRepository<UserEntity> idRepo,
            IGetByEmailRepository<UserEntity> emailRepo ) {
            _idRepo = idRepo;
            _emailRepo = emailRepo;
        }

        // Проверка уникальности для IdentityService
        public async Task<bool> IsEmailUniqueAsync ( string email ) {
            var user = await _emailRepo.GetByEmailAsync(email);
            return user == null;
        }

        // Получение по ID (с маппингом в доменную модель)
        public async Task<UserDomain?> GetByIdAsync ( int id ) {
            var entity = await _idRepo.GetById(id);
            return entity?.ToDomain();
        }

        // Получение по Email
        public async Task<UserDomain?> GetByEmailAsync ( string email ) {
            var entity = await _emailRepo.GetByEmailAsync(email);
            return entity?.ToDomain();
        }

        // получаем пользователя
        public async Task<UserResponse?> GetUserResponseByIdAsync ( int userId ) {
            var entity = await _idRepo.GetById(userId);

            if (entity == null) { return null; }

            return new UserResponse(
                entity.Id, 
                entity.FirstName, 
                entity.LastName, 
                entity.Surname, 
                entity.Email, 
                entity.Phone,
                entity.Avatar != null ? $"/uploads/{entity.Avatar.StoredName}" : null
                );
        }
    }
}
