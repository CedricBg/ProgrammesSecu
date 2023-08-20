using Plugin.NFC;
using ProgrammesSecu.Helpers;
using ProgrammesSecu.Services;
using ProgrammesSecu.Views;


namespace ProgrammesSecu.ViewModels;

public partial class NfcViewModel: BaseViewModel
{
    AuthServices _authServices;

    public NfcViewModel(AuthServices authServices)
    {
        _authServices = authServices;
    }

    [RelayCommand]
    void Logout()
    {
        _authServices.Logout();
    }
}