using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Chowder.Prototype.Levels.Maps
{
    public class StaticPlatform : Platform
    {
        public StaticPlatform(Point pos, Point size)
            : base(new Vector2(pos.X * 48, pos.Y * 48), size) { }

        public override void Update(GameTime gameTime)
        {
            return;
        }
    }
}
