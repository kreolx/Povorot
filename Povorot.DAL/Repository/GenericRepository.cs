using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Povorot.DAL.Contexts;

namespace Povorot.DAL.Repository
{
    public class GenericRepository<T>: IGenericRepository<T> where T: class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<ICollection<T>> List(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Create(T model)
        {
            await _db.AddAsync(model);
        }

        public async Task CreateRange(ICollection<T> models)
        {
            await _db.AddRangeAsync(models);
        }

        public void Update(T model)
        {
            _db.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
        }

        public void UpdateRange(ICollection<T> models)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long id)
        {
            var entity = await _db.FindAsync(id);
            Delete(entity);
        }

        public void  Delete(T model)
        {
            _db.Remove(model);
        }
    }
}