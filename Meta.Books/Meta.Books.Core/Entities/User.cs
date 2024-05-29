using System.ComponentModel.DataAnnotations.Schema;

namespace Meta.Books.Core.Entities;

[Table("Users")]
public class User : BaseEntity
{
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}