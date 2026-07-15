using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StalkerPack.Projectiles
{
    internal class GaussProj : ModProjectile
    {
        private static Texture2D Core;
        private static Texture2D Glow;

        public override void Load()
        {
            Core = ModContent.Request<Texture2D>(
                "StalkerPack/Effects/GaussLaser",
                AssetRequestMode.ImmediateLoad).Value;

            Glow = ModContent.Request<Texture2D>(
                "StalkerPack/Effects/GaussLaserOutLine",
                AssetRequestMode.ImmediateLoad).Value;
        }

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Bullet;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 120;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 10;

            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.penetrate = -1;
            Projectile.tileCollide = false;

            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 35;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;

            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            // === ИСКАЖЕНИЕ ВОЗДУХА (ВОЛНЫ В СТОРОНЫ) ===
            if (Main.rand.NextBool(3))
            {
                Vector2 dir = Projectile.velocity.SafeNormalize(Vector2.UnitX);
                Vector2 normal = dir.RotatedBy(MathHelper.PiOver2);

                for (int i = -1; i <= 1; i += 2)
                {
                    Dust d = Dust.NewDustPerfect(
                        Projectile.Center + normal * i * 8f,
                        DustID.Electric,
                        normal * i * Main.rand.NextFloat(1.5f, 3f),
                        160,
                        new Color(120, 200, 255),
                        1.4f
                    );

                    d.noGravity = true;
                }
            }

            // === ЭЛЕКТРИЧЕСКИЕ РАЗРЯДЫ ВДОЛЬ ТРАЕКТОРИИ ===
            if (Main.rand.NextBool(4))
            {
                Vector2 offset = Main.rand.NextVector2Circular(6f, 6f);
                Dust d = Dust.NewDustPerfect(
                    Projectile.Center + offset,
                    DustID.Electric,
                    Projectile.velocity * 0.1f,
                    100,
                    Color.White,
                    1.1f
                );
                d.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch sb = Main.spriteBatch;

            Vector2 glowOrigin = Glow.Size() / 2f;
            Vector2 coreOrigin = Core.Size() / 2f;

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                Vector2 pos = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition;
                float progress = 1f - i / (float)Projectile.oldPos.Length;

                float glowScale = MathHelper.Lerp(2.2f, 0.6f, i / 40f);
                float coreScale = MathHelper.Lerp(1.4f, 0.5f, i / 60f);

                // === ВНЕШНЕЕ СИНЕЕ СВЕЧЕНИЕ ===
                sb.Draw(
                    Glow,
                    pos,
                    null,
                    new Color(60, 140, 255, 120) * progress,
                    Projectile.rotation,
                    glowOrigin,
                    glowScale * progress,
                    SpriteEffects.None,
                    0f
                );

                // === ЯДРО ГАУСС-ЛУЧА ===
                sb.Draw(
                    Core,
                    pos,
                    null,
                    new Color(220, 245, 255, 220),
                    Projectile.rotation,
                    coreOrigin,
                    coreScale,
                    SpriteEffects.None,
                    0f
                );

                // === ХАОТИЧНЫЕ ЭЛЕКТРО-ДУГИ ===
                if (Main.rand.NextBool(2))
                {
                    Vector2 jitter = Main.rand.NextVector2Circular(5f, 5f);

                    sb.Draw(
                        Core,
                        pos + jitter,
                        null,
                        Color.White * progress,
                        Projectile.rotation,
                        coreOrigin,
                        coreScale * 0.6f,
                        SpriteEffects.None,
                        0f
                    );
                }
            }

            return false;
        }
    }
}

