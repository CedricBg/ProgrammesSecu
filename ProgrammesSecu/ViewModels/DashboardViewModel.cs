using ProgrammesSecu.Services;


namespace ProgrammesSecu.ViewModels;

public partial  class DashboardViewModel: BaseViewModel
{
    AuthServices _authServices;
    public DashboardViewModel(AuthServices authServices)
    {
        _authServices = authServices;
    }
    [RelayCommand]
    public void Logout()
    {
        _authServices.Logout();
    }



}


