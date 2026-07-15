using ReLogic.Utilities;
using StalkerPack.Buffs;
using StalkerPack.Items.Other;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StalkerPack.Projectiles
{
    public class HarmonicaProjectile : ModProjectile
    {
        private Player Player => Main.player[Projectile.owner];

        private SoundStyle HarmonicaMusic = new("StalkerPack/Sounds/Harmonica/harmonica", 5)
        {
            MaxInstances = 0
        };

        private SlotId MusicID;
        private bool PlayingMusic;

        private ref float Timer => ref Projectile.ai[0];
        private ref float ArmMovement => ref Projectile.ai[1];

        public override string Texture => "StalkerPack/Items/Other/Harmonica";

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 120;
        }

        public override bool? CanDamage() => false;
        public override bool ShouldUpdatePosition() => false;

        public override bool PreAI()
        {
            Projectile.KeepAliveIfOwnerIsAlive(Player);

            if (Player.HeldItem.type != ModContent.ItemType<Harmonica>())
                Projectile.Kill();

            return true;
        }

        public override void AI()
        {
            Timer++;

            // ===== INPUT (ТОЛЬКО ВЛАДЕЛЕЦ) =====
            if (Player.whoAmI == Main.myPlayer && !Player.mouseInterface && !Main.mapFullscreen)
            {
                // ЛКМ — начать игру
                if (Main.mouseLeft && !PlayingMusic)
                {
                    MusicID = SoundEngine.PlaySound(
                        HarmonicaMusic,
                        Player.Center,
                        sound =>
                        {
                            sound.Position = Player.Center;
                            return Projectile.active;
                        }
                    );

                    PlayingMusic = true;
                    Projectile.netUpdate = true;
                }

                // ПКМ — убрать гармошку
                if (Main.mouseRight)
                    Projectile.Kill();
            }

            // ===== ОБНОВЛЕНИЕ ПОЗИЦИИ ЗВУКА =====
            if (SoundEngine.TryGetActiveSound(MusicID, out var sound))
                sound.Position = Player.Center;

            // ===== ПОВЕДЕНИЕ =====
            if (PlayingMusic)
            {
                HarmonicaBuff();
                Notes();
            }

            ArmBehavior();

            // ===== ПРИВЯЗКА К ИГРОКУ =====
            Projectile.velocity = Vector2.Zero;
            Projectile.Center = Player.MountedCenter + new Vector2(10 * Player.direction, -2);
            Projectile.spriteDirection = Player.direction;
            Player.heldProj = Projectile.whoAmI;
        }

        private void ArmBehavior()
        {
            // Гармошка — ближе ко рту, меньше движений
            Player.SetCompositeArmFront(
                true,
                Player.CompositeArmStretchAmount.Full,
                MathHelper.PiOver4 * -Player.direction
            );

            Player.SetCompositeArmBack(
                true,
                Player.CompositeArmStretchAmount.Full,
                MathHelper.PiOver4 * -Player.direction
            );
        }

        private void HarmonicaBuff()
        {
            Player.AddBuff(ModContent.BuffType<GoodTunes>(), 30);
            Player.AddBuff(BuffID.Sunflower, 2);

            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player other = Main.player[i];
                if (other.active && !other.dead && other.whoAmI != Player.whoAmI &&
                    Projectile.WithinRange(other.Center, 480 * 480))
                {
                    other.AddBuff(ModContent.BuffType<GoodTunes>(), 30);
                    other.AddBuff(BuffID.Sunflower, 2);
                }
            }
        }

        private void Notes()
        {
            if (!Main.rand.NextBool(60))
                return;

            int note = Main.rand.Next(570, 573);
            Gore.NewGore(
                new EntitySource_Misc("StalkerHarmonica"),
                Projectile.Center,
                Main.rand.NextVector2Circular(0.5f, 0.5f),
                note,
                Main.rand.NextFloat(0.5f, 0.8f)
            );
        }

        public override void OnKill(int timeLeft)
        {
            if (SoundEngine.TryGetActiveSound(MusicID, out var sound))
                sound.Stop();
        }
    }
}
