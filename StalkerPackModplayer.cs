using StalkerPack.Items.Other;
using System.Collections.Generic;

namespace StalkerPack
{
    public class StalkerPackModplayer : ModPlayer
    {
        public bool EnergyDrinkIsActive;

        public override void ResetEffects()
        {
            EnergyDrinkIsActive = false;
        }

        public override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
        {
            Item heartOfTheOasis = new();
            heartOfTheOasis.SetDefaults(ModContent.ItemType<Oasis>());
            rewardItems.Add(heartOfTheOasis);
        }

        public override void PostUpdateRunSpeeds()
        {
            if (EnergyDrinkIsActive)
            {
                Player.maxRunSpeed += 0.25f;
                Player.accRunSpeed += 0.25f;
                Player.runAcceleration += 0.25f;
            }
        }
    }
}