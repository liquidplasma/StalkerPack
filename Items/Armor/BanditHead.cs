namespace StalkerPack.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class BanditHead : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.value = ContentSamples.ItemsByType[ItemID.IronHelmet].value;
            Item.rare = ContentSamples.ItemsByType[ItemID.IronHelmet].rare;
            Item.defense = 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                 .AddIngredient(ItemID.Silk, 3)
                 .AddTile(TileID.WorkBenches)
                 .Register();
        }
    }
}