using System.ComponentModel.DataAnnotations.Schema;

namespace Meta.Books.Core.Entities;

[Table("Categories")]
public class Category : BaseEntity
{
    public string name { get; set; }
}