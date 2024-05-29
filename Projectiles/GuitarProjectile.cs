using ReLogic.Utilities;
using StalkerPack.Buffs;
using StalkerPack.Helpers;
using StalkerPack.Items.Other;
using System.IO;
using Terraria.DataStructures;

namespace StalkerPack.Projectiles
{
    public class GuitarProjectile : ModProjectile
    {
        private Player Player => Main.player[Projectile.owner];

        private bool
            MouseRightPressed,
            MouseLeftPressed,
            PlayingMusic;

        private SoundStyle GuitarMusic = new("StalkerPack/Sounds/Guitar/guitar", 11)
        {
            MaxInstances = 0,
        };

        private ActiveSound MusicPlaying;

        private SlotId MusicID;

        private ref float Timer => ref Projectile.ai[0];
        private ref float ArmMovement => ref Projectile.ai[1];

        public override string Texture => "StalkerPack/Items/Other/Guitar";

        public override void SetDefaults()
        {
            Projectile.height = Projectile.width = 32;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 120;
            base.SetDefaults();
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return false;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool PreAI()
        {
            Projectile.KeepAliveIfOwnerIsAlive(Player);
            if (Player.HeldItem.type != ModContent.ItemType<Guitar>())
                Projectile.Kill();
            return base.PreAI();
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void AI()
        {
            Timer++;
            MusicPlaying = SoundEngine.FindActiveSound(GuitarMusic);
            if (Player.whoAmI == Main.myPlayer && !Player.mouseInterface && !Main.mapFullscreen)
            {
                MouseRightPressed = Main.mouseRight;
                MouseLeftPressed = Main.mouseLeft;
                Projectile.netUpdate = true;
            }

            var tracker = new ProjectileAudioTracker(Projectile);
            if (MouseLeftPressed && MusicPlaying == null)
                MusicID = SoundEngine.PlaySound(GuitarMusic, Player.position, MusicPlaying => Callback(tracker, MusicPlaying));

            if (MouseRightPressed)
                Projectile.Kill();

            Behavior();
            ArmBehavior();
            Projectile.velocity = Vector2.Zero;
            Vector2 pos = Player.MountedCenter + new Vector2(12 * Player.direction, 0);
            Projectile.Center = pos;
            Projectile.spriteDirection = Player.direction;
            Player.heldProj = Projectile.whoAmI;
            if (MusicPlaying == null)
            {
                PlayingMusic = false;
                Projectile.netUpdate = true;
                return;
            }
        }

        private void Behavior()
        {
            if (!SoundEngine.TryGetActiveSound(MusicID, out var _)) //not playing
                return;
            GuitarBuff();
            Notes();
            if (Player.whoAmI == Main.myPlayer)
            {
                PlayingMusic = true;
                Projectile.netUpdate = true;
            }
        }

        private bool Callback(ProjectileAudioTracker tracker, ActiveSound musicPlaying)
        {
            musicPlaying.Position = Player.position;
            return tracker.IsActiveAndInGame();
        }

        private void ArmBehavior()
        {
            if (PlayingMusic)
            {
                if (Timer % 60 == 0)
                    ArmMovement = 0.33f;
                else if (Timer % 30 == 0)
                    ArmMovement = 0f;
                Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (MathHelper.PiOver4 + ArmMovement) * -Player.direction);
                Player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.PiOver4 * -Player.direction);
                return;
            }
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.PiOver4 * -Player.direction);
            Player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, MathHelper.PiOver4 * -Player.direction);
        }

        private void GuitarBuff()
        {
            Player.AddBuff(ModContent.BuffType<GoodTunes>(), 30);
            Player.AddBuff(BuffID.Sunflower, 2);
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player otherPlayer = Main.player[i];
                if (otherPlayer.active && !otherPlayer.dead && otherPlayer.DistanceSQ(Player.Center) < 640 * 640 && otherPlayer.whoAmI != Player.whoAmI)
                {
                    otherPlayer.AddBuff(ModContent.BuffType<GoodTunes>(), 30);
                    otherPlayer.AddBuff(BuffID.Sunflower, 2);
                }
            }
        }

        private void Notes()
        {
            if (!Main.rand.NextBool(50))
                return;
            int MusicNote = Main.rand.Next(570, 573);
            Vector2 SpawnPosition = Projectile.Center;
            Vector2 NoteMovement = Utils.NextVector2Circular(Main.rand, 1, 1);
            Gore.NewGore(new EntitySource_Misc("StalkerGuitar"), SpawnPosition, NoteMovement, MusicNote, Utils.SelectRandom(Main.rand, 0.6f, 0.7f, 0.8f, 0.9f));
        }

        public override void OnKill(int timeLeft)
        {
            if (SoundEngine.TryGetActiveSound(MusicID, out var music))
                music.Stop();
            base.OnKill(timeLeft);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(MouseRightPressed);
            writer.Write(MouseLeftPressed);
            writer.Write(PlayingMusic);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            MouseRightPressed = reader.ReadBoolean();
            MouseLeftPressed = reader.ReadBoolean();
            PlayingMusic = reader.ReadBoolean();
        }
    }
}