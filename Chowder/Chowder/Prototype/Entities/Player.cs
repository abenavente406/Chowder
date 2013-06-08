using System;
using Microsoft.Xna.Framework;
using GameHelperLibrary;
using Chowder.Prototype.Levels;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Chowder.Prototype.Levels.Maps;

namespace Chowder.Prototype.Entities
{
    public class Player : Entity
    {
        float floorPos;

        public Player(Vector2 pos)
            : base(pos)
        {
            floorPos = pos.Y;
            this.pos.Y = floorPos - 5;
            SetTexture();
        }

        protected override void SetTexture()
        {
            //var tempTexture = new DrawableRectangle(ProjectData.GraphicsDevice, new Vector2(48, 48), Color.Black, true);
            //imgLeft = new Image(tempTexture.Texture);
            //imgRight = new Image(tempTexture.Texture);
            //width = imgLeft.Width;
            //height = imgLeft.Height;

            imgRight = new Image(ProjectData.Content.Load<Texture2D>("megaman"));

            var imagesRight = new Texture2D[] { EntityManager.runningSheet.GetSubImage(0, 0), 
                EntityManager.runningSheet.GetSubImage(0, 1), EntityManager.runningSheet.GetSubImage(0, 2),
                EntityManager.runningSheet.GetSubImage(0, 3), EntityManager.runningSheet.GetSubImage(0, 4) };
            var imagesLeft = new Texture2D[] { EntityManager.runningSheet.GetSubImage(1, 0),
                EntityManager.runningSheet.GetSubImage(1, 1), EntityManager.runningSheet.GetSubImage(1, 2),
                EntityManager.runningSheet.GetSubImage(1, 3), EntityManager.runningSheet.GetSubImage(1, 4) };
            var jumpsRight = new Texture2D[] { EntityManager.jumpSheet.GetSubImage(0, 0), 
                EntityManager.jumpSheet.GetSubImage(0, 1), EntityManager.jumpSheet.GetSubImage(0, 2),
                EntityManager.jumpSheet.GetSubImage(0, 2),EntityManager.jumpSheet.GetSubImage(0, 2),
                EntityManager.jumpSheet.GetSubImage(0, 2),EntityManager.jumpSheet.GetSubImage(0, 2),};

            movLeft = new Animation(imagesLeft, 65f);
            movRight = new Animation(imagesRight, 65f);
            jumpRight = new Animation(jumpsRight, 150f);

            width = 44;
            height = 42;
        }

        public override void Update(GameTime gameTime)
        {
            float epsilon = 0.05f;
            Vector2 direction = Vector2.Zero;
            Vector2 movement = new Vector2(.7f, 0);
            Vector2 compensation = new Vector2(0, 0);

            HandleInput(ref direction, ref movement);

            if (Math.Abs(0 - velocity.X) <= epsilon && 
                !InputHandler.KeyDown(Keys.Left) &&
                !InputHandler.KeyDown(Keys.Right)) 
                velocity.X = 0;
            else
                velocity.X = (velocity.X < 0) ? velocity.X + 0.3f : velocity.X - 0.3f;

            isMoving = velocity.X != 0;

            ApplyForce(movement * direction);
            ApplyForce(LevelManager.GRAVITY);

            if (isJumping)
            {
                if (pos.Y > floorPos)
                {
                    pos.Y = floorPos;
                    isJumping = false;
                }
            }

            Platform p = GetCurrentPlatform();
            if (p != null && p.GetType() == typeof(MovingPlatform))
            {
                var testP = (MovingPlatform)p;
                pos += testP.Velocity;
            }

            Velocity = new Vector2(velocity.X + acceleration.X + compensation.X, velocity.Y + acceleration.Y + compensation.Y);
            var newPos = Position + velocity;
            Move(newPos);

            acceleration *= 0;
        }

        private void HandleInput(ref Vector2 direction, ref Vector2 movement)
        {
            if (InputHandler.KeyDown(Keys.Right))
            {
                direction = new Vector2(1, 0);
                dir = Directions.East;
            }

            if (InputHandler.KeyDown(Keys.Left))
            {
                direction = new Vector2(-1, 0);
                dir = Directions.West;
            }

            if (!InputHandler.KeyDown(Keys.Left) &&
                !InputHandler.KeyDown(Keys.Right))
                velocity.X = 0;

            if (InputHandler.KeyPressed(Keys.Space) && !isJumping)
            {
                isJumping = true;
                Jump();
            }
            if (!InputHandler.KeyDown(Keys.Space) && isJumping)
            {
                if (velocity.Y < -1)
                    ApplyForce(LevelManager.GRAVITY * 2);  // I'm not sure what I did but it works
            }
            if (InputHandler.KeyPressed(Keys.Space)) isJumping = true;
        }

        private Platform GetCurrentPlatform()
        {
            foreach (Platform p in LevelManager.CurrentMap.TestPlatforms)
            {
                var test1 = new Rectangle((int)pos.X + 1, (int)pos.Y, (int)width, (int)height);
                var test2 = new Rectangle((int)pos.X, (int)pos.Y + 1, (int)width, (int)height);
                var test3 = new Rectangle((int)pos.X - 1, (int)pos.Y, (int)width, (int)height);

                if (test1.Intersects(p.Bounds)) return p;
                if (test2.Intersects(p.Bounds)) return p;
                if (test3.Intersects(p.Bounds)) return p;
            }
            return null;
        }

        protected override void Move(Vector2 newPos)
        {
            if (!LevelManager.IsSolidTile(newPos.X, Position.Y, (int)width, (int)height) && 
                !LevelManager.PlatformThere(new Rectangle((int)newPos.X, (int)Position.Y, (int)width, (int)height)))
                pos.X = newPos.X;

            if (LevelManager.IsSolidTile(newPos.X, Position.Y, (int)width, (int)height))
                velocity.X = 0;

            if (!LevelManager.IsSolidTile(Position.X, newPos.Y, (int)width, (int)height) &&
                !LevelManager.PlatformThere(new Rectangle((int)Position.X, (int)newPos.Y, (int)width, (int)height)))
                pos.Y = newPos.Y;
            else
            {
                velocity.Y = 0;
                isJumping = false;
            }
        }

        public override void Draw(SpriteBatch batch, GameTime gameTime)
        {
            if (isJumping)
            {
                if (dir == Directions.East)
                {
                    jumpRight.Draw(batch, gameTime, Position);
                }
                else
                {
                    jumpRight.Draw(batch, gameTime, Position, true);
                }

                if (jumpRight.CurrentFrame > 4) jumpRight.CurrentFrame = 1;
                return;
            }
            else
                jumpRight.CurrentFrame = 0;

            if (isMoving)
            {
                switch (dir)
                {
                    case Directions.West:
                        if (movLeft == null)
                            imgRight.Draw(batch, Position, true);
                        else
                            movLeft.Draw(batch, gameTime, Position);
                        break;
                    case Directions.East:
                        if (movRight == null)
                            imgRight.Draw(batch, Position);
                        else
                            movRight.Draw(batch, gameTime, Position);
                        break;
                }
            }
            else
            {
                switch (dir)
                {
                    case Directions.West:
                        imgRight.Draw(batch, Position, true);
                        break;
                    case Directions.East:
                        imgRight.Draw(batch, Position);
                        break;
                }
            }
        }
    }
}
