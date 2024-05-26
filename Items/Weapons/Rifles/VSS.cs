
namespace StalkerPack.Items.Weapons.Rifles
{
    public class VSS : BaseSemi
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(VSS) + "/shot"); }

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.crit = 11;
            Item.useTime = Item.useAnimation = 15;
            Item.rare = ItemRarityID.Blue;
            Item.width = 56;
            Item.height = 18;
            base.SetDefaults();
        }
        public override Vector2? HoldoutOffset()
        {
            return new(-10, -1);
        }
    }
}