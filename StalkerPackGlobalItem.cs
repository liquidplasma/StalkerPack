using StalkerPack.Helpers;
using StalkerPack.Items.Weapons.Pistols;
using StalkerPack.Items.Weapons.Rifles;
using System.Collections;
using Terraria.DataStructures;

namespace StalkerPack
{
    public class StalkerPackGlobalItem : GlobalItem
    {
        public override void OnSpawn(Item item, IEntitySource source)
        {
            if (Main.rand.NextBool() && source is EntitySource_TileBreak)
            {
                switch (item.type)
                {
                    case ItemID.Musket:
                        item.ReplaceItem(ModContent.ItemType<VSS>());
                        break;

                    case ItemID.TheUndertaker:
                        item.ReplaceItem(ModContent.ItemType<Fora12>());
                        break;
                }
            }
            base.OnSpawn(item, source);
        }
    }
}