using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NDetective.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    // ---  Menu
    
    [ObservableProperty]
    private bool _isPaneOpen = true;

    [RelayCommand]
    private void HideOpenMenu()
    {
        IsPaneOpen = !IsPaneOpen;
    }
    
    
    
    // ---  TransitioningContentControl

    [ObservableProperty] 
    private ViewModelBase _currentPage = new SearchPageViewModel();
}

