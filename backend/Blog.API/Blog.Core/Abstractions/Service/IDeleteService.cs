namespace Blog.Core.Abstractions.Service {
    public interface IDeleteService<T> where T : class {
        Task<bool> Delete(T entity);
    }
}
