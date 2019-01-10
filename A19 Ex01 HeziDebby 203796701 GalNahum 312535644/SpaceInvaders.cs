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
        private readonly InputManager m_InputManager;
        private readonly CollisionsManager m_CollisionsManager;
        private PlayerSpaceship m_Player1;
        private PlayerSpaceship m_Player2;
        private EnemiesGroup m_Enemies;
        private Barriers m_Barriers;
        private MotherShip m_MotherSpaceship;
        private RandomActionComponent m_MotherShipRandomNotifier;
        private readonly List<ScoreManager> m_ScoreManagers;
        // TODO: habndle backround design
        private Texture2D m_BackgroundTexture;
        //   private bool                    m_IsGameOver;
        // TODO: fix score - positions + need to be game service + scores are hard coded 
        public SpaceInvaders()
        {
            m_Graphics = new GraphicsDeviceManager(this);

            m_Player1 = new PlayerSpaceship(this);
            m_Player2 = new PlayerSpaceship(this);
            m_InputManager = new InputManager(this);
            m_CollisionsManager = new CollisionsManager(this);

            m_Enemies = new EnemiesGroup(this);
            m_Barriers = new Barriers(this);
            m_MotherSpaceship = new MotherShip(this);
            m_MotherShipRandomNotifier = new RandomActionComponent();
            m_MotherShipRandomNotifier.RandomTimeAchieved += notifier_GoMotherSpaceship;

            m_ScoreManagers = new List<ScoreManager>
            {
                { new ScoreManager("P1", this) { TintColor = Color.Blue} },
                { new ScoreManager("P2", this) { TintColor = Color.LimeGreen} }
            };

            Content.RootDirectory = "Content";
        } // TODO: check sprite batch

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "Space Invaders";

            IsMouseVisible = true;
            // TODO: insert to methods ?
        //    this.Services.AddService(typeof(SpriteBatch), m_SpriteBatch);// TODO: whats going on with the sprite batch ???
            // *** initializing activation inputs ***
            m_Player1.ActivateByMouse = true;
            m_Player1.ActivationKeysList = new List<Keys> { Keys.H, Keys.K, Keys.U };

            m_Player2.ActivationKeysList = new List<Keys> { Keys.A, Keys.D, Keys.W };
            // *** initializing non default players positions ***
            m_Player2.Position = new Vector2(m_Player1.Position.X + m_Player1.Width, m_Player2.Position.Y);
            m_Player2.TintColor = Color.LimeGreen;

            // *** initializing souls components positions ***
            m_Player2.SoulsComponent.Position = new Vector2(m_Player2.SoulsComponent.Position.X, m_Player2.SoulsComponent.Position.Y + m_Player2.SoulsComponent.Height + 6);
            m_Player2.SoulsComponent.TintColor = m_Player2.TintColor;
            //   m_IsGameOver = false;

            initScoreManagers(); // TODO: is score managers list is sprite collection ?

            m_Enemies.Initialize();

            m_Barriers.Initialize(GraphicsDevice.Viewport.Width, m_Player1.Position.Y);
        }
        // TODO: change the name
        private void initScoreManagers()
        {// TODO: discusting. change 
            m_Player1.AddCollisionListener(m_ScoreManagers[0].CollisionHandler);
            m_Player2.AddCollisionListener(m_ScoreManagers[1].CollisionHandler);

            m_ScoreManagers[1].Position = new Vector2(m_ScoreManagers[1].Position.X, m_ScoreManagers[0].Bounds.Height);
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            m_BackgroundTexture = Content.Load<Texture2D>(@"Sprites\BG_Space01_1024x768");
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            m_MotherShipRandomNotifier.Update(gameTime);

            m_Enemies.Update(gameTime); // TODO: i dont want to see this in the code .......
            m_Barriers.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (IsGameOver())
            {
                OnGameOver();
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

        private bool IsGameOver()
        {
            return (m_Player1.SoulsComponent.NumberOfSouls == 0 && m_Player2.SoulsComponent.NumberOfSouls == 0);
            //|| m_Enemies.Bounds.Bottom == m_Player1.Height)
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_SpriteBatch.Begin();

            m_SpriteBatch.Draw(m_BackgroundTexture, new Vector2(0, 0), Color.White); // TODO: background class

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

        private void OnGameOver()
        {
            string finalScore = string.Format(@"
{0}
{1}
The winner is: {2}", m_ScoreManagers[0].TextToString(), m_ScoreManagers[1].TextToString(), getWinner());

            MessageBox.Show("Game Over", finalScore, new[] { "OK" });
            Exit();
        }

        private string getWinner()
        {
            return m_ScoreManagers[0].Score > m_ScoreManagers[1].Score ? m_ScoreManagers[0].Name : m_ScoreManagers[1].Name;
        }
    }
}
