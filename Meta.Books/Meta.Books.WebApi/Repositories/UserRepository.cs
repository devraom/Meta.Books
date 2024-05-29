using Dapper;
using Meta.Books.Core.Entities;
using Meta.Books.WebApi.DataAccess.Interfaces;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Repositories.Interfaces;

namespace Meta.Books.WebApi.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IDbContext dbContext) : base(dbContext) { }

    public async Task<User> Login(LoginDto loginDto)
    {
        string tableName = TableHelper.GetTableName<User>();
        string sql = $"SELECT * FROM {tableName} WHERE email = '{loginDto.email}' AND password = '{loginDto.password}' AND is_deleted = 0";
        return (await _dbContext.Connection.QueryAsync<User>(sql)).FirstOrDefault();
    }
}