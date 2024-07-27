using StalkerPack.Projectiles.Svarog;

namespace StalkerPack.Items.Other
{
    public class SvarogItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 32;
            Item.rare = ItemRarityID.Purple;
            base.SetDefaults();
        }

        public override void UpdateInventory(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ChestFinder>()] == 0)
                Projectile.NewProjectileDirect(player.GetSource_Accessory(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<ChestFinder>(), 0, 0, player.whoAmI);
            if (player.ownedProjectileCounts[ModContent.ProjectileType<HeartFinder>()] == 0)
                Projectile.NewProjectileDirect(player.GetSource_Accessory(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<HeartFinder>(), 0, 0, player.whoAmI);
            if (player.ownedProjectileCounts[ModContent.ProjectileType<LifeFruitFinder>()] == 0)
                Projectile.NewProjectileDirect(player.GetSource_Accessory(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<LifeFruitFinder>(), 0, 0, player.whoAmI);
        }
    }
}