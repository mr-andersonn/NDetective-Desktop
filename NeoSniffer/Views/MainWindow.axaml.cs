using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NetSniffer.Models;

namespace NetSniffer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void RunScan_Click(object sender, RoutedEventArgs e)
    {
        var sm = new ScanManager();
        
        await sm.RunArpScan();

        var devices = sm.GetLastScan()?.Devices
            .OrderBy(d => BitConverter.ToUInt32(System.Net.IPAddress.Parse(d.Ip).GetAddressBytes().Reverse().ToArray()))
            .ToList();
        
        if (devices is null || devices.Count == 0)
        {
            DeviceList.ItemsSource = new List<string> { "No devices found." };
        }
        else
        {
            // Set the Items property to a list of formatted strings
            DeviceList.ItemsSource = devices.Select(d => $"IP: {d.Ip} | MAC: {d.Mac}").ToList();
        }
        
    }
}