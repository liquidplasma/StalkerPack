using StalkerPack.Projectiles;
using Terraria.DataStructures;

namespace StalkerPack.Items.Weapons
{
    public class GaussRifle : BaseSemi
    {
        public override void SetDefaults()
        {
            Item.damage = 667;
            Item.crit = 36;
            Item.useTime = Item.useAnimation = 75;
            Item.rare = ItemRarityID.Yellow;
            Item.width = Item.height = 48;
            Item.autoReuse = false;
            Item.value = ContentSamples.ItemsByType[ItemID.SniperRifle].value * 4;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.knockBack = 2f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
        }

        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<GaussGun>()] == 0)
            {
                Projectile gun = Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<GaussGun>(), 0, 0, player.whoAmI);
                if (gun.ModProjectile is GaussGun gauss)
                    gauss.GaussItem = Item;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SniperRifle)
                .AddIngredient(ItemID.FragmentVortex, 20)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}