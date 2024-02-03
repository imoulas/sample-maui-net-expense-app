using ExpenseApp.Data;
using ExpenseApp.Models;
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

            if( firstTime )
            {
                Db.Categories.Add(new CategoryModel
                {
                    Name = "Supermarket"
                });

                Db.Categories.Add(new CategoryModel
                {
                    Name = "Fashion"
                });

                Db.Categories.Add(new CategoryModel
                {
                    Name = "Fun"
                });

                string[] colors = { "red", "green", "yellow", "brown" };
                int[] heights = { 40, 80, 100, 90 };

                Random rnd = new Random();
                for (int i=0;i<100;i++) {
                    Db.ItemImages.Add(new ItemImageModel
                    {
                        Name = $"Image {i}",
                        ImageName = i.ToString(),
                        Color = colors[rnd.Next(3)],
                        LayoutHeight = heights[rnd.Next(3)]
                    });
                }

                Db.SaveChanges();

            }

            Db.Dispose();

            return builder.Build();
        }
    }
}
