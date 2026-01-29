namespace Blog.Core.Abstractions.Repository {
    public interface IUpDateRepository<T> where T : class {
        Task<bool> UpData(T data);
    }
}
