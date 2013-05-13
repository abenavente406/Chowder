using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Chowder.Prototype.Entities;
using Chowder.Prototype.Levels;

namespace Chowder
{
    public class Camera
    {
        private static int viewportWidth;
        private static int viewportHeight;
        private static Vector2 pos;
        private Matrix transform;

        #region Properties
        public static Vector2 Position
        {
            get { return pos; }
            set
            {
                pos = value;
            }
        }
        public Matrix TransformMatrix
        {
            get { return transform; }
            set { transform = value; }
        }

        public static Rectangle ViewPortRectangle
        {
            get
            {
                return new Rectangle((int)(Position.X - viewportWidth / 2),
                    (int)(Position.Y - viewportHeight / 2), (int)(viewportWidth), (int)(viewportHeight));
            }
        }
        #endregion

        public Camera(Vector2 startPos, int viewPortWidth, int viewPortHeight)
        {
            Position = startPos;
            viewportWidth = viewPortWidth;
            viewportHeight = viewPortHeight;
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            Vector3 matrixRotOrigin = new Vector3(Position.X, Position.Y, 0);
            Vector3 matrixScreenPos = new Vector3(new Vector2(viewportWidth / 2, viewportHeight / 2), 0);

            transform = Matrix.CreateTranslation(-matrixRotOrigin) *
                         Matrix.CreateTranslation(matrixScreenPos);

            return transform;
        }

        public void Move(Vector2 amount)
        {
            if (EntityManager.Player.IsMoving)
                Position += pos;
          
        }

        public void Update(Vector2 newPos)
        {
            Position = newPos;
        }

        public static Vector2 Transform(Vector2 point)
        {
            return point;
        }

        public static bool IsOnCamera(Entity entity)
        {
            return true; // For now
        }

        public static bool IsOnCamera(Rectangle rect)
        {
            return ViewPortRectangle.Intersects(rect);
        }
    }
}