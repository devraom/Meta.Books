using System.ComponentModel.DataAnnotations.Schema;

namespace Meta.Books.Core.Entities;

[Table("Reservations")]
public class Reservation : BaseEntity
{
    public int user_id { get; set; }
    public int book_id { get; set; }
    public DateTime reservation_date { get; set; }
}