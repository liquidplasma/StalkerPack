namespace StalkerPack.Items.Weapons.Rifles
{
    public class SVD : BaseSemi
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(SVD) + "/shot"); }

        public override void SetDefaults()
        {
            Item.damage = 159;
            Item.crit = 29;
            Item.useTime = Item.useAnimation = 26;
            Item.rare = ItemRarityID.Yellow;
            Item.width = 62;
            Item.height = 24;
            Item.autoReuse = true;
            Item.value = ContentSamples.ItemsByType[ItemID.SniperRifle].value;
            base.SetDefaults();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
                type = ProjectileID.BulletHighVelocity;
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override Vector2? HoldoutOffset()
        {
            return new(-10, 0);
        }
    }
}