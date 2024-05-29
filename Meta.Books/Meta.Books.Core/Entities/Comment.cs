using System.ComponentModel.DataAnnotations.Schema;

namespace Meta.Books.Core.Entities;

[Table("Comments")]
public class Comment : BaseEntity
{
    public int book_id { get; set; }
    public int user_id { get; set; }
    public string comment { get; set; }
    public DateTime date { get; set; }
}