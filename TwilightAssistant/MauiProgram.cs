using Microsoft.Extensions.DependencyInjection;
using TwilightAssistant.Services;
using TwilightAssistant.ViewModels;
using TwilightAssistant.Pages;

namespace TwilightAssistant;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//Register Services
		builder.Services.AddTransient<PlayerProfileServices>();
		builder.Services.AddTransient<GamePlayerServices>();
		builder.Services.AddTransient<RaceServices>();
		builder.Services.AddTransient<GameServices>();
		builder.Services.AddTransient<StrategyCardServices>();
		

		//Register View Models
		builder.Services.AddSingleton<TimingTestViewModel>();
		builder.Services.AddTransient<MainPageViewModel>();
		builder.Services.AddTransient<GamePageViewModel>();
		builder.Services.AddTransient<PlayerProfileViewModel>();
		builder.Services.AddTransient<SelectPlayersViewModel>();
		builder.Services.AddTransient<SelectRaceViewModel>();
		builder.Services.AddSingleton<GetRaceViewModel>();
		builder.Services.AddTransient<SelectWinnerViewModel>();
		builder.Services.AddTransient<WinnerPageViewModel>();


		//Register Pages
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<GamePage>();
		builder.Services.AddTransient<PlayerProfilePage>();
		builder.Services.AddTransient<SelectPlayersPage>();
		builder.Services.AddTransient<SelectRacePage>();
		builder.Services.AddSingleton<GetRacePage>();
		builder.Services.AddTransient<GamePage3>();
		builder.Services.AddTransient<SelectWinnerPage>();
		builder.Services.AddTransient<WinnerPage>();

		//Build
		return builder.Build();
	}
}
