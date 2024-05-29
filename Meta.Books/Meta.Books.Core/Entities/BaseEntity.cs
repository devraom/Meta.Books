using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Meta.Books.Core.Entities;

public class BaseEntity
{
    public int id { get; set; }
    public bool is_deleted { get; set; }
    public int created_by { get; set; }
    public DateTime created_date { get; set; }
    public int updated_by { get; set; }
    public DateTime updated_date { get; set; }
}

public static class TableHelper
{
    public static string GetTableName<T>() where T : BaseEntity
    {
        var tableAttribute = typeof(T).GetCustomAttribute<TableAttribute>();
        if (tableAttribute == null || string.IsNullOrEmpty(tableAttribute.Name))
        {
            throw new InvalidOperationException($"El tipo {typeof(T).Name} no tiene el atributo TableAttribute o el nombre de la tabla es nulo o vacío.");
        }
        return tableAttribute.Name;
    }
}