using StalkerPack.Items.Weapons.Pistols;
using Terraria;
using Terraria.DataStructures;

namespace StalkerPack
{
    public class StalkerPackGlobalProjectile : GlobalProjectile
    {
        public bool fromDeagle;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile entity)
        {
            base.SetDefaults(entity);
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse gun && gun.Item is Item item && item.type == ModContent.ItemType<Deagle>())
            {
                projectile.extraUpdates += 4;
                projectile.netUpdate = true;
                NetMessage.SendData(MessageID.SyncProjectile);
            }
            base.OnSpawn(projectile, source);
        }
    }
}