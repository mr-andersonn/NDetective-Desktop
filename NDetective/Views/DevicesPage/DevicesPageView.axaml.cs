using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using NDetective.Models;
using NDetective.ViewModels;

namespace NDetective.Views;

public partial class DevicesPageView : UserControl
{
    private EditDeviceWindow? _editWindow;
    private AddDeviceWindow? _addWindow;
    
    
    public DevicesPageView()
    {
        InitializeComponent();
    }

    private void OpenEditDeviceWindow(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not DevicesPageViewModel parentVm)
            return;

        if (sender is not Button button || button.DataContext is not Device device)
            return;
        
        if (_editWindow is not null)
        {
            _editWindow.Activate();
            return;
        }
        
        parentVm.EditDeviceCommand.Execute(device);

        var vm = parentVm.CurrentEditVm;
        if (vm is null)
            return;
        
        _editWindow = new EditDeviceWindow { DataContext = vm };
        
        vm.RequestClose += _editWindow.Close;
        
        var screenPoint = button.PointToScreen(new Point(0, 0));
        _editWindow.Position = new PixelPoint(
            (int) (screenPoint.X + button.Bounds.Width + 50),
            (int) screenPoint.Y);

        
        // Sets _editWindow back to null on window closed event
        _editWindow.Closed += (obj, args) =>
        {
            _editWindow = null;
        };
        
        _editWindow.Show();
        
    }


    private void OpenAddDeviceWindow(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not DevicesPageViewModel parentVm)
            return;

        if (sender is not Button button)
            return;

        if (_addWindow is not null)
        {
            _addWindow.Activate();
            return;
        }
        
        parentVm.AddDeviceCommand.Execute(null);
        
        var vm = parentVm.CurrentAddVm;
        if (vm is null)
            return;
        
        _addWindow = new AddDeviceWindow { DataContext = vm };
        
        vm.DeviceAdded += _addWindow.Close;
        
        var screenPoint = button.PointToScreen(new Point(0, 0));
        _addWindow.Position = new PixelPoint(
            (int) (screenPoint.X + button.Bounds.Width + 50),
            (int) screenPoint.Y - (int) _addWindow.Height + (int) button.Height / 2);
        
        _addWindow.Closed += (_, _) =>
        {
            _addWindow = null;
        };
        
        _addWindow.Show();

        
    }
}