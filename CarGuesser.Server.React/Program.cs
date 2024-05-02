using CarGuesser.Model.Repositories;
using CarGuesser.Model.Repositories.Guess;
using CarGuesser.Model.Repositories.Leaderboard;
using CarGuesser.Model.Repositories.Session;
using CarGuesser.Model.Services.Car;
using CarGuesser.Model.Services.Client;
using CarGuesser.Model.Services.Guess;
using CarGuesser.Model.Services.Image;
using CarGuesser.Model.Services.Leaderboard;
using CarGuesser.Model.Services.Session;
using CarGuesser.Server.Model.Processors.Guess;
using CarGuesser.Server.Model.Processors.Leaderboard;
using CarGuesser.Server.Model.Processors.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CarGuesser.Server.React;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpContextAccessor();
        RegisterRepositories(builder.Services);
        RegisterServices(builder.Services);
        RegisterProcessors(builder.Services);

        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
        app.MapFallbackToFile("index.html");
        app.Run();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddSingleton<IRepository, Repository>();
        services.AddSingleton<IGuessRepository, GuessRepository>();
        services.AddSingleton<ISessionRepository, SessionRepository>();
        services.AddSingleton<ILeaderboardRepository, LeaderboardRepository>();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<ICarService, CarService>();
        services.AddSingleton<IClientService, ClientService>();
        services.AddSingleton<IGuessService, GuessService>();
        services.AddSingleton<IImageService, ImageService>();
        services.AddSingleton<ISessionService, SessionService>();
        services.AddSingleton<ILeaderboardService, LeaderboardService>();
    }

    private static void RegisterProcessors(IServiceCollection services)
    {
        services.AddSingleton<IGuessProcessor, GuessProcessor>();
        services.AddSingleton<ISessionProcessor, SessionProcessor>();
        services.AddSingleton<ILeaderboardProcessor, LeaderboardProcessor>();
    }
}