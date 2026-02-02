using Blog.Core.Models;

namespace Blog.Core.Abstractions.Service.User {
    // Запрос для поиска профила
    public interface IUserQuerySwrvice {
        Task<Models.User?> GetByIdAsync ( int id );
        Task<Models.User?> GetByEmailAsync( string email );
        Task<bool> IsEmailUniqueAsync ( string email );
    }
}
