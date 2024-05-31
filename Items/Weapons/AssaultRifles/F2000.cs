namespace StalkerPack.Items.Weapons.AssaultRifles
{
    public class F2000 : BaseAutomatic
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(F2000) + "/shot"); }

        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 32;
            Spread = 2;
            Item.damage = 26;
            Item.rare = ContentSamples.ItemsByType[ItemID.Megashark].rare;
            Item.useTime = Item.useAnimation = 6;
            Item.value = ContentSamples.ItemsByType[ItemID.Megashark].value;

            base.SetDefaults();
        }

        public override void HoldItem(Player player)
        {
            player.scope = true;
            base.HoldItem(player);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.NextBool())
                return false;
            return base.CanConsumeAmmo(ammo, player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new(-24, -4);
        }

        public override void AddRecipes() => RegisterAsMegashark();
    }
}