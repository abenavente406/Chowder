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

        List<Platform> testPlatforms = new List<Platform>();
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

        public int WidthInTiles
        {
            get { return width / tileWidth; }
        }

        public int HeightInTiles
        {
            get { return height / tileHeight; }
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

        public List<Platform> TestPlatforms
        {
            get { return testPlatforms; }
        }
        #endregion

        #region Initialization
        public Map()
        {
            Initialize();
        }

        public Map(int width, int height)
        {
            this.width = width * tileWidth;
            this.height = height * tileHeight;
            Initialize(WidthInTiles, HeightInTiles);
        }

        public Map(int[,] data)
        {
            this.width = data.GetLength(0) * tileWidth;
            this.height = data.GetLength(1) * tileHeight;
            Initialize(WidthInTiles, HeightInTiles);
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

            testPlatforms.Add(new MovingPlatform(new Point(0, 4), new Point(3, 1), 4, MovementType.HORIZONTAL_RIGHT));
            testPlatforms.Add(new MovingPlatform(new Point(3, 6), new Point(3, 1), 4, MovementType.HORIZONTAL_LEFT));
            testPlatforms.Add(new MovingPlatform(new Point(8, 4), new Point(3, 1), 2, MovementType.VERTICAL_DOWN));
            testPlatforms.Add(new MovingPlatform(new Point(12, 4), new Point(3, 1), 2, MovementType.VERTICAL_UP));
            testPlatforms.Add(new StaticPlatform(new Point(16, 4), new Point(3, 1)));

            if (width == 0 | height == 0)
                return;

            tiles = new Tile[WidthInTiles, HeightInTiles];

            // Default map creation
            for (int x = 0; x < WidthInTiles; x++)
            {
                for (int y = 0; y < HeightInTiles; y++)
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
            width = data.GetLength(0) * tileWidth;
            height = data.GetLength(1) * tileHeight;
            tiles = new Tile[WidthInTiles, HeightInTiles];

            for (int x = 0; x < WidthInTiles; x++)
            {
                for (int y = 0; y < HeightInTiles; y++)
                {
                    tiles[x, y] = tileList[data[x, y]];
                }
            }
        }
        #endregion

        #region Update and Draw
        public void Update(GameTime gameTime)
        {
            foreach (Platform p in testPlatforms)
                p.Update(gameTime);
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            for (int x = 0; x < WidthInTiles; x++)
            {
                for (int y = 0; y < HeightInTiles; y++)
                {
                    var currTile = tiles[x, y];
                    batch.Draw(currTile.Texture, new Vector2(x * tileWidth, y * tileHeight), (Color)currTile.Tint);
                }
            }

            foreach (Platform p in testPlatforms)
                p.Draw(batch, gameTime);
        }
        #endregion
    }
}
