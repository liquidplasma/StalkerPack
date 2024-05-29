namespace StalkerPack.Projectiles.Warheads
{
    public class PG7VLProj : BaseWarheadProjectile
    {
        public override void SetDefaults()
        {
            Projectile.ArmorPenetration = 13;
            base.SetDefaults();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.defense >= 40)
                modifiers.SourceDamage += 0.25f;
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}