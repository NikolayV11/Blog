namespace Blog.Core.Abstractions.Repository {
    public interface ICreateRepository<T> where T :class {
        Task<bool> Create(T entity);
    }
}
