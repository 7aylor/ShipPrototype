using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShipGame
{
    public class GameBase : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //debug
        SpriteFont defaultFont;
        MouseState mState;
        int mPosToGridX;
        int mPosToGridY;
        Texture2D pixel;

        Map map;
        Dictionary<Tile, Texture2D> textures;

        public GameBase()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            this.IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            map = new Map(40, 22, 10);
            textures = new Dictionary<Tile, Texture2D>();
            textures.Add(Tile.Water, Content.Load<Texture2D>("water"));
            textures.Add(Tile.Grass, Content.Load<Texture2D>("grass"));
            textures.Add(Tile.Dirt, Content.Load<Texture2D>("dirt"));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //debug
            defaultFont = Content.Load<SpriteFont>("default");
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            DrawHelper.Init(spriteBatch, textures);

        }

        protected override void UnloadContent() {}

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mState = Mouse.GetState();
            mPosToGridX = mState.Position.X / 32;
            mPosToGridY = mState.Position.Y / 32;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            map.Draw();

            spriteBatch.DrawString(defaultFont, mPosToGridX.ToString() + "," + mPosToGridY.ToString(), new Vector2(mState.Position.X + 10, mState.Position.Y), Color.Yellow);

            int gridX = mPosToGridX * 32;
            int gridY = mPosToGridY * 32;


            for (int i = 0; i < 32; i++)
            {
                spriteBatch.Draw(pixel, new Vector2((float)gridX + i, (float)gridY),  Color.Yellow);
                spriteBatch.Draw(pixel, new Vector2((float)gridX + i, (float)gridY + 32), Color.Yellow);
                spriteBatch.Draw(pixel, new Vector2((float)gridX, (float)gridY + i), Color.Yellow);
                spriteBatch.Draw(pixel, new Vector2((float)gridX + 32, (float)gridY + i), Color.Yellow);
            }

            spriteBatch.DrawString(defaultFont, "Land Neighbors: " + map.GetNeighborLandCount(mPosToGridX, mPosToGridY).ToString(), new Vector2(3, 704), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
