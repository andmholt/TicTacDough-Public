using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace TicTacDough.Server.Repository {
    public class GenericRepository<T> : IRepository.IGenericRepository<T> where T : class {
        
        // members
        private readonly Data.ApplicationDbContext context;
        private readonly DbSet<T> db;

        // constructor
        public GenericRepository(Data.ApplicationDbContext context) {
            this.context = context;
            this.db = context.Set<T>();
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null) {

            IQueryable<T> query = this.db;

            if (expression != null) {
                query = query.Where(expression);
            }

            if (includes != null) {
                query = includes(query);
            }

            if (orderBy != null) {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Insert(T entity) {
            await this.db.AddAsync(entity);
        }
    }
}