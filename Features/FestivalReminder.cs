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
    public FestivalReminder(IModHelper helper, ModConfig Config) {
        this.helper = helper;
        this.Config = Config;
    }
    private void OnDayStarted(object? sender, EventArgs e) {
        Config.EnableFestivalReminders = true;
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
            if (festivalSeason == Game1.currentSeason && festivalDay == Game1.dayOfMonth) {
                Game1.addHUDMessage(new HUDMessage(
                    $"{festivalName} is happening today!",
                    HUDMessage.newQuest_type));
            }
        }
    }
    public void Initialize() {
        helper.Events.GameLoop.DayStarted += OnDayStarted;
    }
}
