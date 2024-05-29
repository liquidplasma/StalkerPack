namespace StalkerPack.Items.Weapons.Pistols
{
    public class PMM : BaseSemi
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(PMM) + "/shot"); }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.useTime = Item.useAnimation = 15;
            Item.autoReuse = false;
            Item.rare = ItemRarityID.Green;
            Item.width = 30;
            Item.height = 22;
            Item.value = ContentSamples.ItemsByType[ItemID.FlintlockPistol].value;
            base.SetDefaults();
        }

        public override Vector2? HoldoutOffset()
        {
            return new(0, 2);
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