namespace StalkerPack.Items.Weapons.Rifles
{
    public class VSS : BaseSemi
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(VSS) + "/shot"); }

        public override void SetDefaults()
        {
            Item.damage = 27;
            Item.useTime = Item.useAnimation = 27;
            Item.rare = ItemRarityID.Yellow;
            Item.width = 56;
            Item.height = 18;
            base.SetDefaults();
        }
    }
}