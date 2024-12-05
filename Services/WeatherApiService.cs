using System.Net.Http.Json;
using WeatherApp.Model.APIResponse;

namespace WeatherApp.Services
{
    internal class WeatherApiService
    {
        private readonly HttpClient _httpClient;

        public WeatherApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Constants.API_BASE_URL);
        }

        public async Task<WeatherAPIResponse> GetWeatherInformation(string lat, string lng)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return null;
            }

            return await _httpClient.GetFromJsonAsync<WeatherAPIResponse>(
                $"current?access_key={Constants.API_KEY}&query={lat},{lng}");
        }
    }
}
