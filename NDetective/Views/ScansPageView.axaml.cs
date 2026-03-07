using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace NDetective.Views;

public partial class ScansPageView : UserControl
{

    private const int Rows = 10;
    private const int Cols = 10;
    private readonly IBrush _baseColor = Brushes.LightGray;

    private bool _stop = true;
    
    
    public ScansPageView()
    {
        InitializeComponent();
        
        InitGrid();
    }

    private void InitGrid()
    {
        ScansGrid.Height = 500;
        ScansGrid.Width = 500;
        
        for (var j = 0; j < Rows; j++)
        {
            ScansGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
            ScansGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
            
            for (var i = 0; i < Cols; i++)
            {
                var child = new Rectangle()
                {
                    Width = 40, Height = 40,
                    Fill = _baseColor,
                    
                };
                ScansGrid.Children.Add(child);
                Grid.SetRow(child, j);
                Grid.SetColumn(child, i);
            }
        }
    }

    private async void StartGridAnimation(object? sender, RoutedEventArgs e)
    {
        _stop = false;
        
        Rectangle? previous = null;
        foreach (var child in ScansGrid.Children)
        {
            if (_stop) break;
            if (child is not Rectangle rect) continue;
            if (previous is not null) previous.Fill = _baseColor;
            rect.Fill = Brushes.MediumSeaGreen;
            previous = rect;
            await Task.Delay(100);
        }

        if (previous is not null) previous.Fill = _baseColor;
    }

    private void StopGridAnimation(object? sender, RoutedEventArgs e)
    {
        _stop = true;
        foreach (var child in ScansGrid.Children)
        {
            if (child is not Rectangle rect) continue;
            rect.Fill = _baseColor;
        }
    }
}