using Blog.Contracts.DTO.User;
using Blog.Core.Models;

namespace Blog.Core.Abstractions.Service.User {
    // Запрос для поиска профила
    public interface IUserQueryService {
        Task<Models.User?> GetByIdAsync ( int id );
        Task<Models.User?> GetByEmailAsync( string email );
        Task<bool> IsEmailUniqueAsync ( string email );
        Task<UserResponse?> GetUserResponseByIdAsync ( int userId );

    }
}
