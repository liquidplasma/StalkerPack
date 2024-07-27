using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace StalkerPack.Projectiles
{
    internal class GaussProj : ModProjectile
    {
        private static Texture2D outLine = ModContent.Request<Texture2D>("StalkerPack/Effects/GaussLaserOutLine", AssetRequestMode.ImmediateLoad).Value;

        private static Texture2D inLine = ModContent.Request<Texture2D>("StalkerPack/Effects/GaussLaser", AssetRequestMode.ImmediateLoad).Value;

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;
        private ref float Timer => ref Projectile.ai[0];

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 100;
            ProjectileID.Sets.TrailingMode[Type] = 2;
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 20;
            Projectile.DamageType = DamageClass.Ranged;
            base.SetDefaults();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            foreach (Vector2 position in Projectile.oldPos)
            {
                BetterEntityDraw(outLine, position, outLine.Bounds, Color.White, 0f, outLine.Size() / 2, 1f, 0);
            }
            foreach (Vector2 position in Projectile.oldPos)
            {
                BetterEntityDraw(inLine, position, inLine.Bounds, Color.Yellow, 0f, inLine.Size() / 2, 1f, 0);
            }

            return false;
        }

        public override void AI()
        {
            base.AI();
        }
    }
}