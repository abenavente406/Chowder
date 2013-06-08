using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameHelperLibrary;
using Chowder.Prototype.Levels;

namespace Chowder.Prototype.Entities
{
    public class Enemy : Entity
    {
        public Enemy(Vector2 pos)
            : base(pos)
        {
            SetTexture();
            Direction = new Random().Next() % 2 == 0 ? Directions.East : Directions.West;
        }

        protected override void SetTexture()
        {
            return;
        }

        public override void Update(GameTime gameTime)
        {
            return;
        }
    }
}
