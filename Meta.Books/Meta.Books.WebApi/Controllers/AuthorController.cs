using Meta.Books.Core.Entities;
using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meta.Books.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : BaseController<Author, AuthorDto>
{
    public AuthorController(IBaseService<AuthorDto> baseService) : base(baseService)
    {
    }
}