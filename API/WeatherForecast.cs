namespace API;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary { get; set; } // For .Net 6 by default a string is nullable; For .Net 7+ ? means optional, until you turn off nullable in API.csproj
}
