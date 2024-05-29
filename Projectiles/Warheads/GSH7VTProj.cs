namespace StalkerPack.Projectiles.Warheads
{
    public class GSH7VTProj : BaseWarheadProjectile
    {
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            bool noTrace = !Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1);
            if (noTrace)
                modifiers.SourceDamage += 0.5f;
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}