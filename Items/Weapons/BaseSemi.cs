namespace StalkerPack.Items.Weapons
{
    /// <summary>
    /// Semi auto guns
    /// </summary>
    public abstract class BaseSemi : ModItem
    {
        public virtual SoundStyle ShootNoise { get; }

        public override void SetDefaults()
        {
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.knockBack = 7f;
            Item.useAmmo = AmmoID.Bullet;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = ShootNoise with
            {
                Volume = 0.3f,
                PitchRange = (-0.1f, 0.1f),
                MaxInstances = 0
            };
            Item.autoReuse = false;
            base.SetDefaults();
        }
    }
}