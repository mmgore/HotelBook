using Microsoft.EntityFrameworkCore;
using Report.API.Domain.Interfaces;
using Report.API.Infrastructure;
using Report.API.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services
    .AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services
    .AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services
    .AddScoped<IHotelRepository, HotelRepository>();

builder.Services
    .AddScoped<IHotelInformationRepository, HotelInformationRepository>();

builder.Services
    .AddDbContext<ReportContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("HotelDatabase")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
