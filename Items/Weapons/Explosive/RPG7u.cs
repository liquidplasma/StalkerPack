using StalkerPack.Items.Ammo;

namespace StalkerPack.Items.Weapons.Explosive
{
    public class RPG7u : ModItem
    {
        private SoundStyle ShootNoise => new("StalkerPack/Sounds/Weapons/" + nameof(RPG7u) + "/shot");

        public override void SetDefaults()
        {
            Item.damage = 67;
            Item.crit = 11;
            Item.useTime = Item.useAnimation = 60;
            Item.rare = ItemRarityID.Yellow;
            Item.width = 52;
            Item.height = 22;
            Item.value = ContentSamples.ItemsByType[ItemID.RocketLauncher].value;
            Item.useAmmo = ModContent.ItemType<BaseWarheadItem>();
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.knockBack = 4f;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = ShootNoise with
            {
                Volume = 1f,
                PitchRange = (-0.1f, 0.1f),
                MaxInstances = 0
            };
        }

        public override bool? CanChooseAmmo(Item ammo, Player player)
        {
            if (ammo.ModItem is BaseWarheadItem)
            {
                Item.shoot = ammo.shoot;
                return true;
            }
            return base.CanChooseAmmo(ammo, player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new(-22, -1);
        }
    }
}