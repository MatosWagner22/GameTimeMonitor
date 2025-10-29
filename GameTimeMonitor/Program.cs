using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using GameTimeMonitor.Application.Hubs;           //1. Requerido para ControlHub
using GameTimeMonitor.Application.Interfaces;
using GameTimeMonitor.Application.Mappings;       // 2. Requerido para MappingProfile
using GameTimeMonitor.Application.Services;
using GameTimeMonitor.Application.Validators;   // 3. Requerido para el validador
using GameTimeMonitor.Domain.Interfaces;
using GameTimeMonitor.Infrastructure.Data;
using GameTimeMonitor.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configuración de FluentValidation (apuntando a una clase de validador)
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IRemoteControlCommandRepository, RemoteControlCommandRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IRemoteControlCommandService, RemoteControlCommandService>();


builder.Services.AddSingleton<IMapper>(provider =>
{
    // 1. Obtenemos el ILoggerFactory que nos está pidiendo el constructor
    var loggerFactory = provider.GetService<ILoggerFactory>();

    // 2. Creamos el objeto de configuración (el primer argumento)
    var config = new MapperConfigurationExpression();
    config.AddProfile(new MappingProfile());
    // (Si tienes más perfiles, añádelos aquí: config.AddProfile(new OtroPerfil());)

    // 3. Llamamos al constructor de 2 argumentos que nos pide tu compilador
    var mapperConfig = new MapperConfiguration(config, loggerFactory);

    // 4. Creamos y devolvemos el Mapper
    return mapperConfig.CreateMapper();
});
// ------------------------------------

// Add SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// (Recuerda que esta línea la dejaremos comentada hasta que implementes la autenticación)
// app.UseAuthorization();

app.MapControllers();

// Apuntamos al Hub en su nueva ubicación: GameTimeMonitor.Application.Hubs
app.MapHub<ControlHub>("/controlHub");

app.Run();