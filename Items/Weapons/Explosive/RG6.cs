using StalkerPack.Items.Ammo.Grenades;
using StalkerPack.Items.Ammo.Warheads;

namespace StalkerPack.Items.Weapons.Explosive
{
    public class RG6 : ModItem
    {
        private SoundStyle ShootNoise => new("StalkerPack/Sounds/Weapons/" + nameof(RG6) + "/shot");

        public override void SetDefaults()
        {
            Item.damage = 39;
            Item.useTime = Item.useAnimation = 20;
            Item.rare = ContentSamples.ItemsByType[ItemID.GrenadeLauncher].rare;
            Item.width = 56;
            Item.height = 22;
            Item.value = ContentSamples.ItemsByType[ItemID.GrenadeLauncher].value;
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
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool? CanChooseAmmo(Item ammo, Player player)
        {
            if (ammo.ModItem is BaseGrenadeItem)
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