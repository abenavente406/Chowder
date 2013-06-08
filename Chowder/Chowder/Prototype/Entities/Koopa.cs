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
    public class Koopa : Enemy
    {
        public Koopa(Vector2 pos)
            : base(pos)
        {
            SetTexture();
        }

        protected override void SetTexture()
        {
            Texture2D[] imgsRight = new Texture2D[] {
                EntityManager.koopaSheet.GetSubImage(0, 0),
                EntityManager.koopaSheet.GetSubImage(1, 0)
            };

            movRight = new Animation(imgsRight, 300f);
            imgRight = new Image(imgsRight[0]);

            width = 24;
            height = 32;
        }

        public override void Update(GameTime gameTime)
        {
            ApplyForce(new Vector2(.6f * (int)dir, 0));
            ApplyForce(LevelManager.GRAVITY);

            Velocity = Acceleration;
            var newPos = Position + Velocity;
            Move(newPos);

            Acceleration *= 0;
            base.Update(gameTime);
        }
    }
}
