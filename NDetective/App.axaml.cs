using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using NDetective.Models;
using NDetective.ViewModels;
using NDetective.Views;

namespace NDetective;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private TrayService? _trayService;
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };

            _trayService = new TrayService(desktop);
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    private void OpenApp(object? sender, EventArgs e)
    {
        _trayService?.OpenMainWindow();
    }
    
    private void HideApp(object? sender, EventArgs e)
    {
        _trayService?.HideMainWindow();
    }

    private void CloseApp(object? sender, EventArgs e)
    {
        _trayService?.ExitApplication();
    }


    private void TrayClicked(object? sender, EventArgs e)
    {
        _trayService?.OpenMainWindow();
    }
}