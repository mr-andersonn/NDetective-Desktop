using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NDetective.Data;
using NDetective.Models;

namespace NDetective.ViewModels;

public partial class AddDeviceViewModel : ViewModelBase
{

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddDeviceCommand))]
    private string _mac = "";
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddDeviceCommand))]
    private string _ip = "";
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddDeviceCommand))]
    private string _name = "";
    
    [ObservableProperty]
    private string _description = "";
    
    
    public event Action? DeviceAdded;

    [RelayCommand(CanExecute = nameof(CanAddDevice))]
    private void AddDevice()
    {
        var d = !string.IsNullOrWhiteSpace(Description) 
            ? new Device(Ip, Mac, Name, Description) 
            : new Device(Ip, Mac, Name);
        
        DeviceRepository.Add(d);
        
        DeviceAdded?.Invoke();
        
    }

    private bool CanAddDevice()
    {
        return !string.IsNullOrWhiteSpace(Mac) &&
               !string.IsNullOrWhiteSpace(Ip) &&
               !string.IsNullOrWhiteSpace(Name);
    }
}