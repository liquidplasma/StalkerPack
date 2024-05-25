using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;

namespace StalkerPack
{
    public class StalkerPackGlobalProjectile : GlobalProjectile
    {
        public bool fromDeagle;

        public override bool InstancePerEntity => true;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (fromDeagle)
            {
                if (projectile.extraUpdates <= 2)
                {
                    projectile.extraUpdates += 2;
                    projectile.netUpdate = true;
                    NetMessage.SendData(MessageID.SyncProjectile);
                }
            }
            base.OnSpawn(projectile, source);
        }
    }
}