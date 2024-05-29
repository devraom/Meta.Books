using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meta.Books.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : BaseController<Reservation, ReservationDto>
{
    public ReservationController(IBaseService<ReservationDto> baseService) : base(baseService)
    {
    }
}