using Blog.Contracts.DTO.User;

namespace Blog.Core.Abstractions.Service.User {
    public interface IIdentityService {
        Task<bool> RegisterAsync ( RegisterUserRequest request );

        // Возвращает JWT токен
        Task<string> LoginAsync( LoginUserRequest request ); 
    }
}
