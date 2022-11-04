using TicTacDough.Shared.Domain;

namespace TicTacDough.Server.IRepository {
    public interface IUnitOfWork : IDisposable {
        Task Save(HttpContext httpContext);
        IGenericRepository<TicTacDough.Shared.Domain.Score> Scores { get; }
    }
}