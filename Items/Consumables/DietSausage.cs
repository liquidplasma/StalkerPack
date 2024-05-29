using Terraria.DataStructures;

namespace StalkerPack.Items.Consumables
{
    public class DietSausage : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.FoodParticleColors[Item.type] = [
                new Color(160, 71, 87),
                new Color(140, 83, 96)
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