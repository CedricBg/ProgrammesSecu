using ProgrammesSecu.Services;
using System.Diagnostics;
using System.Transactions;

namespace ProgrammesSecu.ViewModels;

public partial  class DashboardViewModel: BaseViewModel
{
    AuthServices _authServices;

    [ObservableProperty]
    string _response;

    public DashboardViewModel(AuthServices authServices)
    {
        _authServices = authServices;
    }
    [RelayCommand]
    public  void Logout()
    {
        _authServices.Logout();
    }
    [RelayCommand]
    public async Task Enter()
    {
        Response = await SecureStorage.Default.GetAsync("token");
        Debug.WriteLine(Response);
    }
}


