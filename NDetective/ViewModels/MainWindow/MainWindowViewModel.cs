using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NDetective.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        // --- Getting initial icons for the menu
        
        PaneClosedIcon = (StreamGeometry?)Application.Current?.FindResource("arrow_right_regular");
        PaneOpenIcon = (StreamGeometry?)Application.Current?.FindResource("arrow_left_regular");
        CurrentPaneIcon = IsPaneOpen ? PaneOpenIcon : PaneClosedIcon;
    }
    
    // ---  Menu
    
    [ObservableProperty] private bool _isPaneOpen = true;
    [ObservableProperty] private StreamGeometry? _paneClosedIcon;
    [ObservableProperty] private StreamGeometry? _paneOpenIcon;
    [ObservableProperty] private StreamGeometry? _currentPaneIcon;
    
    [RelayCommand]
    private void HideOpenMenu()
    {
        IsPaneOpen = !IsPaneOpen;
        CurrentPaneIcon = IsPaneOpen ? PaneOpenIcon : PaneClosedIcon;
    }
    
    // ---  TransitioningContentControl logic

    [ObservableProperty] 
    private ViewModelBase _currentPage = new SearchPageViewModel();

    [ObservableProperty]
    private ListItemTemplate _selectedListItem;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;
        var instance = Activator.CreateInstance(value.ModelType);
        if (instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }
    
    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(SearchPageViewModel), "search_square_regular"),
        new ListItemTemplate(typeof(DevicesPageViewModel), "apps_list_regular"),
        new ListItemTemplate(typeof(ScansPageViewModel), "book_question_mark_regular"),
        new ListItemTemplate(typeof(SettingsPageViewModel), "settings_regular"),
        
    };
}

