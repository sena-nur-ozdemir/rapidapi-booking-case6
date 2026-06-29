namespace RapidApiBookingCase.Dtos.DashboardDtos
{
    public class WeatherDto
    {
        public WeatherLocationData location { get; set; }
        public WeatherCurrentData current { get; set; }
    }
    public class WeatherLocationData
    {
        public string name { get; set; }
        public string country { get; set; }
    }
    public class WeatherCurrentData
    {
        public double temp_c { get; set; }
        public double feelslike_c { get; set; }
        public double wind_kph { get; set; }
        public double humidity { get; set; }
        public WeatherConditionData condition { get; set; }
    }
    public class WeatherConditionData
    {
        public string text { get; set; }
        public string icon { get; set; }
    }
}