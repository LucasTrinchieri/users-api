using Microsoft.EntityFrameworkCore;
using Users_api.Dto;
using Users_api.Models;
using Users_api.Repository;
using Users_api.Service;

var builder = WebApplication.CreateBuilder(args);

// Entity Framework
builder.Services.AddDbContext<UsersContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("UserApiConnection"));
});

// Repository
builder.Services.AddKeyedScoped<IRepository<User>, UserRepostory>("UserRepository");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddKeyedScoped<ICRUD<UserDTO, UserInsertDTO, UserUpdateDTO>, UserService>("UserService");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
