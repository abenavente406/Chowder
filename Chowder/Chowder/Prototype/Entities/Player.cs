using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using GameHelperLibrary.Shapes;
using Chowder.Prototype.Levels;
using Microsoft.Xna.Framework.Input;

namespace Chowder.Prototype.Entities
{
    public class Player : Entity
    {
        float floorPos;

        public Player(Vector2 pos)
            : base(pos)
        {
            floorPos = pos.Y;
            SetTexture();
        }

        protected override void SetTexture()
        {
            var tempTexture = new DrawableRectangle(ProjectData.GraphicsDevice, new Vector2(32, 48), Color.Black, true);
            imgLeft = new Image(tempTexture.Texture);
            imgRight = new Image(tempTexture.Texture);
            width = imgLeft.Width;
            height = imgLeft.Height;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 direction = Vector2.Zero;
            Vector2 movement = Vector2.Zero;
            if (InputHandler.KeyDown(Keys.Right))
            {
                direction = new Vector2(1, 0);
                movement += new Vector2(.7f, 0);
            }
            if (InputHandler.KeyDown(Keys.Left))
            {
                direction = new Vector2(-1, 0);
                movement += new Vector2(-.7f, 0);
            }

            if (!InputHandler.KeyDown(Keys.Left) &&
                !InputHandler.KeyDown(Keys.Right))
            {
                velocity.X = 0;
            }

            if (InputHandler.KeyPressed(Keys.Space) && !isJumping)
            {
                isJumping = true;
                Jump();
            }
            if (!InputHandler.KeyDown(Keys.Space) && isJumping)
            {
                if (velocity.Y < -1) ApplyForce(LevelManager.GRAVITY * 2);  // I'm not sure what I did but it works
            }

            dir = direction.X < 0 ? (byte)Directions.East : (byte)Directions.West;

            ApplyForce(movement);

            if (isJumping)
            {
                ApplyForce(LevelManager.GRAVITY);

                if (pos.Y > floorPos)
                {
                    pos.Y = floorPos;
                    isJumping = false;
                }
            }

            if (!isJumping)
                velocity.Y = 0;

            if (InputHandler.KeyPressed(Keys.Space)) isJumping = true;

            Velocity = new Vector2(velocity.X + acceleration.X, velocity.Y + acceleration.Y);
            var newPos = Position + velocity;
            Move(newPos);

            acceleration *= 0;
        }
    }
}
