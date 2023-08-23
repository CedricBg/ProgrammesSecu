using ProgrammesSecu.Models.Auth;
using ProgrammesSecu.Services;
using ProgrammesSecu.Views;
using System.Diagnostics;

namespace ProgrammesSecu.ViewModels;

public partial class LoginViewModel: BaseViewModel
{

    [ObservableProperty]
    string _login, _passWord, _errorPassword, _errorLogin;

    AuthServices _authServices;
    ConnectivityServices _connectivityServices;

    [ObservableProperty]
    bool _isRunining;

    public LoginViewModel(AuthServices authServices, ConnectivityServices connectivityServices)
    {

        _authServices = authServices;
        _connectivityServices = connectivityServices;
    }
    /// <summary>
    /// Vérifie si les champs de formulaire sont vide ou null
    /// </summary>
    /// <returns></returns>
    async Task<bool> CheckForm()
    {
        if (PassWord == null || PassWord == "")
        {
            await Shell.Current.DisplayAlert("Erreur", "Pas données pour mot de passe", "Ok");
            return false;
        }
        if (Login == null || Login == "")
        {
            await Shell.Current.DisplayAlert("Erreur", "Pas données pour l'identifiant", "Ok");
            return false;
        }
        return true;
    }
    //Fonction qui sert pour la connexion
    [RelayCommand]
    async Task Loginn()
    { 
        if(await _connectivityServices.Test() == false)
            return;
        if(await CheckForm() == false)
            return;
        LoginForm form = new();
        form.Password = PassWord;
        form.Login = Login;
        IsRunining = true;
        if(await _authServices.Login(form))
            await Shell.Current.GoToAsync(nameof(DashboardPage));
        PassWord = "";
        IsRunining = false;
    }

    /// <summary>
    /// OnAppearing déplacé de LoginPage.cs vers le ViewModel fait usage de "Community toolkit Maui"
    /// </summary>
    [RelayCommand]
    async Task Appearing()
    {
        try
        {
            string response = await SecureStorage.Default.GetAsync("token");
       
            if (!(response is null))
            {
                await Shell.Current.GoToAsync(nameof(DashboardPage));
            }
            CheckServer();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message ,"Ok");
        }

    }
    public async void CheckServer()
    {
        string response = await SecureStorage.Default.GetAsync("server");
        Debug.WriteLine(response);
        if (response is null)
        {
            await Shell.Current.GoToAsync(nameof(ServerPage));
            return;
        }
    }
}
