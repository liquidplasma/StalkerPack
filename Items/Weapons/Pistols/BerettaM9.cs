namespace StalkerPack.Items.Weapons.Pistols
{
    public class BerettaM9 : BaseSemi
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(BerettaM9) + "/shot"); }

        public override void SetDefaults()
        {
            Item.damage = 29;
            Item.useTime = Item.useAnimation = 10;
            Item.autoReuse = false;
            Item.rare = ItemRarityID.Green;
            Item.width = 42;
            Item.height = 26;
            Item.scale = 0.8f;
            Item.value = ContentSamples.ItemsByType[ItemID.Handgun].value;
            base.SetDefaults();
        }

        public override Vector2? HoldoutOffset()
        {
            return new(2, 1);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ItemID.Handgun)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}