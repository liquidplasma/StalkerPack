using ReLogic.Utilities;
using StalkerPack.Buffs;
using StalkerPack.Helpers;
using StalkerPack.Items.Other;
using System.IO;
using Terraria;
using Terraria.DataStructures;

namespace StalkerPack.Projectiles
{
    public class GuitarProjectile : ModProjectile
    {
        private Player Player => Main.player[Projectile.owner];
        private Vector2 MouseAim;

        private float armMovement = 0f;

        private bool
            MouseRightPressed,
            MouseLeftPressed;

        private SoundStyle GuitarMusic = new("StalkerPack/Sounds/Guitar/guitar", 11);

        private ActiveSound MusicPlaying;

        private SlotId MusicID;

        private ref float Timer => ref Projectile.ai[0];
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
            if (MouseLeftPressed && !SoundEngine.TryGetActiveSound(MusicID, out MusicPlaying))
                MusicID = SoundEngine.PlaySound(GuitarMusic, Player.position);

            if (MusicPlaying != null)
            {
                Notes();
                MusicPlaying.Position = Player.position;
                if (MouseRightPressed)
                    MusicPlaying.Stop();
            }
            Projectile.KeepAliveIfOwnerIsAlive(Player);
            return base.PreAI();
        }

        public override void AI()
        {
            Timer++;
            if (Player.whoAmI == Main.myPlayer)
            {
                MouseAim = Main.MouseWorld;
                if (!Player.mouseInterface && !Main.mapFullscreen)
                {
                    MouseRightPressed = Main.mouseRight;
                    MouseLeftPressed = Main.mouseLeft;
                }
                Projectile.netUpdate = true;
            }

            Vector2 pos = Player.MountedCenter + new Vector2(12 * Player.direction, 0);
            Projectile.Center = pos;
            Projectile.spriteDirection = Player.direction;
            Player.heldProj = Projectile.whoAmI;

            if (Player.HeldItem.type != ModContent.ItemType<Guitar>())
            {
                MusicPlaying?.Stop();
                Projectile.Kill();
            }
            if (MusicPlaying != null && MusicPlaying.IsPlaying)
            {
                Player.AddBuff(ModContent.BuffType<GoodTunes>(), 30);
                Player.AddBuff(BuffID.Sunflower, 2);
                GuitarBuff();
                if (Timer % 60 == 0)
                    armMovement = 0.33f;
                else if (Timer % 30 == 0)
                    armMovement = 0f;
                Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (MathHelper.PiOver4 + armMovement) * -Player.direction);
            }
            else
            {
                Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, MathHelper.PiOver4 * -Player.direction);
            }
            base.AI();
        }

        private void Notes()
        {
            if (!Main.rand.NextBool(60) || !MusicPlaying.IsPlaying)
                return;
            int MusicNote = Main.rand.Next(570, 573);
            Vector2 SpawnPosition = Projectile.Center;
            Vector2 NoteMovement = Utils.NextVector2Circular(Main.rand, 1, 1);
            Gore.NewGore(new EntitySource_Misc("StalkerGuitar"), SpawnPosition, NoteMovement, MusicNote, Utils.SelectRandom(Main.rand, 0.6f, 0.7f, 0.8f, 0.9f));
        }

        private void GuitarBuff()
        {
            if (MusicPlaying != null && MusicPlaying.IsPlaying)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player otherPlayer = Main.player[i];
                    if (!otherPlayer.active)
                        continue;
                    if(otherPlayer.DistanceSQ(Player.Center) < 40 * 40 && otherPlayer.whoAmI != Player.whoAmI)
                    {
                        otherPlayer.AddBuff(ModContent.BuffType<GoodTunes>(), 30);
                        otherPlayer.AddBuff(BuffID.Sunflower, 2);
                    }                    
                }                
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(MouseRightPressed);
            writer.Write(MouseLeftPressed);
            writer.WriteVector2(MouseAim);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            MouseRightPressed = reader.ReadBoolean();
            MouseLeftPressed = reader.ReadBoolean();
            MouseAim = reader.ReadVector2();
        }
    }
}