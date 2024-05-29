using StalkerPack.Items.Ammo;
using StalkerPack.Items.Consumables;
using StalkerPack.Items.Weapons.AssaultRifles;
using StalkerPack.Items.Weapons.Pistols;
using StalkerPack.Items.Weapons.Rifles;
using StalkerPack.Items.Weapons.Shotguns;
using Terraria.GameContent.ItemDropRules;

namespace StalkerPack
{
    public class StalkerPackGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (NPCID.Search.GetName(npc.type).Contains("zombie", System.StringComparison.CurrentCultureIgnoreCase))
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DietSausage>(), 10));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bread>(), 10));
            }

            switch (npc.type)
            {
                case NPCID.SkeletonSniper:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SVD>(), 12));
                    break;

                case NPCID.TacticalSkeleton:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Protecta>(), 12));
                    break;

                case NPCID.Plantera:
                    LeadingConditionRule deagleRule = new(new Conditions.NotExpert());
                    deagleRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Deagle>(), 7));
                    npcLoot.Add(deagleRule);
                    break;
            }
        }

        public override void ModifyShop(NPCShop shop)
        {
            switch (shop.NpcType)
            {
                case NPCID.ArmsDealer:
                    shop.Add(ModContent.ItemType<AKS74U>());
                    shop.Add(ModContent.ItemType<MP5>());
                    shop.Add(ModContent.ItemType<SPAS12>(), Condition.DownedSkeletron);
                    shop.Add(ModContent.ItemType<Winchester1300>(), Condition.Hardmode);
                    break;

                case NPCID.Merchant:
                    shop.Add(ModContent.ItemType<Vodka>());
                    shop.Add(ModContent.ItemType<TouristsBreakfast>());
                    shop.Add(ModContent.ItemType<EnergyDrink>(), Condition.DownedSkeletron);
                    break;

                case NPCID.Cyborg:
                    shop.Add(ModContent.ItemType<OG7V>(), Condition.DownedMartians);
                    shop.Add(ModContent.ItemType<PG7V>());
                    shop.Add(ModContent.ItemType<PG7VL>());
                    break;
            }
        }
    }
}