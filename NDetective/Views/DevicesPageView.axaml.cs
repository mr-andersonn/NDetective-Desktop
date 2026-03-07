using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace NDetective.Views;

public partial class DevicesPageView : UserControl
{
    public DevicesPageView()
    {
        InitializeComponent();
    }

    private void OpenEditDeviceWindow(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            var editWindow = new EditDeviceWindow();

            var screenPoint = button.PointToScreen(new Point(0, 0));

            editWindow.Position = new PixelPoint(
                (int) (screenPoint.X + button.Bounds.Width * 1.5),
                (int)screenPoint.Y);
            
            editWindow.Show();   
        }
        

    }
}