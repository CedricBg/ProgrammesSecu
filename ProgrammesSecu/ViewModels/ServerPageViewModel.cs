
using ProgrammesSecu.Services;
using ProgrammesSecu.Views;
using System.Diagnostics;
using System.Net.Http;

namespace ProgrammesSecu.ViewModels;
/// <summary>
/// Ici si le serveur pour l'api n'est pas fournis on le demande et on le sauvegarde dans le secure storage
/// </summary>
public partial class ServerPageViewModel: BaseViewModel
{
    [ObservableProperty]
    string server;

    ConnectivityServices _connect;

    string _url;
    HttpClient _httpClient;
    public ServerPageViewModel(ConnectivityServices connectivity, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _connect = connectivity;
    }

    [RelayCommand]
    async Task  SetServer()
    {

        if(!(Server == string.Empty || Server == ""))
        {
            try
            {
                if(!await _connect.Test())
                {
                    return;
                }
                else
                {
                    _url = "https://www." + Server + "/api";
                    HttpResponseMessage response = await _httpClient.GetAsync(_url);
                    await SecureStorage.Default.SetAsync("server", Server);
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                    return;
                }
            }
            catch
            {
                await Shell.Current.DisplayAlert("Erreur", "Vérifier les infos du serveur", "Ok");
            }
        }
    }

}
