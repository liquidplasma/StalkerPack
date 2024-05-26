using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;

namespace StalkerPack.Items.Consumables
{
    public class Bread : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.FoodParticleColors[Item.type] = [
                new Color(180, 158, 140),
                new Color(98, 63, 39),
                new Color(182, 183, 177)
            ];
            ItemID.Sets.IsFood[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(22, 22, BuffID.WellFed, 57600);
            Item.value = Item.buyPrice(copper: 90);
            Item.rare = ItemRarityID.Green;
            base.SetDefaults();
        }
    }
}