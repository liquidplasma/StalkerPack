namespace StalkerPack.Items.Weapons.Pistols
{
    public class APS : BaseAutomatic
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(APS) + "/shot"); }

        public override void SetDefaults()
        {
            Spread = 4;
            Item.damage = 20;
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
            return new(-12, 3);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 5)
                .AddIngredient(ItemID.IllegalGunParts)
                .AddIngredient(ItemID.SoulofFright, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}