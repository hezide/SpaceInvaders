using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class Game1 : Game
    {
        GraphicsDeviceManager           m_graphics;
        private SpaceInvadersManager    m_spaceInvadersManager;
        private SpriteBatch             m_spriteBatch;

        public Game1()
        {
            m_graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = "Space Invaders";
            m_spaceInvadersManager = new SpaceInvadersManager(this.GraphicsDevice);
            base.Initialize();
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_graphics.PreferredBackBufferWidth = Utilities.k_ScreenWidth;
            m_graphics.PreferredBackBufferHeight = Utilities.k_ScreenHeight;
            m_graphics.ApplyChanges();

            m_spaceInvadersManager.Init(this.Content);
        }

        protected override void UnloadContent()
        {
            //todo:: need to unload content in all the others maybe
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            m_spaceInvadersManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            m_spaceInvadersManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
