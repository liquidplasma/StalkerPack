using Microsoft.Xna.Framework.Graphics;
using StalkerPack.Items.Other;

namespace StalkerPack.Projectiles.Svarog
{
    internal class LifeFruitFinder : ModProjectile
    {
        private Player Player => Main.player[Projectile.owner];
        private StalkerPackModplayer Svarog => Player.GetModPlayer<StalkerPackModplayer>();
        private bool Draw;

        public override void SetDefaults()
        {
            Projectile.height = 54;
            Projectile.width = 32;
            Projectile.tileCollide = false;
            base.SetDefaults();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Draw)
            {
                Texture2D myTexture = Projectile.MyTexture();
                Rectangle rect = myTexture.Frame(verticalFrames: Main.projFrames[Type], frameY: Projectile.frame);
                BetterEntityDraw(myTexture, Projectile.Center, rect, Color.White, Projectile.rotation, rect.Size() / 2, 0.75f, SpriteEffects.None, 0);
            }
            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return false;
        }

        public override bool PreAI()
        {
            if (Player.HasItem(ModContent.ItemType<SvarogItem>()))
                Projectile.KeepAliveIfOwnerIsAlive(Player);
            else
                Projectile.Kill();
            return base.PreAI();
        }

        public override void AI()
        {
            Projectile.Center = Player.MountedCenter;
            Projectile.alpha = 255;
            Draw = false;
            if (Svarog.FindNearestLifeFruit())
            {
                if (Svarog.NearestLifeFruit != Vector2.Zero && Projectile.Center.Distance(Svarog.NearestLifeFruit) >= 120f)
                {
                    Draw = true;
                    Projectile.alpha = 0;
                    Vector2 distance = Player.Center.DirectionTo(Svarog.NearestLifeFruit) * 60f;
                    Projectile.Center = Player.MountedCenter + distance;
                    Projectile.position.Y += Player.gfxOffY;
                    Projectile.rotation = Projectile.Center.AngleTo(Svarog.NearestLifeFruit) + MathHelper.PiOver2;
                }
            }
        }
    }
}