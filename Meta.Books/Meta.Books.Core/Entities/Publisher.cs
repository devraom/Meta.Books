using System.ComponentModel.DataAnnotations.Schema;

namespace Meta.Books.Core.Entities;

[Table("Publishers")]
public class Publisher : BaseEntity
{
    public string name { get; set; }
}