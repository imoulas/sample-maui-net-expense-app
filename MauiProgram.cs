using ExpenseApp.Data;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ExpenseApp
{
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            ApplicationDbContext Db = new();

            bool firstTime = Db.Database.EnsureCreated();
            Debug.WriteLine($"FIRSTIME={firstTime}");

            Db.Dispose();

            return builder.Build();
        }
    }
}
