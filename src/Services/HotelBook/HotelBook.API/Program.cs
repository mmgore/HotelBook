using System.Reflection;
using FluentValidation.AspNetCore;
using HotelBook.Application.Commands.CreateHotel;
using HotelBook.Domain.AggregatesModel.HotelAggregate;
using HotelBook.Domain.SeedWork;
using HotelBook.Infrastructure;
using HotelBook.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers()
    .AddFluentValidation(vf => vf.RegisterValidatorsFromAssemblyContaining<CreateHotelCommand>());
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
    .AddDbContext<HotelContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("HotelDatabase")));

builder.Services
    .AddMediatR(typeof(CreateHotelCommandHandler).GetTypeInfo().Assembly);

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