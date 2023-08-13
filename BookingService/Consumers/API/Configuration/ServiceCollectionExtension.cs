using API.Logs;
using Application.Booking;
using Application.Booking.Ports.In;
using Application.Guest;
using Application.Guest.Ports.In;
using Application.Room;
using Application.Room.Ports.In;
using Data;
using Data.Booking;
using Data.Guest;
using Data.Room;
using Domain.Booking.Ports.Out;
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
        services.AddScoped<Domain.Room.Ports.Out.IRoomRepository, RoomRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
    }

    private static void RegisterPorts(IServiceCollection services)
    {
        services.AddScoped<IGuestManager, GuestManager>();
        services.AddScoped<Application.Room.Ports.In.IRoomManager, RoomManager>();
        services.AddScoped<IBookingManager, BookingManager>();
    }
}
