﻿global using Microsoft.Xna.Framework;
global using Terraria;
global using Terraria.Audio;
global using Terraria.ID;
global using Terraria.ModLoader;

namespace StalkerPack.Items.Weapons
{
    /// <summary>
    /// Automatic guns
    /// </summary>
    public abstract class BaseAutomatic : ModItem
    {
        public virtual SoundStyle ShootNoise { get; }
        public int Spread = 1;

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
            Item.autoReuse = true;
            base.SetDefaults();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(Spread));
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}