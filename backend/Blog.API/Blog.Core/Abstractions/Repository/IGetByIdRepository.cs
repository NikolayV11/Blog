namespace Blog.Core.Abstractions.Repository {
    public interface IGetByIdRepository<T> where T : class {
        Task<T?> GetById ( int id );
    }
}
