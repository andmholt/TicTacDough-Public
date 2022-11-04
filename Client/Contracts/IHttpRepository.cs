

namespace TicTacDough.Client.Contracts {
    public interface IHttpRepository<T> where T : class {
        Task<List<T>> GetAll(String url);
    }
}