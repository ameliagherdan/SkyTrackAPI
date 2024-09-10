namespace SkyTrackAPI.Models;

public class Location
{
    public string Name { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public long Localtime_Epoch { get; set; }
    public string Localtime { get; set; }
}