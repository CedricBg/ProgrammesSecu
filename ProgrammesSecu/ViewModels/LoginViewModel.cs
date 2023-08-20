using ProgrammesSecu.Models.Auth;
using ProgrammesSecu.Services;
using ProgrammesSecu.Views;


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

    [RelayCommand]
    async void Loginn()
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

    [RelayCommand]
    void Appearing()
    {
        try
        {

            Shell.Current.DisplayAlert("Ok", "Test", "ok");

        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.ToString());
        }
    }
}
