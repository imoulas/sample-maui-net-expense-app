using ExpenseApp.Models;
using ExpenseApp.ViewModels;
using Microsoft.Maui.Graphics.Converters;

namespace ExpenseApp.Pages;

public class MasonryPage : ContentPage
{
	public MasonryPage()
	{
        BindingContext = new MasonryViewModel();
        MasonryViewModel viewModel = (MasonryViewModel)BindingContext;

        Grid grid = new()
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(),
                new ColumnDefinition()
            }
        };

        VerticalStackLayout stackLayout1 = new();
        VerticalStackLayout stackLayout2 = new();

        ColorTypeConverter converter = new ColorTypeConverter();

        int split = viewModel.Items.Count / 2;
        int counter = 0;
        foreach (ItemImageModel model in viewModel.Items)
        {
            StackLayout stack = new()
                {   BackgroundColor = (Color)(converter.ConvertFromInvariantString( model.Color )),
                    HeightRequest = model.LayoutHeight,
                    Margin = 5
                };

            if (counter < split) {
                stackLayout1.Add(stack);
            }
            else
            {
                stackLayout2.Add(stack);
            }
            counter++;
        }

        grid.Add(stackLayout1, 1);
        grid.Add(stackLayout2, 0);

        ScrollView scrollView = new()
        {
            Margin = new Thickness(20),
            Content = grid
        };

        Title = "Masonry demo";
        Content = scrollView;

    }
}
