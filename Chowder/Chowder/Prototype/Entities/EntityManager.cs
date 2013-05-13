using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chowder.Prototype.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chowder.Prototype.Entities
{
    public class EntityManager
    {
        #region Fields
        private static Player player;
        #endregion

        #region Properties
        public static Player Player
        {
            get { return player; }
        }
        #endregion

        #region Initialization
        public EntityManager()
        {
            player = new Player(new Vector2(5, LevelManager.CurrentMap.GroundPoint.Y - 48));
        }
        #endregion

        #region Update
        public void Update(GameTime gameTime)
        {
            UpdatePlayer(gameTime);
        }

        public void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);
        }
        #endregion

        #region Draw
        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            player.Draw(batch, gameTime);
        }
        #endregion
    }
}
