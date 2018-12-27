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
        GraphicsDeviceManager           m_Graphics;
        private SpaceInvadersManager    m_SpaceInvadersManager;
        private SpriteBatch             m_SpriteBatch;
        private Texture2D               m_BackgroundTexture;
        private bool                    m_IsGameOver;
        public Game1()
        {
            m_Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = "Space Invaders";
            m_SpaceInvadersManager = new SpaceInvadersManager(this.GraphicsDevice);
            base.Initialize();
            IsMouseVisible = true;
            m_IsGameOver = false;
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            m_BackgroundTexture = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
            m_SpaceInvadersManager.Init(this.Content);
        }

        protected override void UnloadContent()
        {
            m_SpaceInvadersManager.UnloadContent(Content);
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if(!m_IsGameOver)
            {
                if (m_SpaceInvadersManager.IsGameOver)
                {
                    m_IsGameOver = true;
                    OnGameOver(m_SpaceInvadersManager.GetScore());
                }
                else
                {
                    m_SpaceInvadersManager.Update(gameTime);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_SpriteBatch.Begin();

            m_SpriteBatch.Draw(m_BackgroundTexture, new Vector2(0,0), Color.White);

            m_SpriteBatch.End();

            m_SpaceInvadersManager.Draw(gameTime);
            base.Draw(gameTime);
        }

        private void OnGameOver(int i_score)
        {
            MessageBox.Show("Game Over", "Your Score is: " + i_score, new[] { "OK" });
            Exit();
        }
    }
}
