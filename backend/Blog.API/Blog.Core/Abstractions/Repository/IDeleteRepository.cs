namespace Blog.Core.Abstractions.Repository {
    public interface IDeleteRepository<T> where T : class {
        Task<bool> Delete(T entity);
    }
}
