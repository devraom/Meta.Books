using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Repositories.Interfaces;
using Meta.Books.WebApi.Services.Interfaces;

namespace Meta.Books.WebApi.Services;

public class ReservationService : IBaseService<ReservationDto>
{
    private readonly IBaseRepository<Reservation> _reservationRepository;
    
    public ReservationService(IBaseRepository<Reservation> reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<ReservationDto> SaveAsync(ReservationDto reservationDto)
    {
        var reservation = new Reservation
        {
            user_id = reservationDto.user_id,
            book_id = reservationDto.book_id,
            reservation_date = DateTime.Now,
            created_by = 0,
            created_date = DateTime.Now,
            updated_by = 0,
            updated_date = DateTime.Now
        };
        
        reservation = await _reservationRepository.SaveAsync(reservation);
        reservationDto.id = reservation.id;

        return reservationDto;
    }

    public async Task<ReservationDto> UpdateAsync(ReservationDto reservationDto)
    {
        var reservation = await _reservationRepository.GetById(reservationDto.id);

        if (reservation == null)
            throw new Exception("Reservation not found");
            
        reservation.user_id = reservationDto.user_id;
        reservation.book_id = reservationDto.book_id;
        reservation.reservation_date = reservationDto.reservation_date;
        reservation.updated_by = 0;
        reservation.updated_date = DateTime.Now;
        
        await _reservationRepository.UpdateAsync(reservation);

        return reservationDto;
    }

    public async Task<List<ReservationDto>> GetAllAsync()
    {
        var reservations = await _reservationRepository.GetAllAsync();
        var reservationsDto = reservations.Select(c => new ReservationDto(c)).ToList();
        return reservationsDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var reservation = await _reservationRepository.GetById(id);
        if (reservation == null)
        {
            throw new Exception("Reservation not found");
        }
        
        return await _reservationRepository.DeleteAsync(id);
    }

    public async Task<ReservationDto> GetById(int id)
    {
        var reservation = await _reservationRepository.GetById(id);
        if (reservation == null)
        {
            throw new Exception("Reservation not found");
        }

        var reservationDto = new ReservationDto(reservation);
        return reservationDto;
    }
}