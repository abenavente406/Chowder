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
    public enum Directions{ West = -1, East = 1}
    public abstract class Entity
    {
        protected static Vector2 MAXSPEED = new Vector2(6, 6);

        #region Fields
        protected Vector2 pos = Vector2.Zero;
        protected Directions dir = Directions.East;
        protected Vector2 velocity = Vector2.Zero;
        protected Vector2 acceleration = Vector2.Zero;
        protected float width = 0;
        protected float height = 0;
        protected Image imgLeft, imgRight;
        protected Animation movLeft, movRight;
        protected Animation jumpRight;
        protected bool isMoving = false;
        protected bool isJumping = false;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public Point GridPos
        {
            get { return LevelManager.Vector2toTile(Position); }
        }

        public Directions Direction
        {
            get { return dir; }
            set { dir = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = Vector2.Clamp(value, -MAXSPEED, MAXSPEED); }
        }

        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = Vector2.Clamp(value, -MAXSPEED, MAXSPEED); }
        }

        public Rectangle SpriteBounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, (int)width, (int)height); }
        }

        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        public bool IsMoving
        {
            get { return isMoving; }
            set { isMoving = true; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)pos.X, (int)pos.Y, (int)width, (int)height); }
        }
        #endregion

        #region Initialization
        public Entity(Vector2 pos)
        {
            Position = pos;
        }

        protected abstract void SetTexture();
        #endregion

        #region Update and Draw
        public abstract void Update(GameTime gameTime);
        public virtual void Draw(SpriteBatch batch, GameTime gameTime)
        {
            switch (Direction)
            {
                case Directions.East:
                    movRight.Draw(batch, gameTime, Position);
                    break;
                case Directions.West:
                    movRight.Draw(batch, gameTime, Position, true);
                    break;
            }

        }
        #endregion

        #region Movement Methods
        protected virtual void Move(Vector2 newPos)
        {
            if (!LevelManager.IsSolidTile(newPos.X, Position.Y, (int)width, (int)height) &&
                !LevelManager.PlatformThere(new Rectangle((int)newPos.X, (int)Position.Y, (int)width, (int)height)))
                pos.X = newPos.X;

            if (!LevelManager.IsSolidTile(Position.X, newPos.Y, (int)width, (int)height) &&
                !LevelManager.PlatformThere(new Rectangle((int)Position.X, (int)newPos.Y, (int)width, (int)height)))
                pos.Y = newPos.Y;
            else
                velocity.Y = 0;
        }

        protected void Jump()
        {
            ApplyForce(new Vector2(0, -6.75f));
        }

        protected void ApplyForce(Vector2 force)
        {
            Acceleration += force;
        }
        #endregion
    }
}
