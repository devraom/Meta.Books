using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meta.Books.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : BaseController<Book, BookDto>
{
    public BookController(IBaseService<BookDto> baseService) : base(baseService)
    {
    }
}