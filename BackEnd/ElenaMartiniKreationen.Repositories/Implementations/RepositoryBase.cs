using ElenaMartiniKreationen.DataAccess.Data;
using ElenaMartiniKreationen.Entities;
using ElenaMartiniKreationen.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.Repositories.Implementations
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        private readonly ElenaMartiniKreationenDbContext _context;

        public RepositoryBase(ElenaMartiniKreationenDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var field = await FindAsync(id);
            if (field is not null)
            {
                field.State = false;
                await UpdateAsync();
            }
        }

        public async Task<TEntity?> FindAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<ICollection<TEntity>> ListAsync()
        {
            return await _context.Set<TEntity>()
              .Where(p => p.State)
              .AsNoTracking()
              .ToListAsync();
        }

        public async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>()
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<ICollection<TInfo>> ListAsync<TInfo>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TInfo>> selector, string relations)
        {
            var collection = _context.Set<TEntity>()
             .Where(predicate)
             .AsQueryable();


            if (!string.IsNullOrEmpty(relations))
            {
                foreach (var tabla in relations.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    collection = collection.Include(tabla);
                }
            }
            return await collection
                .AsNoTracking()
                .Select(selector)
                .ToListAsync();
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
