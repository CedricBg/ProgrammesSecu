using ProgrammesSecu.Helpers;
using ProgrammesSecu.Models.Planning;
using ProgrammesSecu.Services;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace ProgrammesSecu.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    AuthServices _authServices;
    AgentService _agentService;
    BearerToken _httpClient;
    static string _url;

   [ObservableProperty]
    string _response;

    [ObservableProperty]
    ImageSource _images;


    [ObservableProperty]
    MemoryStream _memory;

    [ObservableProperty]
    string _isworking;

    public DashboardViewModel(AuthServices authServices, AgentService agentService, BearerToken httpClient)
    {
        _authServices = authServices;
        _agentService = agentService;
        _httpClient = httpClient;
    }


    [RelayCommand]
    public void Logout()
    {
        _authServices.Logout();
    }
    [RelayCommand]
    public async Task Enter()
    {
        Response = await SecureStorage.Default.GetAsync("token");
        Debug.WriteLine(Response);
    }

    [RelayCommand]
    async Task Appearing()
    {
        try
        {
            _url = "https://www." + await SecureStorage.Default.GetAsync("server") + "/api";
            string id = await SecureStorage.Default.GetAsync("id");
            HttpClient client = await _httpClient.GetClient();
            HttpResponseMessage response = await client.GetAsync(_url + "/employee/loadfile/" + id);
            if (response.IsSuccessStatusCode)
            {

                byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
                MemoryStream stream = new MemoryStream(fileBytes);
                Images = ImageSource.FromStream(() => new MemoryStream(fileBytes));

            } 
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        Working work = await  _agentService.IsWorking();
        if(work.IsWorking is false)
        {
            Isworking = "Attention vous n'êtes pas service";
        }
        else
        {
            Isworking = "En Service"; 
        }

    }
}


