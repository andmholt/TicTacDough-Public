using Microsoft.EntityFrameworkCore;
using TicTacDough.Shared.Domain;

namespace TicTacDough.Server.Repository {
    public class UnitOfWork : IRepository.IUnitOfWork {

        // members
        private readonly Data.ApplicationDbContext context;
        private IRepository.IGenericRepository<TicTacDough.Shared.Domain.Score> scores;

        // constructor
        public UnitOfWork(Data.ApplicationDbContext context) {
            this.context = context;
        }

        public IRepository.IGenericRepository<TicTacDough.Shared.Domain.Score> Scores
            => this.scores ??= new Repository.GenericRepository<TicTacDough.Shared.Domain.Score>(this.context);

        // dispose of this object
        public void Dispose() {
            this.context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext) {
            var entries = this.context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);
            
            foreach (var entry in entries) {
                if (entry.State == EntityState.Added) {
                    ((Shared.Domain.BaseDomainModel)entry.Entity).date_added = DateTime.Now;
                }
            }

            await this.context.SaveChangesAsync();
        }
    }
}