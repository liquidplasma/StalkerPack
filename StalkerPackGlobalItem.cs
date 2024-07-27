using StalkerPack.Helpers;
using StalkerPack.Items.Other;
using StalkerPack.Items.Weapons.Pistols;
using StalkerPack.Items.Weapons.Rifles;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;

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

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            switch (item.type)
            {
                case ItemID.PlanteraBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Deagle>(), 7));
                    break;

                case ItemID.EyeOfCthulhuBossBag:
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SvarogItem>()));
                    break;
            }
        }
    }
}