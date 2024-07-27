namespace StalkerPack.Items.Weapons.Pistols
{
    public class Fora12 : BaseSemi
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(Fora12) + "/shot"); }

        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.useTime = Item.useAnimation = 16;
            Item.rare = ItemRarityID.Blue;
            Item.width = 40;
            Item.height = 24;
            Item.value = ContentSamples.ItemsByType[ItemID.TheUndertaker].value;
            base.SetDefaults();
        }

        public override Vector2? HoldoutOffset()
        {
            return new(-4, 0);
        }
    }
}