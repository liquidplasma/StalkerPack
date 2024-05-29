namespace StalkerPack.Items.Weapons.AssaultRifles
{
    public class MP5 : BaseAutomatic
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(MP5) + "/shot"); }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 20;
            Spread = 3;
            Item.damage = 6;
            Item.rare = ItemRarityID.Green;
            Item.useTime = Item.useAnimation = 5;
            Item.value = ContentSamples.ItemsByType[ItemID.Minishark].value;
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
    }
}