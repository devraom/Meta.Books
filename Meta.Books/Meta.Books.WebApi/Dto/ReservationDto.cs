using Meta.Books.Core.Entities;

namespace Meta.Books.WebApi.Dto;

public class ReservationDto : BaseDto
{
    public int user_id { get; set; }
    public int book_id { get; set; }
    public DateTime reservation_date { get; set; }
    
    public ReservationDto(){}

    public ReservationDto(Reservation reservation)
    {
        id = reservation.id;
        user_id = reservation.user_id;
        book_id = reservation.book_id;
        reservation_date = reservation.reservation_date;
    }
}