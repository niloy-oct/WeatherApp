using WeatherApp.Model.ViewModel;

namespace WeatherApp.Pages;

public partial class WeatherInfo : ContentPage
{
	public WeatherInfo()
	{
		InitializeComponent();
        BindingContext = new WeatherInfoViewModel();
    }
}