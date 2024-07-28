using StalkerPack.Items.Weapons.Pistols;
using StalkerPack.Items.Weapons.Shotguns;
using Terraria.DataStructures;

namespace StalkerPack
{
    public class StalkerPackGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile entity)
        {
            base.SetDefaults(entity);
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse deagle && deagle.Item is Item deagleItem && deagleItem.type == ModContent.ItemType<Deagle>())
            {
                projectile.extraUpdates += 4;
                projectile.netUpdate = true;
                NetMessage.SendData(MessageID.SyncProjectile);
            }
            if (source is EntitySource_ItemUse toz34 && toz34.Item is Item toz34Item && toz34Item.type == ModContent.ItemType<TOZ34>())
            {
                projectile.extraUpdates += 3;
                projectile.netUpdate = true;
                NetMessage.SendData(MessageID.SyncProjectile);
            }
            base.OnSpawn(projectile, source);
        }
    }
}