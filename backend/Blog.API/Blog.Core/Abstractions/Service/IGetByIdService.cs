namespace Blog.Core.Abstractions.Service {
    public interface IGetByIdService<T> where T : class {
        Task<T?> GetById ( int id );
    }
}
