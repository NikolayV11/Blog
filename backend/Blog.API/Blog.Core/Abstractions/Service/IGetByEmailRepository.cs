namespace Blog.Core.Abstractions.Service {
    public interface IGetByEmailRepository<T> where T : class {
        Task<T> GetByEmailAsync(string email);
    }
}
