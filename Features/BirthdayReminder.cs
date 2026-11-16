using System;
using System.Runtime.CompilerServices;
using StardewComforts;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewComforts.Features;
public class BirthdayReminder
{
    private readonly IModHelper helper;
    private readonly ModConfig Config;
    private readonly IMonitor Monitor;
    private bool hasNotified = false;

    public BirthdayReminder(IModHelper helper, ModConfig Config, IMonitor Monitor) {
        this.helper = helper;
        this.Config = Config;
        this.Monitor = Monitor;
    }
    private void OnDayStarted(object? sender, EventArgs e) {
        Config.EnableBirthdayReminders = true;
        hasNotified = false;
        if(!Context.IsWorldReady) {
            return;
        }
        string[] seasons = {"spring", "summer", "fall","winter"};
        int currentDay = (Array.IndexOf(seasons, Game1.currentSeason) * 28) + (Game1.dayOfMonth -1);
        var characters = Game1.characterData;
        foreach (var character in characters) {
            int characterBirthday =  (Array.IndexOf(seasons, character.Value.BirthSeason) *28)+ (character.Value.BirthDay -1);
            int daysUntilBirthday = characterBirthday - currentDay;
            if(daysUntilBirthday < 0) {
                daysUntilBirthday += 112;
            }
            if(daysUntilBirthday == Config.DaysBeforeReminder && !hasNotified) {
                Game1.addHUDMessage(
                    new HUDMessage($"It is {character.Key} 's birthday, Give them a present!", HUDMessage.newQuest_type));
                hasNotified = true;
            }
        }
       
    }
    public void Initialize() {
        helper.Events.GameLoop.DayStarted += OnDayStarted;
    }
}
