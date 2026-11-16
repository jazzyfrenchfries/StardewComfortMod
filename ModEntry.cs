using System;
using System.Runtime.CompilerServices;
using StardewComforts;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewComforts.Features;

namespace StardewMod;

public class ModEntry : Mod
{
    private EnergyReminder energyReminder;
    private ModConfig Config = new();

    public override void Entry(IModHelper helper)
    {
        Config = helper.ReadConfig<ModConfig>();
        energyReminder = new EnergyReminder(helper,Config);
        energyReminder.Initialize();
        Monitor.Log("Stardew Comforts loaded!", LogLevel.Info);
        helper.Events.GameLoop.GameLaunched += OnGameLaunched;
      
    }
    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e) {
        var configMenu = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>(
            "spacechase0.GenericModConfigMenu");
        if (configMenu == null) {
            Monitor.Log(
                "Generic Mod Config Menu not found. Skipping settings menu.",
                LogLevel.Info);
            return;
        }
        configMenu.Register(
            ModManifest,
            reset: () => Config = new ModConfig(),
            save: () => Helper.WriteConfig(Config));

        configMenu.AddBoolOption(
            ModManifest,
            getValue: () => Config.EnableEnergyReminders,
            setValue: value => Config.EnableEnergyReminders = value,
            name: () => "Energy reminders",
            tooltip: () => "Get notified when your energy is low.");

        configMenu.AddBoolOption(
            ModManifest,
            getValue: () => Config.EnableBirthdayReminders,
            setValue: value => Config.EnableBirthdayReminders = value,
            name: () => "Birthday reminders",
            tooltip: () => "Get reminded about upcoming birthdays.");

        configMenu.AddBoolOption(
            ModManifest,
            getValue: () => Config.EnableFestivalReminders,
            setValue: value => Config.EnableFestivalReminders = value,
            name: () => "Festival reminders",
            tooltip: () => "Get reminded about upcoming festivals.");
    
        configMenu.AddNumberOption(
            ModManifest,
            getValue: () => Config.DaysBeforeReminder,
            setValue: value => Config.DaysBeforeReminder = value,
            name: () => "Days before reminder",
            tooltip: () => "How many days ahead should reminders appear?",
            min: 1,
            max: 7,
            interval: 1);

        Monitor.Log("Configuration menu registered!", LogLevel.Info);
    }
}
