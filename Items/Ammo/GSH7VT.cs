using StalkerPack.Projectiles.Warheads;

namespace StalkerPack.Items.Ammo
{
    /// <summary>
    /// Bunker buster
    /// </summary>
    public class GSH7VT : BaseWarheadItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 30;
            Item.damage = 38;
            Item.shoot = ModContent.ProjectileType<GSH7VTProj>();
            Item.value = (int)(ContentSamples.ItemsByType[ItemID.RocketI].value * 1.05f);
            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.RocketI)
                .AddIngredient(ItemID.Cog)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}