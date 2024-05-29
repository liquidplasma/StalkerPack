namespace StalkerPack.Projectiles.Warheads
{
    public class PG7VProj : BaseWarheadProjectile
    {
        public override void SetDefaults()
        {
            Projectile.ArmorPenetration = 25;
            base.SetDefaults();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.defense >= 40)
                modifiers.SourceDamage += 0.4f;
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}