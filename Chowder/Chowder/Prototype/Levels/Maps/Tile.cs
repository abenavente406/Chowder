using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Chowder.Prototype.Levels.Maps
{
    public class Tile
    {
        #region Fields
        private string name = "";
        private Texture2D texture;
        private int width = 0;
        private int height = 0;
        private bool isWalkable = true;
        private bool isHurtful = false;
        private Color? tint = null;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public Point Size
        {
            get { return new Point(width, height); }
        }

        public bool Walkable
        {
            get { return isWalkable; }
            set { isWalkable = true; }
        }

        public bool Hurtful
        {
            get { return isHurtful; }
            set { isHurtful = value; }
        }

        public Color? Tint
        {
            get { return tint; }
            set { tint = value; }
        }
        #endregion

        public Tile(Texture2D texture, int width, int height, bool walkable, bool hurtful, string name, Color? color = null)
        {
            this.texture = texture;
            this.width = width;
            this.height = height;
            this.isWalkable = walkable;
            this.isHurtful = hurtful;
            this.name = name;
            this.tint = color == null ? Color.White : color;
        }
    }
}
