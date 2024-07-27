using Microsoft.Xna.Framework.Graphics;
using StalkerPack.Items.Weapons;
using System.IO;

namespace StalkerPack.Projectiles
{
    internal class GaussGun : ModProjectile
    {
        public Item GaussItem { get; set; }
        private Player Player => Main.player[Projectile.owner];
        public override string Texture => "StalkerPack/Items/Weapons/GaussRifle";
        public SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(GaussRifle) + "/shot"); }

        private ref float ShotTimer => ref Projectile.ai[0];
        private Vector2 mouseAim, shotAim, aim;

        private bool Draw, CanShoot;

        private int mouseDirection;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            base.SetDefaults();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Draw)
            {
                Texture2D myTexture = Projectile.MyTexture();
                Rectangle rect = myTexture.Frame(verticalFrames: Main.projFrames[Type], frameY: Projectile.frame);
                BetterEntityDraw(myTexture, Projectile.Center, rect, lightColor, Projectile.rotation, rect.Size() / 2, 1f, (Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipVertically), 0); ;
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
            if (ShotTimer > 0)
            {
                Player.SetDummyItemTime(2);
                Draw = true;
                ShotTimer--;
            }
            else
                Draw = false;
            Projectile.KeepAliveIfOwnerIsAlive(Player);
            if (Player.HeldItem.type != ModContent.ItemType<GaussRifle>())
                Projectile.Kill();

            CanShoot = false;
            if (Player.whoAmI == Main.myPlayer)
            {
                mouseAim = Main.MouseWorld;
                Projectile.netUpdate = true;
            }
            Player.heldProj = Projectile.whoAmI;
            Projectile.Center = Player.MountedCenter + shotAim;

            if (Player.channel && ShotTimer == 0 && Player.HasAmmo(GaussItem))
            {
                ShotTimer = GaussItem.useTime;
                aim = Projectile.Center.DirectionTo(mouseAim) * 10f;
                shotAim = Player.MountedCenter.DirectionTo(mouseAim) * 18f;
                mouseDirection = Player.DirectionTo(mouseAim).X > 0f ? 1 : -1;
                Projectile.spriteDirection = mouseDirection;
                Player.ChangeDir(mouseDirection);
                Projectile.rotation = Projectile.Center.AngleTo(mouseAim) + MathHelper.PiOver4 * Player.direction;
                CanShoot = true;
            }
            return base.PreAI();
        }

        public override void AI()
        {
            if (CanShoot)
            {
                Item Ammo = Player.ChooseAmmo(GaussItem);
                Player.ConsumeItem(Ammo.type);
                shotAim = Player.MountedCenter.DirectionTo(mouseAim) * 18f;
                SoundEngine.PlaySound(ShootNoise with
                {
                    Volume = 0.3f,
                    PitchRange = (-0.1f, 0.1f),
                    MaxInstances = 0
                }, Player.Center);
                if (Player.whoAmI == Main.myPlayer)
                    Projectile.NewProjectile(Player.GetSource_ItemUse(GaussItem), Player.Center + Player.Center.DirectionTo(mouseAim) * 52f, aim, ModContent.ProjectileType<GaussProj>(), GaussItem.damage + Ammo.damage, GaussItem.knockBack * 4f, Player.whoAmI);
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.WriteVector2(mouseAim);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            mouseAim = reader.ReadVector2();
        }
    }
}