using Terraria.DataStructures;
using Terraria.Localization;

namespace StalkerPack.Items.Weapons.Shotguns
{
    public class SPAS12 : BaseSemi
    {
        private enum Modes
        {
            Buckshot,

            Slug,
        }

        private int
            mode,
            switchDelay;

        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(SPAS12) + "/shot", 2); }

        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.useTime = Item.useAnimation = 26;
            Item.rare = ItemRarityID.Orange;
            Item.width = 44;
            Item.height = 18;
            Item.autoReuse = true;
            Item.value = (int)(ContentSamples.ItemsByType[ItemID.Shotgun].value * 0.7f);
            base.SetDefaults();
        }

        public override void HoldItem(Player player)
        {
            if (switchDelay > 0)
                switchDelay--;
            if (player.whoAmI == Main.myPlayer && Main.mouseRight && switchDelay == 0 && !player.mouseInterface && !Main.mapFullscreen)
            {
                SoundEngine.PlaySound(SoundID.Item4, player.Center);
                mode++;
                if (mode > 1)
                    mode = 0;
                switch (mode)
                {
                    case 0:
                        CreateCombatText(player, Color.Green, Language.GetTextValue("Mods.StalkerPack.TOZ34.Modes.Buckshot"));
                        mode = (int)Modes.Buckshot;
                        break;

                    case 1:
                        CreateCombatText(player, Color.Green, Language.GetTextValue("Mods.StalkerPack.TOZ34.Modes.Slug"));
                        mode = (int)Modes.Slug;
                        break;
                }
                switchDelay = 75;
                return;
            }
            base.HoldItem(player);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            switch (mode)
            {
                case 1:
                    damage *= 3;
                    break;
            }
            base.ModifyWeaponDamage(player, ref damage);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 newVelocity = velocity;
            var pellets = mode switch
            {
                0 => 6,
                _ => 1,
            };
            for (int i = 0; i < pellets; i++)
            {
                if (mode == 0)
                    newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(pellets + 2));
                BetterNewProjectile(player, source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new(0, 0);
        }
    }
}