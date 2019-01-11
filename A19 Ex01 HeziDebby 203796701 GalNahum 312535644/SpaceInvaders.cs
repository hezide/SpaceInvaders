using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class SpaceInvaders : Game
    {
        readonly GraphicsDeviceManager m_Graphics;
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
        private int m_ActivePlayers;

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
        }

        protected override void Initialize()
        {
            base.Initialize();

            Window.Title = "Space Invaders";
            IsMouseVisible = true;

            // *** initializing activation inputs ***
            m_Player1.ActivateByMouse = true;
            m_Player1.ActivationKeysList = new List<Keys> { Keys.H, Keys.K, Keys.U };
            m_Player2.ActivationKeysList = new List<Keys> { Keys.A, Keys.D, Keys.W };
            
            // *** initializing non default players positions ***
            m_Player2.Position = new Vector2(m_Player1.Position.X + m_Player1.Width, m_Player2.Position.Y);

            m_Player1.Disposed += onPlayerDisposed;
            m_Player2.Disposed += onPlayerDisposed;
            m_ActivePlayers = 2;
            // *** initializing souls components non default positions ***
            m_Player2.SoulsComponent.Position = new Vector2(m_Player2.SoulsComponent.Position.X, m_Player2.SoulsComponent.Position.Y + m_Player2.SoulsComponent.Height + 6);

            initScoreManagers();

            m_Enemies.Initialize();

            m_Barriers.Initialize(GraphicsDevice.Viewport.Width, m_Player1.Position.Y);
        }

        private void initScoreManagers()
        {
            m_Player1.AddCollisionListener(m_ScoreManagers[0].CollisionHandler);
            m_Player2.AddCollisionListener(m_ScoreManagers[1].CollisionHandler);

            m_ScoreManagers[0].Position = new Vector2(m_ScoreManagers[0].Position.X + 5, m_ScoreManagers[0].Position.Y + 3);
            m_ScoreManagers[1].Position = new Vector2(m_ScoreManagers[0].Position.X, m_ScoreManagers[0].Bounds.Height);
        }

        protected override void Update(GameTime gameTime)
        {
            if (isGameOver())
            {
                onGameOver();
            }
            else
            {
                base.Update(gameTime);
                m_MotherShipRandomNotifier.Update(gameTime);
                m_Enemies.Update(gameTime);
                m_Barriers.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }

        private bool isGameOver()
        {
            return (m_ActivePlayers == 0)
                || m_Enemies.ReachedHeight(m_Player1.Bounds.Top);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        private void notifier_GoMotherSpaceship()
        {
            if (m_MotherSpaceship.IsOutOfSight())
            {
                m_MotherSpaceship.SetInitialValues();
            }
        }

        private void onGameOver()
        {
            string finalScore = string.Format(@"
{0}
{1}
The winner is: {2}", m_ScoreManagers[0].TextToString(), m_ScoreManagers[1].TextToString(), getWinner());
            System.Windows.Forms.MessageBox.Show(finalScore, "Game Over", System.Windows.Forms.MessageBoxButtons.OK);
            Exit();
        }

        private string getWinner()
        {
            string theWinner = "Tie!";

            if (m_ScoreManagers[0].Score > m_ScoreManagers[1].Score)
            {
                theWinner = m_ScoreManagers[0].Name;
            }
            else if (m_ScoreManagers[0].Score < m_ScoreManagers[1].Score)
            {
                theWinner = m_ScoreManagers[1].Name;
            }

            return theWinner;
        }

        private void onPlayerDisposed(object i_Sender, EventArgs i_EventArgs)
        {
            m_ActivePlayers--;
        }
    }
}
