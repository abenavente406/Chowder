using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chowder.Prototype.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameHelperLibrary;

namespace Chowder.Prototype.Entities
{
    public class EntityManager
    {
        #region Fields
        private static Player player;
        public static SpriteSheet runningSheet;
        public static SpriteSheet jumpSheet;
        public static SpriteSheet koopaSheet;

        static List<Enemy> enemies = new List<Enemy>();
        #endregion

        #region Properties
        public static Player Player
        {
            get { return player; }
        }

        public static List<Enemy> Enemies
        {
            get { return enemies; }
        }
        #endregion

        #region Initialization
        public EntityManager()
        {
            runningSheet = new SpriteSheet(ProjectData.Content.Load<Texture2D>("megamanRun"), 44, 42, ProjectData.GraphicsDevice);
            jumpSheet = new SpriteSheet(ProjectData.Content.Load<Texture2D>("megamanJump"), 37, 51, ProjectData.GraphicsDevice);
            koopaSheet = new SpriteSheet(ProjectData.Content.Load<Texture2D>("koopa"), 24, 32, ProjectData.GraphicsDevice);
            player = new Player(new Vector2(44, LevelManager.CurrentMap.GroundPoint.Y - 42));
            enemies.Add(new Koopa(new Vector2(48 * 24, LevelManager.CurrentMap.GroundPoint.Y - 33)));

        }
        #endregion

        #region Update
        public void Update(GameTime gameTime)
        {
            UpdatePlayer(gameTime);
            UpdateEnemies(gameTime);
        }

        public void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public void UpdateEnemies(GameTime gameTime)
        {
            Enemies.ForEach((e) => { e.Update(gameTime); });
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            player.Draw(batch, gameTime);
            Enemies.ForEach((e) => { e.Draw(batch, gameTime); });
        }
        #endregion
    }
}
