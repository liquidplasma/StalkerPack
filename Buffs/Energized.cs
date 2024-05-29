namespace StalkerPack.Buffs
{
    public class Energized : ModBuff
    {
        public override void Update(Player player, ref int buffIndex) => player.GetModPlayer<StalkerPackModplayer>().EnergyDrinkIsActive = true;
    }
}