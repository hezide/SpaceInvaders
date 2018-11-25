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
        private Texture2D               m_backgroundTexture;
        private bool                    m_isGameOver;
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
            m_isGameOver = false;
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_backgroundTexture = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_spaceInvadersManager.Init(this.Content);
        }

        protected override void UnloadContent()
        {
            m_spaceInvadersManager.UnloadContent(Content);
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if(!m_isGameOver)
            {
                if (m_spaceInvadersManager.IsGameOver)
                {
                    m_isGameOver = true;
                    OnGameOver(m_spaceInvadersManager.GetScore());
                }
                else
                {
                    m_spaceInvadersManager.Update(gameTime);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_spriteBatch.Begin();

            m_spriteBatch.Draw(m_backgroundTexture, new Vector2(0,0), Color.White);

            m_spriteBatch.End();

            m_spaceInvadersManager.Draw(gameTime);
            base.Draw(gameTime);
        }

        private void OnGameOver(int i_score)
        {
            MessageBox.Show("Game Over", "Your Score is: " + i_score, new[] { "OK" });
            Exit();
        }
    }
}
