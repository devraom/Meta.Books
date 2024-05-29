using System.ComponentModel.DataAnnotations.Schema;

namespace Meta.Books.Core.Entities;

[Table("Books")]
public class Book : BaseEntity
{
    public string title { get; set; }
    public int author_id { get; set; }
    public int category_id { get; set; }
    public int publisher_id { get; set; }
}