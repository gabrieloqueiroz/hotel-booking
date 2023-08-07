using API.Logs;
using Application.Guest;
using Application.Guest.Ports.In;
using Application.Room;
using Application.Room.Ports.In;
using Data;
using Data.Guest;
using Data.Room;
using Domain.Guest.Ports.Out;
using Domain.Room.Ports.Out;
using Microsoft.EntityFrameworkCore;

namespace API.Configuration;

public static class ServiceCollectionExtension
{

    public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<LogModel>();
        RegisterRepositories(services, configuration);
        RegisterPorts(services);
    }

    private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Main");
        services.AddDbContext<HotelDbContext>(
            options => options.UseSqlServer(connectionString));

        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
    }

    private static void RegisterPorts(IServiceCollection services)
    {
        services.AddScoped<IGuestManager, GuestManager>();
        services.AddScoped<IRoomManager, RoomManager>();
    }
}
