namespace Blog.Core.Abstractions.Service {
    public interface ICreateService<T> where T :class {
        Task<bool> Create(T entity);
    }
}
