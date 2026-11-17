using System;
using StardewModdingAPI;
using StardewValley;
namespace StardewComforts;

public class ModConfig
{
    public bool EnableEnergyReminders {get; set;} = true;
    public bool EnableBirthdayReminders { get; set; } = true;
    public bool EnableFestivalReminders { get; set; } = true;
    public bool EnableWeatherReminders {get; set;} = true;
    public int DaysBeforeReminder { get; set; } = 1;
    
}
