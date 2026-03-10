using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NDetective.Data;
using NDetective.Models;
using NDetective.Views;

namespace NDetective.ViewModels;

public partial class DevicesPageViewModel : ViewModelBase
{
    private readonly ScanManager _scanManager = new();
    public ObservableCollection<Device> Devices { get; }
    
    public EditDeviceViewModel? CurrentEditVm { get; private set; }
    
    public DevicesPageViewModel()
    {
        Devices = new ();
        
        LoadDevices();
        
    }

    private void LoadDevices()
    {
        Devices.Clear();
        
        foreach(var d in DeviceRepository.GetAll()) 
        {
            Devices.Add(d);
        }
    }
    
    [ObservableProperty] private bool _isEditWindowOpen;


    
    [RelayCommand]
    private void EditDevice(Device d)
    {
        IsEditWindowOpen = true;

        var vm = new EditDeviceViewModel(d);
        vm.DeviceDeleted += LoadDevices;

        CurrentEditVm = vm;
        OnPropertyChanged(nameof(CurrentEditVm));

    }

    
}