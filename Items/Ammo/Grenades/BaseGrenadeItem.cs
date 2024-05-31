namespace StalkerPack.Items.Ammo.Grenades
{
    public class BaseGrenadeItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = Item.height = 14;
            Item.shootSpeed = 8.2f;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.ammo = Type;
            Item.knockBack = 5f;
            Item.rare = ItemRarityID.Lime;
            Item.DamageType = DamageClass.Ranged;
            base.SetDefaults();
        }
    }
}