using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PaginationMaui.Models;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<BreedResponse> GetBreedsAsync(string url)
    {
        var response = await _httpClient.GetStringAsync(url);
        return JsonConvert.DeserializeObject<BreedResponse>(response);
    }
}
