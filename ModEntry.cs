using System;
using StardewModdingAPI;
using StardewValley;

namespace StardewMod;

public class ModEntry : Mod
{
    private bool hasNotified = false;

    public override void Entry(IModHelper helper)
    {
        Monitor.Log("Stardew Comforts loaded!", LogLevel.Info);

        helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
    }

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
}
