using StalkerPack.Projectiles.Warheads;

namespace StalkerPack.Items.Ammo.Warheads
{
    public class PG7V : BaseWarheadItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.damage = 48;
            Item.shoot = ModContent.ProjectileType<PG7VProj>();
            Item.value = (int)(ContentSamples.ItemsByType[ItemID.RocketIII].value * 1.05f);
            base.SetDefaults();
        }
    }
}