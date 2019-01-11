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
        readonly GraphicsDeviceManager r_Graphics;
        private readonly Background r_Background;
        private readonly InputManager r_InputManager;
        private readonly CollisionsManager r_CollisionsManager;
        private Spaceship m_Player1;
        private Spaceship m_Player2;
        private EnemiesGroup m_Enemies;
        private Barriers m_Barriers;
        private MotherShip m_MotherSpaceship;
        private RandomActionComponent m_MotherShipRandomNotifier;
        private readonly List<ScoreManager> r_ScoreManagers;
        private int m_ActivePlayers;

        public SpaceInvaders()
        {
            r_Graphics = new GraphicsDeviceManager(this);
            r_Background = new Background(this);
            m_Player1 = new Spaceship(@"Sprites\Ship01_32x32", this);
            m_Player2 = new Spaceship(@"Sprites\Ship02_32x32", this);
            r_InputManager = new InputManager(this);
            r_CollisionsManager = new CollisionsManager(this);

            m_Enemies = new EnemiesGroup(this);
            m_Barriers = new Barriers(this);
            m_MotherSpaceship = new MotherShip(this);
            m_MotherShipRandomNotifier = new RandomActionComponent();
            m_MotherShipRandomNotifier.RandomTimeAchieved += motherShipRandomNotifier_GoMotherSpaceship;

            r_ScoreManagers = new List<ScoreManager>
            {
                { new ScoreManager("Player 1", this) { TintColor = Color.Blue} },
                { new ScoreManager("Player 2", this) { TintColor = Color.Green} }
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

            m_Player1.Disposed += player_Disposed;
            m_Player2.Disposed += player_Disposed;
            m_ActivePlayers = 2;
            // *** initializing souls components non default positions ***
            m_Player2.SoulsComponent.Position = new Vector2(m_Player2.SoulsComponent.Position.X, m_Player2.SoulsComponent.Position.Y + m_Player2.SoulsComponent.Height + 6);

            initScoreManagers();

            m_Enemies.Initialize();

            m_Barriers.Initialize(GraphicsDevice.Viewport.Width, m_Player1.Position.Y);
        }

        private void initScoreManagers()
        {
            m_Player1.AddCollisionListener(r_ScoreManagers[0].CollisionHandler);
            m_Player2.AddCollisionListener(r_ScoreManagers[1].CollisionHandler);

            r_ScoreManagers[0].Position = new Vector2(r_ScoreManagers[0].Position.X + 5, r_ScoreManagers[0].Position.Y + 3);
            r_ScoreManagers[1].Position = new Vector2(r_ScoreManagers[0].Position.X, r_ScoreManagers[0].Bounds.Height);
        }

        protected override void Update(GameTime i_GameTime)
        {
            if (isGameOver())
            {
                onGameOver();
            }
            else
            {
                base.Update(i_GameTime);
                m_MotherShipRandomNotifier.Update(i_GameTime);
                m_Enemies.Update(i_GameTime);
                m_Barriers.Update(i_GameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }

        private bool isGameOver()
        {
            return (m_ActivePlayers == 0) || m_Enemies.ReachedHeight(m_Player1.Bounds.Top) || m_Enemies.Enemies.Capacity == 0;
        }

        protected override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(i_GameTime);
        }

        private void motherShipRandomNotifier_GoMotherSpaceship()
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
The winner is: {2}", r_ScoreManagers[0].TextToString(), r_ScoreManagers[1].TextToString(), getWinner());
            System.Windows.Forms.MessageBox.Show(finalScore, "Game Over", System.Windows.Forms.MessageBoxButtons.OK);
            Exit();
        }

        private string getWinner()
        {
            string theWinner = "It's a Tie!";

            if (r_ScoreManagers[0].Score > r_ScoreManagers[1].Score)
            {
                theWinner = r_ScoreManagers[0].Name;
            }
            else if (r_ScoreManagers[0].Score < r_ScoreManagers[1].Score)
            {
                theWinner = r_ScoreManagers[1].Name;
            }

            return theWinner;
        }

        private void player_Disposed(object i_Sender, EventArgs i_EventArgs)
        {
            m_ActivePlayers--;
        }
    }
}
