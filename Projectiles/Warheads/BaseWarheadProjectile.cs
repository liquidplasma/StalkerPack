using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using StalkerPack.Helpers;
using Terraria.DataStructures;

namespace StalkerPack.Projectiles.Warheads
{
    public abstract class BaseWarheadProjectile : ModProjectile
    {
        public Player Player => Main.player[Projectile.owner];
        private Geometry Geometry = new();

        private SoundStyle Explosion => new("StalkerPack/Sounds/Warheads/explosion")
        {
            Volume = 0.15f,
            MaxInstances = 0
        };

        private SoundStyle Fly => new("StalkerPack/Sounds/Warheads/rocket_fly")
        {
            Volume = 0.2f,
            MaxInstances = 0,
        };

        private SlotId RocketFly;

        private ActiveSound RocketLoop;

        public int Timer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public int State
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public enum Exploded
        {
            Not,

            /// <summary>
            /// Projectile timeleft is less than 6
            /// </summary>
            Exploding
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.IsARocketThatDealsDoubleDamageToPrimaryEnemy[Type] = true;
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.width = Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 3600;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.timeLeft <= 3)
                return false;
            Texture2D texture = Projectile.MyTexture();
            Rectangle rect = texture.Bounds;
            BetterEntityDraw(texture, Projectile.Center, rect, lightColor, Projectile.rotation, texture.Size() / 2, Projectile.scale, SpriteEffects.None);
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (State != (int)Exploded.Exploding)
                Projectile.timeLeft = 3;
            else
            {
                if (!target.active)
                {
                    for (int i = 0; i < 72; i++)
                    {
                        Vector2 dustPos = target.position + (target.Hitbox.Size() * Main.rand.NextFloat());
                        Dust effect = Dust.NewDustDirect(target.position, target.width, target.height, SmokeyDust);
                        effect.velocity =
                            Main.rand.NextBool()
                            ? dustPos.DirectionFrom(target.Center) * 8f //true
                            : Utils.RandomVector2(Main.rand, -8f, 8f); //false

                        effect.scale = 3f * Main.rand.NextFloat();
                        effect.noGravity = Main.rand.NextBool();
                    }
                }
            }
        }

        public override void AI()
        {
            RocketLoop = SoundEngine.FindActiveSound(Fly);
            if (!SoundEngine.TryGetActiveSound(RocketFly, out var _))
            {
                var tracker = new ProjectileAudioTracker(Projectile);
                RocketFly = SoundEngine.PlaySound(Fly, Projectile.position, soundInstance => SoundCallBack(tracker, soundInstance));
            }

            Projectile.FaceForward();
            Timer++;
            if (State != (int)Exploded.Exploding)
            {
                for (int i = 0; i < 6; i++)
                {
                    Vector2 dustVel = -Projectile.velocity.RotatedBy(Geometry.IncreaseDecrease(0.1f, 45, 0)) * 0.1f;
                    dustVel = dustVel.RotatedByRandom(MathHelper.ToRadians(2));
                    Dust dusty = Dust.NewDustDirect(Projectile.Center, 1, 1, SmokeyDust);
                    dusty.velocity = dustVel;
                    dusty.noGravity = true;
                }
            }

            if (Timer >= 600)
                Projectile.velocity.Y += 0.5f;
            Projectile.velocity.Y += 0.033f;
            if (Projectile.velocity.Y >= 16f)
                Projectile.velocity.Y = Projectile.oldVelocity.Y;

            if (Projectile.timeLeft < 3)
            {
                State = (int)Exploded.Exploding;
                Projectile.Resize(400, 400);
                Projectile.alpha = 255;
                Projectile.velocity = Vector2.Zero;
                Projectile.tileCollide = false;
                Projectile.netUpdate = true;
                SmokeGore(Projectile.GetSource_Death(), Projectile.Center, 9, 4);
            }
        }

        private bool SoundCallBack(ProjectileAudioTracker tracker, ActiveSound soundInstance)
        {
            soundInstance.Position = Projectile.position;
            return tracker.IsActiveAndInGame();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft = 3;
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            RocketLoop?.Stop();
            SoundEngine.PlaySound(Explosion with
            {
                Pitch = Main.rand.NextFloat(-0.15f, 0.15f),
                Volume = 0.15f,
                MaxInstances = 0
            }, Projectile.Center);

            if (Player.DistanceSQ(Projectile.Center) <= 200 * 200 && Collision.CanHitLine(Projectile.Center, 1, 1, Player.Center, 1, 1))
            {
                Player.HurtInfo explosionSelfDamage = new()
                {
                    Dodgeable = true,
                    HitDirection = Projectile.Center.DirectionTo(Player.Center).X > 0f ? 1 : -1,
                    Damage = (int)(Projectile.damage * 0.9f),
                    DamageSource = PlayerDeathReason.ByProjectile(Player.whoAmI, Projectile.identity),
                    Knockback = 6f
                };
                Player.Hurt(explosionSelfDamage);
            }
            base.OnKill(timeLeft);
        }
    }
}