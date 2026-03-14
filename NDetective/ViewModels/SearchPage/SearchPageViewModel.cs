using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NDetective.Data;
using NDetective.Models;

namespace NDetective.ViewModels;

public partial class SearchPageViewModel : ViewModelBase
{
    private static readonly ScanManager _scanManager = new();
    public ObservableCollection<Device> Devices { get; } = new();
    public ObservableCollection<Device> SavedDevices { get; } = new(DeviceRepository.GetAll());

    public static bool ScanRunning;
    
    public ObservableCollection<DisplayedDevice> Authorized { get; } = new();
    public ObservableCollection<DisplayedDevice> Unauthorized { get; } = new(); 
    
    [RelayCommand]
    private async Task Scan()
    {
        ScanRunning = true;
        
        int i = 0;
        
        Console.WriteLine("Scan started.");
        
        while (ScanRunning)
        {
            Authorized.Clear();
        
            await _scanManager.RunArpScan();
            
            if (_scanManager.LastScan?.Devices is null) return;

            foreach (var d in _scanManager.LastScan.Devices)
            {
                bool isAuthorized = SavedDevices.Contains(d);

                if (isAuthorized)
                {
                    Authorized.Add(new DisplayedDevice(d, isAuthorized));
                }
                else
                {
                    var match = false;
                    foreach (var ud in Unauthorized)
                    {
                        if (!ud.Device.Equals(d)) continue;
                        
                        match = true;
                        break;



                    }

                    if (!match)
                    {
                        Unauthorized.Add(new DisplayedDevice(d, isAuthorized));
                    }
                }
            }

            Console.WriteLine($"Iteration {++i}");
            
            if(ScanRunning)
                await Task.Delay(5000);
        }
        
        Console.WriteLine("Scan stopped.");

    }
    
    [RelayCommand]
    private void Save()
    {
        _scanManager.SaveLastScanDevices();
    }

    [RelayCommand]
    private void Stop()
    {
        ScanRunning = false;
    }
}