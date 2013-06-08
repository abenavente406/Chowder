using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameHelperLibrary.Shapes;

namespace Chowder.Prototype.Levels.Maps
{
    public abstract class Platform
    {
        protected Vector2 pos;
        protected int widthInTiles;
        protected int heightInTiles;
        protected int width;
        protected int height;
        protected Texture2D texture;

        private Color defaultColor = Color.Red;

        public virtual Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public int WidthInTiles
        {
            get { return widthInTiles; }
        }

        public int HeightInTiles
        {
            get { return heightInTiles; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)pos.X, (int)pos.Y, width, height); }
        }

        public Platform(Vector2 pos, Point sizeInTiles, Texture2D texture = null)
        {
            this.pos = pos;
            this.widthInTiles = sizeInTiles.X;
            this.width = widthInTiles * 48;
            this.heightInTiles = sizeInTiles.Y;
            this.height = heightInTiles * 48;
            this.texture = texture != null ? texture : new DrawableRectangle(ProjectData.GraphicsDevice,
                new Vector2(width, height), Color.White, true).Texture;
        }

        public abstract void Update(GameTime gameTime);

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            batch.Draw(texture, new Rectangle((int)pos.X, (int)pos.Y, width, height), defaultColor);
        }
    }
}
