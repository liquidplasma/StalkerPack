using StalkerPack.Projectiles.Grenades;

namespace StalkerPack.Items.Ammo.Grenades
{
    public class VOG25P : BaseGrenadeItem
    {
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.shoot = ModContent.ProjectileType<VOG25PProj>();
            Item.value = (int)(ContentSamples.ItemsByType[ItemID.RocketI].value * 1.2f);
            base.SetDefaults();
        }
    }
}