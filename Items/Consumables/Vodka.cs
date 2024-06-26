﻿using Terraria.DataStructures;

namespace StalkerPack.Items.Consumables
{
    public class Vodka : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.FoodParticleColors[Item.type] = [
                Color.Wheat,
                Color.White,
                Color.AntiqueWhite
            ];
            ItemID.Sets.IsFood[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(22, 22, BuffID.Tipsy, 57600, useGulpSound: true);
            Item.value = Item.buyPrice(silver: 4);
            Item.rare = ItemRarityID.Green;
            base.SetDefaults();
        }
    }
}