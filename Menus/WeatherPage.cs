
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;
using StardewComforts.Features;
using StardewValley.BellsAndWhistles;

namespace StardewComforts.Menus;

public class WeatherPage : IClickableMenu
{
    private readonly WeatherReminder weatherReminder;

    public WeatherPage(
        int x,
        int y,
        int width,
        int height,
        WeatherReminder weatherReminder
    ) : base(x, y, width, height) {
        this.weatherReminder = weatherReminder;
    }

    private string GetWeatherName() {
        return weatherReminder.tomorrowWeather switch {
            "Sun" => "Sunny",
            "Rain" => "Rainy",
            "Storm" => "Stormy",
            "Wind" => "Windy",
            "Snow" => "Snowy",
            "GreenRain" => "Green Rain",
            "Festival" => "Festival",
            "Wedding" => "Wedding",
            _ => "Unknown"
        };
    }

    public override void draw(SpriteBatch b) {
        base.draw(b);

        SpriteText.drawString(
            b,
            "Tomorrow's Weather",
            xPositionOnScreen + 80,
            yPositionOnScreen + 100);

        SpriteText.drawString(
            b,
            GetWeatherName(),
            xPositionOnScreen + 80,
            yPositionOnScreen + 180);
    }
}
