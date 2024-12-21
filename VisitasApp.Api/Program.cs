using VisitasApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using VisitasApp.Core.Domain.RepositoryContracts;
using VisitasApp.Infrastructure.Repositories;
using VisitasApp.Core.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<VisitasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the repository
builder.Services.AddScoped<IVisitaRepository, VisitaRepository>();
// Register the DbContext
builder.Services.AddScoped<IVisitasDbContext, VisitasDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Habilitar anotaciones Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
