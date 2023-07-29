using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PasswordGenerator.Data;
using PasswordGenerator.ViewModel;

namespace PasswordGenerator;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Database and Services
		builder.Services.AddSingleton<HistoryDatabase>();

		// Pages
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<HistoryPage>();
		
		// ViewModels
		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddSingleton<HistoryViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
