namespace SkyTrackAPI.Models;

public class CurrentWeather
{
        public double Temp_C { get; set; }
        public int Is_Day { get; set; }
        public double Wind_Kph { get; set; }
        public string Wind_Dir { get; set; }
        public int Humidity { get; set; }
        public int Cloud { get; set; }
        public double Feelslike_C { get; set; }
}