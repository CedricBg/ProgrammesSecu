using ProgrammesSecu.Models.Auth;
using ProgrammesSecu.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ProgrammesSecu.Helpers;
using static System.Net.WebRequestMethods;

namespace ProgrammesSecu.Services;


/// <summary>
/// Logout and clear all SecureStorage
/// </summary>
public class AuthServices
{
    BearerToken _httpClient;
    private string _url;
    private JsonSerializerOptions _serializerOptions;

    public AuthServices(BearerToken httpClient)
    {
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _httpClient = httpClient;
    }

    /// <summary>
    /// Supprsession des données dans secure storage qui renvoi a loginpage ensuite
    /// </summary>
    public async void Logout()
    {
        try
        {
            SecureStorage.Default.Remove("token");
            SecureStorage.Default.Remove("role");
            SecureStorage.Default.Remove("surName");
            SecureStorage.Default.Remove("firstName");
            SecureStorage.Default.Remove("id");


            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        catch(Exception ex) 
        {
            await Shell.Current.DisplayAlert("Not logged out", ex.Message, "Ok");
        }
    }

    /// <summary>
    /// Save Infortmation after Login in SecureStorage
    /// </summary>
    /// <param name="connectedForm"></param>
    /// <returns></returns>
    private async Task<bool> SaveInformation(ConnectedForm connectedForm)
    {
        try
        {
            await SecureStorage.Default.SetAsync("token", connectedForm.Token);
            await SecureStorage.Default.SetAsync("role", connectedForm.Role);
            await SecureStorage.Default.SetAsync("surName", connectedForm.SurName);
            await SecureStorage.Default.SetAsync("firstName", connectedForm.FirstName);
            await SecureStorage.Default.SetAsync("id", connectedForm.Id.ToString());
            return true;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Nous n'avons pas pu enregistré les données de l'utilisateur", ex.Message, "Ok");
            return false;
        }
    }

    public async Task<bool> Login(LoginForm form)
    {
        ConnectedForm connectedForm = new ConnectedForm();
        _url = "https://www."+await SecureStorage.Default.GetAsync("server")+"/api";
        HttpClient client = await _httpClient.GetClient();
        try
        {
            string jsonToDo = JsonSerializer.Serialize(form, _serializerOptions);
            StringContent stringContent = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync($"{_url}/Auth/login", stringContent);

            if (response.IsSuccessStatusCode)
            {
                connectedForm = await response.Content.ReadFromJsonAsync<ConnectedForm>();
                return await SaveInformation(connectedForm);
            }
            await Shell.Current.DisplayAlert("Erreur", "Veuillez vérifier les informations de connexion", "Ok");
            return false;
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", "Données pas envoyées", "Ok");
            return false;
        }


    }
}
