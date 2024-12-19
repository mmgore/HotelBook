using System.Reflection;
using FluentValidation.AspNetCore;
using HotelBook.Application.Automapper;
using HotelBook.Application.Commands.CreateHotel;
using HotelBook.Domain.AggregatesModel.HotelAggregate;
using HotelBook.Domain.SeedWork;
using HotelBook.Infrastructure;
using HotelBook.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers()
    .AddFluentValidation(vf => vf.RegisterValidatorsFromAssemblyContaining<CreateHotelCommand>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

ConfigureLogging();
builder.Host.UseSerilog();

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

builder.Services
    .AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

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

void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Console()
        .WriteTo.Debug()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfigration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetEntryAssembly().GetName().Name.ToLower().Replace(".","-")}-{environment.ToLower()}--{DateTime.UtcNow:yyyy-MM)}",
        NumberOfReplicas = 1,
        NumberOfShards = 2
    };
}