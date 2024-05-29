namespace StalkerPack.Projectiles.Warheads
{
    public class OG7VProj : BaseWarheadProjectile
    {
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < Main.rand.Next(4, 8); i++)
            {
                Vector2 velocity = Utils.NextVector2Circular(Main.rand, 16, 16);
                BetterNewProjectile(Player, Projectile.GetSource_Death(), Projectile.Center, velocity, ProjectileID.ClusterFragmentsI, (int)(Projectile.damage * 0.5f), Projectile.knockBack * 0.5f, Player.whoAmI);
            }
            base.OnKill(timeLeft);
        }
    }
}