using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meta.Books.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublisherController : BaseController<Publisher, PublisherDto>
{
    public PublisherController(IBaseService<PublisherDto> baseService) : base(baseService)
    {
    }
}