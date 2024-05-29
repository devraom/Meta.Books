using System.ComponentModel.DataAnnotations.Schema;

namespace Meta.Books.Core.Entities;

[Table("Authors")]
public class Author : BaseEntity
{
    public string name { get; set; }
}