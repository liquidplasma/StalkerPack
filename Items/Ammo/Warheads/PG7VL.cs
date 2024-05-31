using StalkerPack.Projectiles.Warheads;

namespace StalkerPack.Items.Ammo.Warheads
{
    public class PG7VL : BaseWarheadItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 30;
            Item.damage = 40;
            Item.shoot = ModContent.ProjectileType<PG7VLProj>();
            Item.value = (int)(ContentSamples.ItemsByType[ItemID.RocketIII].value * 0.575f);
            base.SetDefaults();
        }
    }
}