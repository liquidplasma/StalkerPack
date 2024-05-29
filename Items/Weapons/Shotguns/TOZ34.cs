using Terraria.DataStructures;
using Terraria.Localization;

namespace StalkerPack.Items.Weapons.Shotguns
{
    public class TOZ34 : BaseSemi
    {
        private enum Modes
        {
            Buckshot,

            Slug,
        }

        private int
            mode,
            switchDelay;

        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(TOZ34) + "/shot"); }

        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.useTime = Item.useAnimation = 27;
            Item.rare = ItemRarityID.Blue;
            Item.width = 56;
            Item.height = 14;
            Item.autoReuse = false;
            Item.value = ContentSamples.ItemsByType[ItemID.Boomstick].value;
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
                    newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(pellets));
                BetterNewProjectile(player, source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new(-10, 0);
        }
    }
}