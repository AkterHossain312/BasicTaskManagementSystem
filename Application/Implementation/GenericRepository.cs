using Application.Interface;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementation
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly TaskManagementDbContext _context;
        private readonly DbSet<TEntity> _table = null;

        public GenericRepository(TaskManagementDbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _table.FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _table.AddAsync(entity);

        }

        public async Task UpdateAsync(TEntity entityToUpdate)
        {
            _table.Update(entityToUpdate);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(object id)
        {
            TEntity entityToDelete = _table.Find(id);
            await DeleteAsync(entityToDelete);

        }

        public async Task DeleteAsync(TEntity entityToDelete)
        {
            _table.Remove(entityToDelete);
            await Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}