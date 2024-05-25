using StalkerPack.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;

namespace StalkerPack.Items.Weapons.Pistols
{
    public class Deagle : BaseSemi
    {
        public override SoundStyle ShootNoise { get => new("StalkerPack/Sounds/Weapons/" + nameof(Deagle) + "/shot"); }

        public override void SetDefaults()
        {
            Item.damage = 86;
            Item.useTime = Item.useAnimation = 20;
            Item.rare = ItemRarityID.Yellow;
            Item.width = 40;
            Item.height = 24;
            base.SetDefaults();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile shot = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            shot.StalkerGlobal().fromDeagle = true;
            return false;
        }
    }
}