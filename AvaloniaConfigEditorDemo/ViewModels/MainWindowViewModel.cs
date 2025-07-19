using System;
using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaConfigEditorDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaConfigEditorDemo.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<NavigationListItem> Items { get; } = new()
    {
        new NavigationListItem(typeof(ServerConfigurationPageViewModel), "SettingsRegular"),
        new NavigationListItem(typeof(SessionManagementPageViewModel), "AppsListRegular")
    };

    [ObservableProperty] private NavigationListItem? _selectedItem;

    [ObservableProperty] private bool _isPaneOpen = true;

    [ObservableProperty] private ViewModelBase _currentPage = new ServerConfigurationPageViewModel();

    [RelayCommand]
    private void TogglePane() => IsPaneOpen = !IsPaneOpen;

    [RelayCommand]
    private static void ExitApplication()
    {
        Environment.Exit(0);
    }

    partial void OnSelectedItemChanged(NavigationListItem? value)
    {
        if (value is null)
            return;

        var page = Activator.CreateInstance(value.PageType) as ViewModelBase;
        if (page is null)
            return;
        CurrentPage = page;
    }
}

public sealed class NavigationListItem
{
    public NavigationListItem(Type pageType, string iconKey)
    {
        Label = MakeLabel(pageType);
        PageType = pageType;

        if (Application.Current!.TryFindResource(iconKey, out var resource))
            Icon = (StreamGeometry)resource!;
    }

    public string Label { get; }
    public Type PageType { get; }
    public StreamGeometry? Icon { get; }

    private static string MakeLabel(Type pageType)
    {
        var label = pageType.Name.Replace("PageViewModel", string.Empty);
        for (var i = 1; i < label.Length; i++)
        {
            if (!char.IsUpper(label[i])) continue;
            label = label.Insert(i, " ");
            i++;
        }
        return label;
    }
}