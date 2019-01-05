using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class SpaceInvaders : Game
    {
        readonly GraphicsDeviceManager m_Graphics;
        // private SpaceInvadersManager m_SpaceInvadersManager;
        private SpriteBatch m_SpriteBatch;
        private InputManager m_InputManager;
        private CollisionsManager m_CollisionsManager;
        private PlayerSpaceship m_Player1;
        private PlayerSpaceship m_Player2;
        private EnemiesMat m_Enemies;
        private Barriers m_Barriers; 
        private MotherShip m_MotherSpaceship;
        private RandomActionComponent m_MotherSpaceShipRandomNotifier;
        // TODO: habndle backround design
        private Texture2D               m_BackgroundTexture;
        //   private bool                    m_IsGameOver;

        public SpaceInvaders()
        {
            m_Graphics = new GraphicsDeviceManager(this);

            m_Player1 = new PlayerSpaceship(this);
            m_Player2 = new PlayerSpaceship(this);
            m_InputManager = new InputManager(this);
            m_CollisionsManager = new CollisionsManager(this);

            m_Enemies = new EnemiesMat(this);
            m_Barriers = new Barriers(this);
            m_MotherSpaceship = new MotherShip(this);
            m_MotherSpaceShipRandomNotifier = new RandomActionComponent();
            m_MotherSpaceShipRandomNotifier.RandomTimeAchieved += notifier_GoMotherSpaceship;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "Space Invaders";
            
            IsMouseVisible = true;

            this.Services.AddService(typeof(SpriteBatch), m_SpriteBatch);
            // *** initializing activation inputs ***
            m_Player1.ActivateByMouse = true;
            m_Player1.ActivationKeysList = new List<Keys> { Keys.H, Keys.K, Keys.U };
            m_Player2.ActivationKeysList = new List<Keys> { Keys.A, Keys.D, Keys.W };
            // *** initializing non default players positions ***
            m_Player2.Position = new Vector2(m_Player1.Position.X + m_Player1.Width, m_Player2.Position.Y);
            m_Player2.TintColor = Color.LightGreen;
            // *** initializing souls components positions ***

            m_Player2.SoulsComponent.Position = new Vector2(m_Player2.SoulsComponent.Position.X, m_Player2.SoulsComponent.Position.Y + m_Player2.SoulsComponent.Height + 6);
            m_Player2.SoulsComponent.TintColor = m_Player2.TintColor;
            //   m_IsGameOver = false;

            m_Enemies.Initialize();

            m_Barriers.Initialize(GraphicsDevice.Viewport.Width, m_Player1.Position.Y);
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            m_BackgroundTexture = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
    //        m_SpaceInvadersManager.Init(this.Content);
        }

        protected override void UnloadContent()
        {
   //         m_SpaceInvadersManager.UnloadContent(Content);
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            m_MotherSpaceShipRandomNotifier.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            //   if(!m_IsGameOver)
            //   {
            //if (m_SpaceInvadersManager.IsGameOver)
            //{
            //    //           m_IsGameOver = true;
            //    OnGameOver(m_SpaceInvadersManager.GetScore());
            //}
            //else
            //{
            //    m_SpaceInvadersManager.Update(gameTime);
            //}
            //   }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_SpriteBatch.Begin();

            m_SpriteBatch.Draw(m_BackgroundTexture, new Vector2(0, 0), Color.White);

            m_SpriteBatch.End();

            //m_SpaceInvadersManager.Draw(gameTime);
            base.Draw(gameTime);
        }

        private void notifier_GoMotherSpaceship()
        {
            //if (m_MotherSpaceship == null)
            //{
            //    m_MotherSpaceship = new MotherSpaceship(this);
            //}

            if (m_MotherSpaceship.IsOutOfSight())
            {
                m_MotherSpaceship.SetInitialValues();
            }
        }

        private void OnGameOver(int i_score)
        {
            MessageBox.Show("Game Over", "Your Score is: " + i_score, new[] { "OK" });
            Exit();
        }
    }
}
