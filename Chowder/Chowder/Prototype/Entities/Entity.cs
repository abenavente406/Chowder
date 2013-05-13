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
    public enum Directions : byte { West, East}
    public abstract class Entity
    {
        protected static Vector2 MAXSPEED = new Vector2(8, 8);

        #region Fields
        protected Vector2 pos = Vector2.Zero;
        protected byte dir = (byte)Directions.East;
        protected Vector2 velocity = Vector2.Zero;
        protected Vector2 acceleration = Vector2.Zero;
        protected float width = 0;
        protected float height = 0;
        protected Image imgLeft, imgRight;
        protected Animation movLeft, movRight;
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

        public byte Direction
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
            switch (dir)
            {
                case (byte)Directions.West:
                    imgLeft.Draw(batch, Position);
                    break;
                case (byte)Directions.East:
                    imgRight.Draw(batch, Position);
                    break;
            }
        }
        #endregion

        #region Movement Methods
        protected void Move(Vector2 pos)
        {
            var testPos = new Vector2(pos.X - Width, pos.Y);
            Point newGridPos = new Point(0, 0);

            switch (dir)
            {
                case 0:
                    newGridPos = new Point(GridPos.X - 1, GridPos.Y);
                    break;
                case 1:
                    newGridPos = new Point(GridPos.X + 1, GridPos.Y);
                    break;
            }

            if (true)//!LevelManager.IsWalkable(newGridPos))
            {
                isMoving = true;
                Position = pos;
            }
            //else
            //{
            //    isMoving = false;
            //}
        }

        protected void Jump()
        {
            ApplyForce(new Vector2(0, -6.5f));
        }

        protected void ApplyForce(Vector2 force)
        {
            Acceleration += force;
        }
        #endregion
    }
}
