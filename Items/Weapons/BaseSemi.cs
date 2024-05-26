using StalkerPack.Items.Weapons.Pistols;
using StalkerPack.Items.Weapons.Rifles;
using Terraria.DataStructures;

namespace StalkerPack.Items.Weapons
{
    /// <summary>
    /// Semi auto guns
    /// </summary>
    public abstract class BaseSemi : ModItem
    {
        public virtual SoundStyle ShootNoise { get; }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.knockBack = 2f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = ShootNoise with
            {
                Volume = 0.3f,
                PitchRange = (-0.1f, 0.1f),
                MaxInstances = 0
            };
            Item.value = 100 * Item.damage;
            base.SetDefaults();
        }

        public override void HoldItem(Player player)
        {
            if (Item.ModItem is SVD || Item.ModItem is VSS)
                player.scope = true;
            base.HoldItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (Item.ModItem is Deagle)
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(1));
            }
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Item.ModItem is Deagle)
            {
                Projectile shot = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
                shot.GetGlobalProjectile<StalkerPackGlobalProjectile>().fromDeagle = true;
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}