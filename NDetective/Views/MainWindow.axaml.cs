using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NDetective.ViewModels;
using NDetective.Models;

namespace NDetective.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel();

        PropertyChanged += OnWindowStateChanged;
    }

    private void OnWindowStateChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == Window.WindowStateProperty)
        {
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
                ShowInTaskbar = false;
                Hide();
            }
        }
    }
}