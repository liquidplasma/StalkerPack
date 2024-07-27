namespace StalkerPack.Items.Ammo.Warheads
{
    public class BaseWarheadItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.shootSpeed = 9.5f;
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