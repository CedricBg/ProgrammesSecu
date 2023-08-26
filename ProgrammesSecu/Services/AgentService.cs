using ProgrammesSecu.Helpers;
using ProgrammesSecu.Models.Planning;
using System.Net.Http.Json;

namespace ProgrammesSecu.Services;

public class AgentService
{
    BearerToken _httpClient;
    string _url;
    string _server;
    string _id;
    Working _workTime;
    public AgentService(BearerToken httpclient)
    {
        _httpClient = httpclient;   
    }

    public async Task<Working> IsWorking()
    {
        _server = await SecureStorage.Default.GetAsync("server");
        _id = await SecureStorage.Default.GetAsync("id");
        _url = "https://www." + _server + "/api/planning/working/"+_id;
        HttpClient client = await _httpClient.GetClient();
        
        HttpResponseMessage message = await client.GetAsync(_url);
        if(message.IsSuccessStatusCode)
        {
            _workTime = await message.Content.ReadFromJsonAsync<Working>();
            return _workTime;
        }
        else
        {
            return null;
        }
    }
}
