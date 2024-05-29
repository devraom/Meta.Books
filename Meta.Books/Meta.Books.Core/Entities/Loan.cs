using System.ComponentModel.DataAnnotations.Schema;

namespace Meta.Books.Core.Entities;

[Table("Loans")]
public class Loan : BaseEntity
{
    public int user_id { get; set; }
    public int book_id { get; set; }
    public DateTime loan_date { get; set; }
    public DateTime return_date { get; set; }
}