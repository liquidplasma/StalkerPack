namespace StalkerPack.Items.Weapons.AssaultRifles
{
    public class AK74 : BaseAutomatic
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(AK74) + "/shoot", 5); }

        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 22;
            Spread = 4;
            Item.damage = 25;
            Item.rare = ItemRarityID.Pink;
            Item.useTime = Item.useAnimation = 6;
            base.SetDefaults();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.NextBool())
                return false;
            return base.CanConsumeAmmo(ammo, player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new(-18, 2);
        }

        public override void AddRecipes() => RegisterAsMegashark();
    }
}