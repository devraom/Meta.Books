using Meta.Books.Core.Entities;
using Meta.Books.WebApi.DataAccess;
using Meta.Books.WebApi.DataAccess.Interfaces;
using Meta.Books.WebApi.Dto;
using Meta.Books.WebApi.Repositories;
using Meta.Books.WebApi.Repositories.Interfaces;
using Meta.Books.WebApi.Services;
using Meta.Books.WebApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDbContext, DbContext>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IBaseService<AuthorDto>, AuthorService>();
builder.Services.AddScoped<IBaseService<BookDto>, BookService>();
builder.Services.AddScoped<IBaseService<CategoryDto>, CategoryService>();
builder.Services.AddScoped<IBaseService<CommentDto>, CommentService>();
builder.Services.AddScoped<IBaseService<LoanDto>, LoanService>();
builder.Services.AddScoped<IBaseService<PublisherDto>, PublisherService>();
builder.Services.AddScoped<IBaseService<ReservationDto>, ReservationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();