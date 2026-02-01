namespace Blog.Core.Abstractions.Service {
    public interface IGetService<T> where T :class {
        Task<List<T>> Get ( );
    }
}
