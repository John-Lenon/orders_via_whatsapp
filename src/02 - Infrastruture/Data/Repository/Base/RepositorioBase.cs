﻿using System.Linq.Expressions;
using Domain.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repository.Base
{
    public abstract class RepositorioBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class, new()
        where TContext : DbContext
    {
        private readonly TContext _context;
        private DbSet<TEntity> DbSet { get; }

        protected RepositorioBase(IServiceProvider service)
        {
            _context = service.GetRequiredService<TContext>();
            DbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression != null)
                return DbSet.Where(expression);

            return DbSet.AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(int id) => await DbSet.FindAsync(id);

        public virtual async Task InsertAsync(TEntity entity) => await DbSet.AddAsync(entity);

        public void Update(TEntity entity) => DbSet.Update(entity);

        public void Delete(TEntity entity) => DbSet.Remove(entity);

        public void DeleteRange(TEntity[] entityArray) => DbSet.RemoveRange(entityArray);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
