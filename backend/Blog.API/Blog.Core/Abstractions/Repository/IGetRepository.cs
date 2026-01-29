namespace Blog.Core.Abstractions.Repository {
    public interface IGetRepository<T> where T :class {
        Task<List<T?>> Get ( );
    }
}
