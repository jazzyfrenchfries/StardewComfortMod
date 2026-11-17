using StardewValley;

namespace StardewComforts.Features;

public class WeatherReminder
{
    public string tomorrowWeather
    {
        get
        {
            string weather = Game1.weatherForTomorrow;
            return weather ?? "NULL";
        }
    }
}