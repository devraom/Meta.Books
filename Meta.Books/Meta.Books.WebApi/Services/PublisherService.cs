using Meta.Books.Core.Entities;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Repositories.Interfaces;
using Meta.Books.WebApi.Services.Interfaces;

namespace Meta.Books.WebApi.Services;

public class PublisherService : IBaseService<PublisherDto>
{
    private readonly IBaseRepository<Publisher> _publisherRepository;
    
    public PublisherService(IBaseRepository<Publisher> publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<PublisherDto> SaveAsync(PublisherDto publisherDto)
    {
        var publisher = new Publisher
        {
            name = publisherDto.name,
            created_by = 0,
            created_date = DateTime.Now,
            updated_by = 0,
            updated_date = DateTime.Now
        };
        
        publisher = await _publisherRepository.SaveAsync(publisher);
        publisherDto.id = publisher.id;

        return publisherDto;
    }

    public async Task<PublisherDto> UpdateAsync(PublisherDto publisherDto)
    {
        var publisher = await _publisherRepository.GetById(publisherDto.id);

        if (publisher == null)
            throw new Exception("Publisher not found");
            
        publisher.name = publisherDto.name;
        publisher.updated_by = 0;
        publisher.updated_date = DateTime.Now;
        
        await _publisherRepository.UpdateAsync(publisher);

        return publisherDto;
    }

    public async Task<List<PublisherDto>> GetAllAsync()
    {
        var publishers = await _publisherRepository.GetAllAsync();
        var publishersDto = publishers.Select(c => new PublisherDto(c)).ToList();
        return publishersDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var publisher = await _publisherRepository.GetById(id);
        if (publisher == null)
        {
            throw new Exception("Publisher not found");
        }
        
        return await _publisherRepository.DeleteAsync(id);
    }

    public async Task<PublisherDto> GetById(int id)
    {
        var publisher = await _publisherRepository.GetById(id);
        if (publisher == null)
        {
            throw new Exception("Publisher not found");
        }

        var loanDto = new PublisherDto(publisher);
        return loanDto;
    }
}