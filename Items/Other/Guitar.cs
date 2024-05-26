using StalkerPack.Helpers;
using StalkerPack.Projectiles;

namespace StalkerPack.Items.Other
{
    public class Guitar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = Item.height = 32;
            Item.rare = ItemRarityID.Cyan;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            base.SetDefaults();
        }

        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<GuitarProjectile>()] < 1)
                BetterNewProjectile(player, player.GetSource_ItemUse(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<GuitarProjectile>(), 0, 0, player.whoAmI);
        }
    }
}