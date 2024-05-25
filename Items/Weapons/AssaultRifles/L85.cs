namespace StalkerPack.Items.Weapons.AssaultRifles
{
    public class L85 : BaseAutomatic
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(L85) + "/shot", 2); }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 22;
            Spread = 2;
            Item.damage = 24;
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
            return new(-16, 2);
        }
    }
}