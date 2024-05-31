using StalkerPack.Projectiles.Grenades;

namespace StalkerPack.Items.Ammo.Grenades
{
    public class VOG25 : BaseGrenadeItem
    {
        public override void SetDefaults()
        {
            Item.damage = 28;
            Item.shoot = ModContent.ProjectileType<VOG25Proj>();
            Item.value = ContentSamples.ItemsByType[ItemID.RocketI].value;
            base.SetDefaults();
        }
    }
}