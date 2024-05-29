using System.Reflection;
using Dapper;
using Dapper.Contrib.Extensions;
using Meta.Books.Core.Entities;
using Meta.Books.WebApi.DataAccess.Interfaces;
using Meta.Books.WebApi.Repositories.Interfaces;

namespace Meta.Books.WebApi.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly IDbContext _dbContext;

    public BaseRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> SaveAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        
        entity.id = await _dbContext.Connection.InsertAsync(entity);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await _dbContext.Connection.UpdateAsync(entity);
        return entity;
    }

    public async Task<List<T>> GetAllAsync()
    {
        string tableName = TableHelper.GetTableName<T>();
        string sql = $"SELECT * FROM {tableName} WHERE is_deleted = 0";
        return (await _dbContext.Connection.QueryAsync<T>(sql)).ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetById(id);
        if (entity == null)
            return false;

        entity.is_deleted = true;
        await _dbContext.Connection.UpdateAsync(entity);
        return true;
    }

    public async Task<T> GetById(int id)
    {
        return await _dbContext.Connection.GetAsync<T>(id);
    }
}
