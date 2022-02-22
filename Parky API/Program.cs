using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.ParkyMapper;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//------------------------------------------------------------------

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(x =>
{
    x.UseSqlServer(connectionString);
});

builder.Services.AddScoped<INationalParkRepository, NationalParkRepository>();

builder.Services.AddAutoMapper(typeof(ParkyMappings));


//------------------------------------------------------------------

builder.Services.AddControllers();
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
