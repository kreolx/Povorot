using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Povorot.DAL.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);

        Task<ICollection<T>> List(Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null);

        Task Create(T model);
        Task CreateRange(ICollection<T> models);
        Task Update(T model);
        void UpdateRange(ICollection<T> models);
        Task Delete(long id);
        void Delete(T model);
    }
}