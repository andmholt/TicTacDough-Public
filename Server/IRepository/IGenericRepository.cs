using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace TicTacDough.Server.IRepository {
    public interface IGenericRepository<T> where T : class {
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null
         );
        Task Insert(T entity);
    }
}