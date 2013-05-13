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
            currentMap = new Map(@"C:\Users\Anthony Benavente\Desktop\test_map.txt");
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

        public static bool IsWalkable(Point pos)
        {
            if (pos.X < 0) return false;
            if (pos.X > currentMap.Width) return false;

            return currentMap.Tiles[pos.X, pos.Y].Walkable;
        }
    }
}
