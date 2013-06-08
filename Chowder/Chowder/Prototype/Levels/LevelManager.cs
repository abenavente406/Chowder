using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Chowder.Prototype.Levels.Maps;
using Microsoft.Xna.Framework.Graphics;
using Chowder.Prototype.Entities;

namespace Chowder.Prototype.Levels
{
    public class LevelManager
    {
        public static Vector2 GRAVITY = new Vector2(0, .21875f);

        private static Map currentMap;
        public static Map CurrentMap
        {
            get { return currentMap; }
        }

        EntityManager em;

        public LevelManager()
        {
            currentMap = new Map(@".\test_map.txt");
            em = new EntityManager();
        }

        public void Update(GameTime gameTime)
        {
            currentMap.Update(gameTime);
            em.Update(gameTime);
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            currentMap.Draw(batch, gameTime);
            em.Draw(batch, gameTime);
        }

        public static Point Vector2toTile(Vector2 vec)
        {
            return new Point((int)vec.X / CurrentMap.TileWidth, (int)vec.Y / CurrentMap.TileHeight);
        }
        public static int XToTile(float x)
        {
            return (int)x / currentMap.TileWidth;
        }
        public static int YToTile(float y)
        {
            return (int)y / currentMap.TileHeight;
        }

        public static bool IsSolidTile(float x, float y, int width, int height)
        {
            int atx1 = (int)(x / currentMap.TileWidth);
            int atx2 = (int)(x + width) / currentMap.TileWidth;
            int aty1 = (int)(y / currentMap.TileHeight);
            int aty2 = (int)(y + height) / currentMap.TileHeight;

            if (atx1 < 0 || aty1 < 0 || atx2 >= currentMap.Width / currentMap.TileWidth ||
                aty2 >= currentMap.Height / currentMap.TileHeight)
            {
                return true;
            }

            if (IsTileBlocked(atx1, aty1))
                return true;
            if (IsTileBlocked(atx1, aty2))
                return true;
            if (IsTileBlocked(atx2, aty1))
                return true;
            if (IsTileBlocked(atx2, aty2))
                return true;

            return false;
        }

        public static bool IsTileBlocked(int x, int y)
        {
            if (y >= currentMap.Tiles.GetLength(1) || x >= currentMap.Tiles.GetLength(0))
                return true;

            return currentMap.Tiles[x, y].Solid;
        }

        public static bool PlatformThere(Rectangle bounds)
        {
            foreach (Platform p in currentMap.TestPlatforms)
            {
                if (p.Bounds.Intersects(bounds)) return true;
            }
            return false;
        }
    }
}
