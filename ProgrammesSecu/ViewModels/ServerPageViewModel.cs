
using ProgrammesSecu.Views;
using System.Net.Http;

namespace ProgrammesSecu.ViewModels;
/// <summary>
/// Ici si le serveur pour l'api n'est pas fournis on le demande et on le sauvegarde dans le secure storage
/// </summary>
public partial class ServerPageViewModel: BaseViewModel
{
    [ObservableProperty]
    string server;

    string _url;
    HttpClient _httpClient = new HttpClient();

    [RelayCommand]
    async Task  SetServer()
    {
        if(!(Server == string.Empty || Server == ""))
        {

            try
            {
                _url = "https://www." + Server + "/api";
                HttpResponseMessage response = await _httpClient.GetAsync(_url);
                await SecureStorage.Default.SetAsync("server", Server);
                await Shell.Current.GoToAsync(nameof(LoginPage));
                return;
            }
            catch
            {
                await Shell.Current.DisplayAlert("Erreur", "Vérifier les infos du serveur", "Ok");
            }
        }
    }

}
