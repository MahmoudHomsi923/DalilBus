using DalilBus.Helper;
using DalilBus.MVVM.ViewModels;
using DalilBus.MVVM.Views;
using DalilBus.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using DalilBus.Config;

namespace DalilBus;

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
                fonts.AddFont("Brands-Regular-400.otf", "FAB");
                fonts.AddFont("Free-Regular-400.otf", "FAR");
                fonts.AddFont("Free-Solid-900.otf", "FAS");
            });

        // Remove underline for DatePicker and TimePicker on Android
        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        {
#if ANDROID
        handler.PlatformView.Background = null;
#endif
        });
        Microsoft.Maui.Handlers.TimePickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
        {
#if ANDROID
        handler.PlatformView.Background = null;
#endif
        });

        // Pages register
        builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<AboutPage>();
		builder.Services.AddTransient<TravelsPage>();
        // ViewModels register
		builder.Services.AddTransient<MainPageViewModel>();
		builder.Services.AddTransient<TravelsPageViewModel>();
        // ApiClient register
        builder.Services.AddHttpClient<ApiClient>();
        // Services register
        builder.Services.AddSingleton<SharedDataService>();
        // AppShell register
        builder.Services.AddSingleton<AppShell>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
