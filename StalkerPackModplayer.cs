using StalkerPack.Items.Other;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StalkerPack
{
    public class StalkerPackModplayer : ModPlayer
    {
        public bool EnergyDrinkIsActive;

        public Vector2
            NearestChest,
            NearestHeart,
            NearestLifeFruit;

        public int SearchTimer;

        public override void ResetEffects()
        {
            EnergyDrinkIsActive = false;
        }

        public override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems)
        {
            Item heartOfTheOasis = new();
            heartOfTheOasis.SetDefaults(ModContent.ItemType<Oasis>());
            rewardItems.Add(heartOfTheOasis);
        }

        public override void PostUpdateRunSpeeds()
        {
            if (EnergyDrinkIsActive)
            {
                Player.maxRunSpeed += 0.25f;
                Player.accRunSpeed += 0.25f;
                Player.runAcceleration += 0.25f;
            }
        }

        public override void PostUpdate()
        {
            SearchTimer++;
            base.PostUpdate();
        }

        public bool FindNearestChest()
        {
            NearestChest = Vector2.Zero;
            float closestDistance = 1280f;
            bool found = false;

            for (int i = 0; i < Main.chest.Length; i++)
            {
                Chest chest = Main.chest[i];
                if (chest != null)
                {
                    Vector2 chestPosition = new(chest.x * 16, chest.y * 16);
                    float distance = Player.Center.Distance(chestPosition);
                    if (distance <= 1280 && distance < closestDistance)
                    {
                        NearestChest = chestPosition;
                        closestDistance = distance;
                        found = true;
                    }
                }
            }
            return found;
        }

        public Vector2 FindClosestTileInBox(Vector2 center, float boxWidth, float boxHeight, float closestDistance, params int[] tileTypes)
        {
            float halfWidth = boxWidth / 2;
            float halfHeight = boxHeight / 2;

            // Calculate the boundaries of the box
            float left = center.X - halfWidth;
            float right = center.X + halfWidth;
            float top = center.Y - halfHeight;
            float bottom = center.Y + halfHeight;

            // Convert boundaries from world coordinates to tile coordinates and clamp them to the tile map bounds
            int leftTile = Math.Max(0, (int)(left / 16));
            int rightTile = Math.Min(Main.maxTilesX - 1, (int)(right / 16));
            int topTile = Math.Max(0, (int)(top / 16));
            int bottomTile = Math.Min(Main.maxTilesY - 1, (int)(bottom / 16));

            Vector2 closestTilePos = Vector2.Zero;

            for (int x = leftTile; x <= rightTile; x++)
            {
                for (int y = topTile; y <= bottomTile; y++)
                {
                    Tile tile = Framing.GetTileSafely(x, y);
                    if (tile != null && tileTypes.Contains(tile.TileType))
                    {
                        Vector2 tilePos = new Vector2(x * 16, y * 16);
                        float distance = Vector2.Distance(center, tilePos);
                        if (distance < closestDistance)
                        {
                            closestTilePos = tilePos;
                            closestDistance = distance;
                        }
                    }
                }
            }
            return closestTilePos;
        }

        public bool FindNearestHeartCrystal()
        {
            NearestHeart = Vector2.Zero;
            bool found = false;

            Vector2 heart = FindClosestTileInBox(Player.Center, 2400, 2400, 2400, TileID.Heart);
            if (heart != Vector2.Zero)
            {
                NearestHeart = heart;
                found = true;
            }
            return found;
        }

        public bool FindNearestLifeFruit()
        {
            NearestLifeFruit = Vector2.Zero;
            bool found = false;

            Vector2 lifeFruit = FindClosestTileInBox(Player.Center, 2400, 2400, 2400, TileID.LifeFruit);
            if (lifeFruit != Vector2.Zero)
            {
                NearestLifeFruit = lifeFruit;
                found = true;
            }
            return found;
        }
    }
}