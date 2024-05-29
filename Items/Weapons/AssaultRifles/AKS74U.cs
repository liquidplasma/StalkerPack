namespace StalkerPack.Items.Weapons.AssaultRifles
{
    public class AKS74U : BaseAutomatic
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(AKS74U) + "/shot"); }

        public override void SetDefaults()
        {
            Spread = 3;
            Item.damage = 7;
            Item.rare = ItemRarityID.Green;
            Item.useTime = Item.useAnimation = 6;
            Item.value = ContentSamples.ItemsByType[ItemID.Minishark].value;
            base.SetDefaults();
        }

        public override void HoldItem(Player player)
        {
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
            return new(-12, 3);
        }
    }
}