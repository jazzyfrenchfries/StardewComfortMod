using System;
using System.Runtime.CompilerServices;
using StardewComforts;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
namespace StardewComforts.Features;

public class EnergyReminder
{
    private bool hasNotified = false;
    private readonly IModHelper helper;
    private readonly ModConfig Config;
    private void OnUpdateTicked(object? sender, EventArgs e)
    {
        if (!Context.IsWorldReady)
        {
            return;
        }

        float currentEnergy = Game1.player.Stamina;
        float maxEnergy = Game1.player.MaxStamina;

        float energyPercentage = (currentEnergy / maxEnergy) * 100;

        if (energyPercentage <= 25 && !hasNotified)
        {
            Game1.addHUDMessage(
                new HUDMessage("Your energy is getting low!", HUDMessage.newQuest_type)
            );

            hasNotified = true;
        }

        if (energyPercentage > 25)
        {
            hasNotified = false;
        }
    }

    public void Initialize() {
        helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
    }
    public EnergyReminder(IModHelper helper, ModConfig Config) {
        this.helper = helper;
        this.Config = Config;

    }
}
