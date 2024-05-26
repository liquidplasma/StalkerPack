namespace StalkerPack.Buffs
{
    public class EnergizedModplayer : ModPlayer
    {
        public bool isActive;

        public override void ResetEffects()
        {
            isActive = false;
        }

        public override void PostUpdateRunSpeeds()
        {
            if (isActive)
            {
                Player.maxRunSpeed += 0.25f;
                Player.accRunSpeed += 0.25f;
                Player.runAcceleration += 0.25f;
            }
        }
    }

    public class Energized : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<EnergizedModplayer>().isActive = true;
        }
    }
}