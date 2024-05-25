﻿namespace StalkerPack.Items.Weapons.AssaultRifles
{
    public class SG550 : BaseAutomatic
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(SG550) + "/shot"); }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 20;
            Spread = 3;
            Item.damage = 23;
            Item.rare = ItemRarityID.Pink;
            Item.useTime = Item.useAnimation = 6;
            Item.channel = true;
            base.SetDefaults();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.NextBool(3))
                return false;
            return base.CanConsumeAmmo(ammo, player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new(-12, 2);
        }
    }
}