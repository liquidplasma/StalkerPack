namespace StalkerPack.Buffs
{
    public class GoodTunes : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 6;
            player.lifeRegen += 10;
        }
    }
}