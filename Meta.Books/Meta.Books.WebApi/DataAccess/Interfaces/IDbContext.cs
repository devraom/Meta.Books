using System.Data.Common;

namespace Meta.Books.WebApi.DataAccess.Interfaces;

public interface IDbContext
{
    DbConnection Connection { get; }
}