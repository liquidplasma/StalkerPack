using StalkerPack.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StalkerPack.Items.Other
{
    public class Harmonica : ModItem
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
            if (player.ownedProjectileCounts[ModContent.ProjectileType<HarmonicaProjectile>()] < 1)
            {
                BetterNewProjectile(
                    player,
                    player.GetSource_ItemUse(Item),
                    player.Center,
                    Vector2.Zero,
                    ModContent.ProjectileType<HarmonicaProjectile>(),
                    0,
                    0,
                    player.whoAmI
                );
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 3)
                .AddIngredient(ItemID.IronBar, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
