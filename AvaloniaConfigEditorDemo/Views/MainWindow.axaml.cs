using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using AvaloniaConfigEditorDemo.Util;
using AvaloniaConfigEditorDemo.ViewModels;

namespace AvaloniaConfigEditorDemo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();

        if (PlatformUtil.IsWindows11())
            TransparencyLevelHint = [WindowTransparencyLevel.Mica];
    }
}