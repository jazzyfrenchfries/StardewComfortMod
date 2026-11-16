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

public class FestivalReminder
{
    private readonly IModHelper helper;
    private readonly ModConfig Config;
    private readonly IMonitor Monitor;
    private bool hasNotified = false;
    public FestivalReminder(IModHelper helper, ModConfig Config, IMonitor Monitor) {
        this.helper = helper;
        this.Config = Config;
        this.Monitor = Monitor;
    }
    private void OnDayStarted(object? sender, EventArgs e) {
        Config.EnableFestivalReminders = true;
        hasNotified = false;
        if(!Config.EnableFestivalReminders || !Context.IsWorldReady) {
            return;
        }
        string[] seasons = {"spring", "summer", "fall","winter"};
        int currentDay = (Array.IndexOf(seasons, Game1.currentSeason) * 28) + (Game1.dayOfMonth -1);
        var festivals = DataLoader.Festivals_FestivalDates(Game1.content);
        foreach(var festival in festivals) {
            string festivalDate = festival.Key;
            string festivalName = festival.Value;
            int dayStart = festivalDate.Length;
            
            for (int i = 0; i < festivalDate.Length; i++) {
                if (char.IsDigit(festivalDate[i])) {
                    dayStart = i;
                    break;
                }
            }
            
            string festivalSeason = festivalDate.Substring(0, dayStart);
            string festivalDayText = festivalDate.Substring(dayStart);
            int festivalDay = int.Parse(festivalDayText);
            int festivalSeasonIndex = Array.IndexOf(seasons, festivalSeason);
            int festivalDateInYear = (festivalSeasonIndex* 28) + (festivalDay- 1);
            int daysUntilFestival = festivalDateInYear - currentDay;
            if(daysUntilFestival < 0) {
                daysUntilFestival += 112;
            }
            if (daysUntilFestival == Config.DaysBeforeReminder && !hasNotified) {
                Game1.addHUDMessage(new HUDMessage(
                    $"{festivalName} is {daysUntilFestival} days away!",
                    HUDMessage.newQuest_type));
                hasNotified = true;
            }
            
        }
    }
    public void Initialize() {
        helper.Events.GameLoop.DayStarted += OnDayStarted;
    }
}
