using Domain.Entities.Base;
using Domain.Interfaces.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Data.Repository.Base
{
    public abstract class RepositorioBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : EntityBase
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> DbSet;

        protected RepositorioBase(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<TContext>();
            DbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression != null)
                return DbSet.Where(expression);

            return DbSet.AsQueryable();
        }

        public async Task<TEntity> GetByIdAsync(int id) => await DbSet.FindAsync(id);

        public async Task<TEntity> GetByCodigoAsync(Guid? codigo) =>
            await Get(x => x.Codigo == codigo.GetValueOrDefault()).FirstOrDefaultAsync();

        public virtual async Task InsertAsync(TEntity entity) => await DbSet.AddAsync(entity);

        public void Update(TEntity entity) => DbSet.Update(entity);

        public void Delete(TEntity entity) => DbSet.Remove(entity);

        public void DeleteRange(TEntity[] entityArray) => DbSet.RemoveRange(entityArray);

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

        protected IQueryable<TResult> ExecuteSqlQuery<TResult>(string tableName, List<SqlParameter> listParameters)
        {
            var sqlString = BuildSqlString(tableName, listParameters);
            var listResults = _context.Database.SqlQueryRaw<TResult>(sqlString.ToString(), listParameters);
            return listResults;
        }

        protected void ExecuteSql(string tableName, List<SqlParameter> listParameters)
        {
            var sqlString = BuildSqlString(tableName, listParameters);
            _context.Database.ExecuteSqlRaw(sqlString, listParameters);
        }

        #region Private Methods
        private string BuildSqlString(string tableName, List<SqlParameter> listParameters)
        {
            var sqlString = new StringBuilder($"SELECT * FROM {tableName} PRODUTO");

            if (listParameters.Count > 0)
            {
                var parameter = listParameters.First();
                sqlString.Append($" WHERE {parameter.ParameterName.Substring(1)} = {parameter.ParameterName}");
            }

            foreach (var parameter in listParameters)
                sqlString.Append($" AND {parameter.ParameterName[1..]} = {parameter.ParameterName}");

            return sqlString.ToString();
        }
        #endregion
    }
}
