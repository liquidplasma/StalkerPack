namespace StalkerPack.Items.Other
{
    public class Oasis : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = Item.height = 34;
            Item.accessory = true;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Purple;
        }

        public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 20;
            player.statLifeMax2 += 50;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            Lighting.AddLight(Item.position, Color.ForestGreen.ToVector3() * 1.5f);
            if (Main.rand.NextBool(60))
            {
                for (int i = 0; i < 20; i++)
                {
                    int dustType = Utils.SelectRandom(Main.rand, DustID.GreenTorch, DustID.GreenFairy);
                    Vector2 velocity = Utils.NextVector2Circular(Main.rand, 8, 8);
                    Dust dusty = Dust.NewDustDirect(Item.position, Item.width, Item.height, dustType);
                    dusty.velocity = velocity;
                    dusty.noGravity = true;
                }
            }
        }
    }
}