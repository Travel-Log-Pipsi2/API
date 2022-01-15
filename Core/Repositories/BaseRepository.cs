using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IDbModel
    {
        protected readonly ApiDbContext _context;

        public BaseRepository(ApiDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Create(T model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }
        public virtual async Task Delete(T model)
        {
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Edit(T model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindByConditions(Expression<Func<T, bool>> expresion)
        {
            return await _context.Set<T>().Where(expresion).ToListAsync();
        }
    }
}
