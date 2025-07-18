using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using AvaloniaConfigEditorDemo.Util;
using AvaloniaConfigEditorDemo.ViewModels;
using AvaloniaConfigEditorDemo.Views;

namespace AvaloniaConfigEditorDemo;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        var theme = new FluentTheme();
        
        if (PlatformUtil.IsWindows11())
        {
            theme.Palettes[ThemeVariant.Light] = new ColorPaletteResources
            {
                RegionColor = Color.Parse("#80F3F3F3"),
                AltLow = Color.Parse("#80F3F3F3"),
                AltHigh = Color.Parse("#80F3F3F3")
            };

            theme.Palettes[ThemeVariant.Dark] = new ColorPaletteResources
            {
                RegionColor = Color.Parse("#1E000000"),
                AltLow = Color.Parse("#1E000000"),
                AltHigh = Color.Parse("#1E000000")
            };
        }
        
        Styles.Insert(0, theme); // Insert before other styles
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}