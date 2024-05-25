namespace StalkerPack.Items.Weapons.Rifles
{
    public class SVD : BaseSemi
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(SVD) + "/shot"); }

        public override void SetDefaults()
        {
            Item.damage = 167;
            Item.useTime = Item.useAnimation = 29;
            Item.rare = ItemRarityID.Yellow;
            Item.width = 62;
            Item.height = 24;
            base.SetDefaults();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
                type = ProjectileID.BulletHighVelocity;
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}