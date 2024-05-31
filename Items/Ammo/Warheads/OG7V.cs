using StalkerPack.Projectiles.Warheads;

namespace StalkerPack.Items.Ammo.Warheads
{
    public class OG7V : BaseWarheadItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.damage = 44;
            Item.shoot = ModContent.ProjectileType<OG7VProj>();
            Item.value = (int)(ContentSamples.ItemsByType[ItemID.ClusterRocketI].value * 0.67f);
            base.SetDefaults();
        }
    }
}