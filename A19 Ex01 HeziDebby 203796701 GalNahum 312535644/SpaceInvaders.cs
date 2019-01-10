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
        private SpriteBatch m_SpriteBatch;
        private readonly Background m_Background;
        private readonly InputManager m_InputManager;
        private readonly CollisionsManager m_CollisionsManager;
        private Spaceship m_Player1;
        private Spaceship m_Player2;
        private EnemiesGroup m_Enemies;
        private Barriers m_Barriers;
        private MotherShip m_MotherSpaceship;
        private RandomActionComponent m_MotherShipRandomNotifier;
        private readonly List<ScoreManager> m_ScoreManagers;

        // TODO: fix score - positions + need to be game service + scores are hard coded 
        public SpaceInvaders()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            m_Background = new Background(this);
            m_Player1 = new Spaceship(@"Sprites\Ship01_32x32", this);
            m_Player2 = new Spaceship(@"Sprites\Ship02_32x32", this);
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
                { new ScoreManager("P2", this) { TintColor = Color.Green} }
            };

            Content.RootDirectory = "Content";
        } // TODO: check shared sprite batch - currently creating for everyone ther own sprite batch

        protected override void Initialize()
        {// TODO: *** this is where this should be for the spriteBatch to be for everyone
            //m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            //this.Services.AddService(typeof(SpriteBatch), m_SpriteBatch);

            base.Initialize();
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), m_SpriteBatch);
            Window.Title = "Space Invaders";

            IsMouseVisible = true;

            // TODO: insert to methods ?

            // *** initializing activation inputs ***
            m_Player1.ActivateByMouse = true;
            m_Player1.ActivationKeysList = new List<Keys> { Keys.H, Keys.K, Keys.U };
            m_Player2.ActivationKeysList = new List<Keys> { Keys.A, Keys.D, Keys.W };
            
            // *** initializing non default players positions ***
            m_Player2.Position = new Vector2(m_Player1.Position.X + m_Player1.Width, m_Player2.Position.Y);

            // *** initializing souls components positions ***
            m_Player2.SoulsComponent.Position = new Vector2(m_Player2.SoulsComponent.Position.X, m_Player2.SoulsComponent.Position.Y + m_Player2.SoulsComponent.Height + 6);

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

        protected override void Update(GameTime gameTime)
        {
            if (IsGameOver())
            {
                OnGameOver();
            }
            else
            {
                base.Update(gameTime);
                m_MotherShipRandomNotifier.Update(gameTime);

                m_Enemies.Update(gameTime); // TODO: i dont want to see this in the code .......
                m_Barriers.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }

        private bool IsGameOver()
        { // TODO: on disposed ?
            //return (m_Player1.SoulsComponent.NumberOfSouls == 0 && m_Player2.SoulsComponent.NumberOfSouls == 0)
            //    || m_Enemies.ReachedHeight(m_Player1.Bounds.Top);
            // DEBUG ONLY
            if (m_Player1.SoulsComponent.NumberOfSouls == 0 && m_Player2.SoulsComponent.NumberOfSouls == 0
                || m_Enemies.ReachedHeight(m_Player1.Bounds.Top))
            {
                return true;
            }

            return false;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_SpriteBatch.Begin();

            base.Draw(gameTime);

            m_SpriteBatch.End();
        }

        private void notifier_GoMotherSpaceship()
        {
            if (m_MotherSpaceship.IsOutOfSight())
            {
                m_MotherSpaceship.SetInitialValues();
            }
        }
        // TODO: logic of game over
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
