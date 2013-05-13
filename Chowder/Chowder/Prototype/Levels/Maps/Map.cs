using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chowder.Prototype.Levels.Maps
{
    public class Map
    {
        public static Tile AIR_TILE;
        public static Tile FLOOR_TILE;
        public static Tile BLOCK_TILE;

        #region Fields
        private int width = 0;
        private int height = 0;
        private int tileWidth = 48;
        private int tileHeight = 48;
        private Tile[,] tiles;
        private string path = "";
        private List<Tile> tileList = new List<Tile>();
        #endregion

        #region Properties
        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public int TileWidth
        {
            get { return tileWidth; }
        }

        public int TileHeight
        {
            get { return tileHeight; }
        }

        public Tile[,] Tiles
        {
            get { return tiles; }
        }

        public string PathToMap
        {
            get { return path; }
        }

        public Vector2 GroundPoint
        {
            get { return new Vector2(ProjectData.GAMEHEIGHT - TileHeight); }
        }
        #endregion

        #region Initialization
        public Map()
        {
            Initialize();
        }

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            Initialize(width, height);
        }

        public Map(int[,] data)
        {
            this.width = data.GetLength(0);
            this.height = data.GetLength(1);
            Initialize(width, height);
        }

        public Map(string path)
        {
            this.path = path;
            Initialize();
            BuildMap(new MapParser(path).ParseMap());
        }

        private void Initialize(int width = 0, int height = 0)
        {
            var plainTexture = new DrawableRectangle(ProjectData.GraphicsDevice, new Vector2(48, 48), Color.White, true);

            AIR_TILE = new Tile(plainTexture.Texture, 48, 48, false, false, "air", Color.CornflowerBlue);
            FLOOR_TILE = new Tile(plainTexture.Texture, 48, 48, true, false, "floor", Color.GreenYellow);
            BLOCK_TILE = new Tile(plainTexture.Texture, 48, 48, true, false, "block", Color.LightGray);
            tileList.Add(AIR_TILE);
            tileList.Add(FLOOR_TILE);
            tileList.Add(BLOCK_TILE);
            if (width == 0 | height == 0)
                return;

            tiles = new Tile[width, height];

            // Default map creation
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = AIR_TILE;
                    if (y == ProjectData.GAMEHEIGHT / tileHeight - 1)
                        tiles[x, y] = FLOOR_TILE;
                }
            }
        }

        private void BuildMap(int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // TODO: Customize the map making... Maybe random?   
                }
            }
        }

        private void BuildMap(int[,] data)
        {
            width = data.GetLength(0);
            height = data.GetLength(1);
            tiles = new Tile[width, height];
            // TODO: Build a map based on an array made from a txt file
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = tileList[data[x, y]];
                }
            }
        }
        #endregion

        #region Update and Draw
        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    var currTile = tiles[x, y];
                    batch.Draw(currTile.Texture, new Vector2(x * tileWidth, y * tileHeight), (Color)currTile.Tint);
                }
        }
        #endregion
    }
}
