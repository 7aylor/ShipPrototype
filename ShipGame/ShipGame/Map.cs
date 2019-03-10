using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShipGame
{
    enum Tile { Water, Grass, Dirt }

    class Map
    {
        public int SizeX { get; private set; }   
        public int SizeY { get; private set; }
        private int landRatio;

        public Map(int sizeX, int sizeY, int landRatio)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            this.landRatio = landRatio;
            Generate();
        }

        public static Tile[,] map;

        /// <summary>
        /// Generates the map for our game world
        /// </summary>
        public void Generate()
        {
            //random number generator to determine if a tile is land
            Random rand = new Random();

            //set the size of our map
            map = new Tile[SizeX, SizeY];

            int smoothCycles = 5;

            Tile[,] tempMap = new Tile[SizeX, SizeY];

            //set up base map
            for(int y = 0; y < SizeY; y++)
            {
                for(int x = 0; x < SizeX; x++)
                {
                    if(rand.Next(0, 100) < landRatio)
                    {
                        map[x, y] = Tile.Grass;
                    }
                    else
                    {
                        map[x, y] = Tile.Water;
                    }
                }
            }

            //make island clumps
            for (int i = 0; i < smoothCycles; i++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    for (int x = 0; x < SizeX; x++)
                    {
                        int neighborLandCount = GetNeighborLandCount(x, y);
                        if (neighborLandCount < 3 && rand.Next(0,100) < 99)
                        {
                            tempMap[x, y] = Tile.Water;
                        }
                        else
                        {
                            tempMap[x, y] = Tile.Grass;
                        }
                    }
                }
                map = tempMap;
            }

            //Add Dirt

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    int neighborLandCount = GetNeighborLandCount(x, y);
                    if (map[x,y] == Tile.Grass && neighborLandCount <= 5)
                    {
                        map[x, y] = Tile.Dirt;
                    }
                }
            }
        }

        /// <summary>
        /// Looks at a cell in our map and returns how many neighboring cells are land
        /// </summary>
        /// <param name="xPos">X Position</param>
        /// <param name="yPos">Y Position</param>
        /// <returns></returns>
        public int GetNeighborLandCount(int xPos, int yPos)
        {
            int countNeighborLand = 0;
            for(int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if(xPos + x > 0 && xPos + x < SizeX &&
                       yPos + y > 0 && yPos + y < SizeY &&
                       (xPos + x != xPos || yPos + y != yPos))
                    {   
                        if(map[xPos + x, yPos + y] == Tile.Grass || map[xPos + x, yPos + y] == Tile.Dirt)
                        {
                            countNeighborLand++;
                        }
                    }
                }
            }

            return countNeighborLand;
        }

        /// <summary>
        /// draws out map
        /// </summary>
        public void Draw()
        {
            for(int y = 0; y < SizeY; y++)
            {
                for(int x = 0; x < SizeX; x++)
                {
                    Texture2D t = DrawHelper.textures[map[x, y]];
                    DrawHelper.spriteBatch.Draw(t, new Vector2(x * t.Width, y * t.Height), Color.White);
                }
            }
        }
    }
}
