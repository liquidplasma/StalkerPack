using StalkerPack.Helpers;

namespace StalkerPack.Items.Weapons.Pistols
{
    public class Deagle : BaseSemi
    {
        private Geometry Geometry = new();

        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(Deagle) + "/shot"); }

        public override void SetDefaults()
        {
            Item.damage = 86;
            Item.useTime = Item.useAnimation = 24;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Lime;
            Item.width = 40;
            Item.height = 24;
            Item.scale = 0.8f;
            Item.value = ContentSamples.ItemsByType[ItemID.VenusMagnum].value;
            base.SetDefaults();
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
           
            base.UseStyle(player, heldItemFrame);
        }

        public override Vector2? HoldoutOffset()
        {
            return new(-4, 4);
        }
    }
}