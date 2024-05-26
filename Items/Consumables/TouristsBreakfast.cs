using Terraria.DataStructures;

namespace StalkerPack.Items.Consumables
{
    public class TouristsBreakfast : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.FoodParticleColors[Item.type] = [
                Color.White,
                Color.Wheat
            ];
            ItemID.Sets.IsFood[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(22, 22, BuffID.WellFed3, 57600);
            Item.value = Item.buyPrice(silver: 80);
            Item.rare = ItemRarityID.LightRed;
            base.SetDefaults();
        }
    }
}