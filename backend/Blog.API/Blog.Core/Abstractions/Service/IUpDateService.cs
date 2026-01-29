namespace Blog.Core.Abstractions.Service {
    public interface IUpDateService<T> where T : class {
        Task<bool> UpData(T data);
    }
}
