using Avalonia.Controls.ApplicationLifetimes;

namespace NDetective.Models;

public class TrayService
{
    private readonly IClassicDesktopStyleApplicationLifetime _desktop;

    public TrayService(IClassicDesktopStyleApplicationLifetime desktop)
    {
        _desktop = desktop;
    }

    public void OpenMainWindow()
    {
        if (_desktop.MainWindow is null) return;

        _desktop.MainWindow.ShowInTaskbar = true;
        _desktop.MainWindow.Show();
        _desktop.MainWindow.Activate();
    }

    public void HideMainWindow()
    {
        if (_desktop.MainWindow is null) return;

        _desktop.MainWindow.ShowInTaskbar = false;
        _desktop.MainWindow.Hide();
    }

    public void ExitApplication()
    {
        _desktop.Shutdown();
    }

    public void SetNormalState()
    {
        // Switch tray icon to normal
    }

    public void SetAlertState()
    {
        // Switch tray icon to alert
    }
    
    
}