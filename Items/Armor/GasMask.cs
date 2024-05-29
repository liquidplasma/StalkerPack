namespace StalkerPack.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GasMask : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = ContentSamples.ItemsByType[ItemID.IronHelmet].value;
            Item.rare = ItemRarityID.Orange;
            Item.defense = 6;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                 .AddIngredient(ItemID.Silk, 4)
                 .AddIngredient(ItemID.Glass, 2)
                 .AddTile(TileID.WorkBenches)
                 .Register();
        }
    }
}