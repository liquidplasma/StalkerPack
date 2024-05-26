using StalkerPack.Items.Weapons.AssaultRifles;
using StalkerPack.Items.Weapons.Pistols;
using StalkerPack.Items.Weapons.Rifles;
using Terraria.GameContent.ItemDropRules;

namespace StalkerPack
{
    public class StalkerPackGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            switch (npc.type)
            {
                case NPCID.SkeletonSniper:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SVD>(), 12));
                    break;

                case NPCID.Plantera:
                    LeadingConditionRule deagleRule = new(new Conditions.IsExpert());
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
                    break;
            }
        }
    }
}