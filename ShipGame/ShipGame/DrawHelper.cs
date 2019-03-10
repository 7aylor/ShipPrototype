using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShipGame
{
    static class DrawHelper
    {
        public static SpriteBatch spriteBatch;
        public static Dictionary<Tile, Texture2D> textures;

        /// <summary>
        /// Initializes the DrawHelper so that spritebatch and textures have values that can
        /// be used statically
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="txts"></param>
        public static void Init(SpriteBatch sb, Dictionary<Tile, Texture2D> txts)
        {
            spriteBatch = sb;
            textures = txts;
        }
    }
}
