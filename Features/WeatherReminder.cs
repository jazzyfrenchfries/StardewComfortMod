using System;
using System.Runtime.CompilerServices;
using StardewComforts;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewComforts.Features;
using StardewValley.GameData;
using StardewValley.Delegates;

namespace StardewComforts.Features;

public class WeatherReminder
{
    public string tomorrowWeather = Game1.weatherForTomorrow;
    
}
