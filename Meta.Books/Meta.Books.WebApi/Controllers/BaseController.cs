using Meta.Books.Core.Entities;
using Meta.Books.Core.Http;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Meta.Books.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController<T, TDto> : ControllerBase
    where T: BaseEntity
    where TDto : BaseDto
{
    private readonly IBaseService<TDto> _baseService;

    public BaseController(IBaseService<TDto> baseService)
    {
        _baseService = baseService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<T>>>> GetAll()
    {
        var response = new Response<List<TDto>>
        {
            data = await _baseService.GetAllAsync()
        };

        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<Response<TDto>>> Post([FromBody] TDto entityDto)
    {

        var response = new Response<TDto>()
        {
            data = await _baseService.SaveAsync(entityDto)
        };

        return Created($"/api/[controller]/{response.data.id}", response); 
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<TDto>>> GetByID(int id)
    {
        var response = new Response<TDto>();
        
        try
        {
            response.data = await _baseService.GetById(id);
        }
        catch (Exception ex)
        {
            response.errors.Add(ex.Message);
            return NotFound(response);
        }

        return Ok(response);
    }
    
    [HttpPut]
    public async Task<ActionResult<Response<TDto>>> Update([FromBody] TDto entityDto)
    {
        var response = new Response<TDto>();
        
        try
        {
            response.data = await _baseService.UpdateAsync(entityDto);
        }
        catch (Exception ex)
        {
            response.errors.Add(ex.Message);
            return NotFound(response);
        }
        
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
       
        try
        {
            response.data = await _baseService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            response.errors.Add(ex.Message);
            return NotFound(response);
        }
        
        return Ok(response);
    }
}