using System;
using System.Threading.Tasks;
using AvaloniaConfigEditorDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaConfigEditorDemo.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private string _jsonFilepath 
        = System.IO.Path.Combine(Environment.CurrentDirectory, "example.json");
    
    [ObservableProperty] private string _name = string.Empty;

    [ObservableProperty] private string _description = string.Empty;

    [ObservableProperty] private string _language = string.Empty;

    [ObservableProperty] private int _httpPort = 8080;

    [ObservableProperty] private int _httpsPort = 8081;

    [ObservableProperty] private bool _forceSsl = true;
    
    private ServerConfig _serverConfig = new ServerConfig();

    [RelayCommand]
    private async Task LoadJson()
    {
        if (!System.IO.File.Exists(JsonFilepath))
            return;

        await using System.IO.Stream stream = System.IO.File.OpenRead(JsonFilepath);
        _serverConfig = await System.Text.Json.JsonSerializer.DeserializeAsync<ServerConfig>(stream)
            .ConfigureAwait(true) ?? new ServerConfig();

        Name = _serverConfig.Name;
        Description = _serverConfig.Description;
        Language = _serverConfig.Language;
        HttpPort = _serverConfig.HttpPort;
        HttpsPort = _serverConfig.HttpsPort;
        ForceSsl = _serverConfig.ForceSsl;
    }

    [RelayCommand]
    private async Task SaveJson()
    {
        _serverConfig.Name = Name;
        _serverConfig.Description = Description;
        _serverConfig.Language = Language;
        _serverConfig.HttpPort = HttpPort;
        _serverConfig.HttpsPort = HttpsPort;
        _serverConfig.ForceSsl = ForceSsl;

        await using System.IO.Stream stream = System.IO.File.Create(JsonFilepath);
        await System.Text.Json.JsonSerializer.SerializeAsync(stream, _serverConfig)
            .ConfigureAwait(true);
    }
}