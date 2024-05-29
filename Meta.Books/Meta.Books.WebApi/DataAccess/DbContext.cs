using System.Data.Common;
using Meta.Books.WebApi.DataAccess.Interfaces;
using MySqlConnector;

namespace Meta.Books.WebApi.DataAccess;

public class DbContext : IDbContext
{
    private readonly string _connectionString = "server=localhost;user=root;pwd=tai310820tai*;database=MetaBooks;port=3306";

    private MySqlConnection _connection;

    public DbConnection Connection
    {
        get
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(_connectionString);
            }

            return _connection;
        }
    }
}