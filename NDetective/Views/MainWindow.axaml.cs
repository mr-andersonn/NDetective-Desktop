using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

    }

    /*
    private CancellationTokenSource? _cts;
    private async void RunScan_Click(object sender, RoutedEventArgs e)
    {
        RunButton.IsEnabled = false;
        StopButton.IsEnabled = true;
        
        _cts = new CancellationTokenSource();
        CancellationToken token = _cts.Token;
        
        var sm = new ScanManager();

        while (!token.IsCancellationRequested)
        {
            await sm.RunArpScan();

            var devices = sm.GetLastScan()?.Devices
                .OrderBy(d => BitConverter.ToUInt32(System.Net.IPAddress.Parse(d.Ip).GetAddressBytes().Reverse().ToArray()))
                .ToList();
            
            DeviceList.ItemsSource = (devices is null || devices.Count == 0)
                ? new List<string> { "No devices found." }
                : devices.Select(d => $"IP: {d.Ip} | MAC: {d.Mac}").ToList();
            
            Console.WriteLine("Scan completed");

            await Task.Delay(3_000);
        }

        RunButton.IsEnabled = true;
    }

    private void StopScan_Click(object sender, RoutedEventArgs e)
    {
        _cts?.Cancel();
        StopButton.IsEnabled = false;
    }
    */
    
}