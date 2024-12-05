using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Services;

namespace WeatherApp.Model.ViewModel
{
    internal partial class WeatherInfoViewModel : ObservableObject
    {
        private readonly WeatherApiService _weatherApiService;

        public WeatherInfoViewModel()
        {
            _weatherApiService = new WeatherApiService();
        }

        [ObservableProperty]
        private string lattitude = "23.6850";
        [ObservableProperty]
        private string longitude = "90.3563";
        [ObservableProperty]
        private string weatherIcon;
        [ObservableProperty]
        private string temperature;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty] private string weatherDescription;
        [ObservableProperty] private string location;
        [ObservableProperty] private string humidity;
        [ObservableProperty] private string cloudConverLevel;
        [ObservableProperty] private string isDay;


        public IAsyncRelayCommand FetchWeatherInformationCommand => new AsyncRelayCommand(FetchWeatherInformation);
        public async Task FetchWeatherInformation()
        {

            try
            {
                isLoading = true;
                await Task.Delay(2000);

                var weatherApiResponse = await _weatherApiService.GetWeatherInformation(Lattitude, Longitude);
                if (weatherApiResponse != null)
                {
                    // Update properties with API response data
                    WeatherIcon = weatherApiResponse.Current.weather_icons[0];
                    Temperature = $"{weatherApiResponse.Current.temperature}°C";
                    WeatherDescription = weatherApiResponse.Current.weather_descriptions.FirstOrDefault() ??
                                         "No description available";
                    Location =
                        $"{weatherApiResponse.Location.name}, {weatherApiResponse.Location.region}, {weatherApiResponse.Location.country}";
                    Humidity = $"{weatherApiResponse.Current.humidity}%";
                    CloudConverLevel = $"{weatherApiResponse.Current.cloudcover}%";
                    IsDay = weatherApiResponse.Current.is_day.Equals("yes", StringComparison.OrdinalIgnoreCase)
                        ? "Day"
                        : "Night";
                }
                else
                {
                    // Fallback values in case of null API response
                    WeatherIcon = string.Empty;
                    Temperature = "N/A";
                    WeatherDescription = "Unable to fetch weather description";
                    Location = "Unable to fetch location";
                    Humidity = "N/A";
                    CloudConverLevel = "N/A";
                    IsDay = "Unknown";
                }

                isLoading = false;
            }
            catch(Exception ex) 
            {
                IsLoading = false;
            }

            
        }

    }
}
