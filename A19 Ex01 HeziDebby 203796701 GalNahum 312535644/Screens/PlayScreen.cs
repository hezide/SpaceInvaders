using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects;
using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Screens;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    class PlayScreen : GameScreen
    {
        public class GameState
        {
            public bool Player1Active { get; set; }
            public bool Player2Active { get; set; }
            public List<ScoreManager> ScoreManagers { get; set; }
            
            public GameState(bool i_Player1Active, bool i_Player2Active)
            {
                Player1Active = i_Player1Active;
                Player2Active = i_Player2Active;
            }

            public void AddItemsToScreen(PlayScreen i_PlayScreen)
            {
                foreach (ScoreManager scoreManager in ScoreManagers)
                {
                    i_PlayScreen.Add(scoreManager);
                }
            }
        }

        private Spaceship m_Player1;
        private Spaceship m_Player2;
        private EnemiesGroup m_Enemies;
        private Barriers m_Barriers;
        private MotherShip m_MotherSpaceship;
        private RandomActionComponent m_MotherShipRandomNotifier;
        private SpaceInvadersSettings m_GameSettings;
        private GameState m_GameState;

        public PlayScreen(Game i_Game, GameState i_GameState = null) : base(i_Game)
        {
            Background background = new Background(this.Game, this);

            m_GameSettings = (this.Game.Services.GetService(typeof(IGameSettings)) as SpaceInvadersSettings);
            if(m_GameSettings == null)
            {
                throw new Exception("a setting class has not been initialized as a service");
            }
            setGameState(i_GameState, m_GameSettings);

            createPlayersAndScoreManagers();
            m_Enemies = new EnemiesGroup(this.Game, this);
            m_Barriers = new Barriers(this.Game,this);
            m_MotherSpaceship = new MotherShip(this.Game, this);
            m_MotherShipRandomNotifier = new RandomActionComponent();
            m_MotherShipRandomNotifier.RandomTimeAchieved += motherShipRandomNotifier_GoMotherSpaceship;
        }

        private void setGameState(GameState i_GameState, SpaceInvadersSettings i_SpaceInvadersSettings)
        {
            if (i_GameState == null)
            {
                m_GameState = new GameState(i_SpaceInvadersSettings.NumberOfPlayers > 0, i_SpaceInvadersSettings.NumberOfPlayers > 1);
                m_GameState.ScoreManagers = new List<ScoreManager>();
                if (m_GameState.Player1Active)
                {
                    m_GameState.ScoreManagers.Add(new ScoreManager("Player 1", this.Game, this) { TintColor = Color.Blue });
                }

                if (m_GameState.Player2Active)
                {
                    m_GameState.ScoreManagers.Add(new ScoreManager("Player 2", this.Game, this) { TintColor = Color.Green });
                }
            }
            else
            {
                m_GameState = i_GameState;
                m_GameState.AddItemsToScreen(this);
            }
        }

        private void createPlayersAndScoreManagers()
        {
            if(m_GameState.Player1Active)
            {
                m_Player1 = new Spaceship(@"Sprites\Ship01_32x32", this.Game, this);
            }

            if (m_GameState.Player2Active)
            {
                m_Player2 = new Spaceship(@"Sprites\Ship02_32x32", this.Game, this);
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            initPlayers();
            initScoreManagers();
            m_Enemies.Initialize();
            //todo::fix to a const, or something, 456 was m_Player1.Position.Y
            m_Barriers.Initialize(GraphicsDevice.Viewport.Width, 456);
        }

        private void initPlayers()
        {
            // *** initializing activation inputs ***
            if(m_GameState.Player1Active)
            {
                m_Player1.ActivateByMouse = true;
                m_Player1.ActivationKeysList = new List<Keys> { Keys.H, Keys.K, Keys.U };

                m_Player1.Disposed += player_Disposed;
            }

            if (m_GameState.Player2Active)
            {
                m_Player2.ActivationKeysList = new List<Keys> { Keys.A, Keys.D, Keys.W };

                // *** initializing non default players positions ***
                if(m_GameState.Player1Active)
                {
                    m_Player2.Position = new Vector2(m_Player2.Position.X + m_Player1.Width, m_Player2.Position.Y);
                }
               
                m_Player2.Disposed += player_Disposed;

                // *** initializing souls components non default positions ***
                m_Player2.SoulsComponent.Position = new Vector2(m_Player2.SoulsComponent.Position.X, m_Player2.SoulsComponent.Position.Y + m_Player2.SoulsComponent.Height + 6);
            }
        }

        private void initScoreManagers()
        {
            if(m_GameState.Player1Active)
            {
                m_Player1.AddCollisionListener(m_GameState.ScoreManagers[0].CollisionHandler);
                m_GameState.ScoreManagers[0].Position = new Vector2(m_GameState.ScoreManagers[0].Position.X + 5, m_GameState.ScoreManagers[0].Position.Y + 3);

            }
            if (m_GameState.Player2Active)
            {
                m_Player2.AddCollisionListener(m_GameState.ScoreManagers[1].CollisionHandler);
                m_GameState.ScoreManagers[1].Position = new Vector2(m_GameState.ScoreManagers[0].Position.X, m_GameState.ScoreManagers[0].Bounds.Height);
            }
        }

        private bool isGameOver()
        {
            //todo:: fix to const or something, 456 was m_Player1.Bounds.Top
            return (
                !m_GameState.Player1Active && !m_GameState.Player2Active) || 
                m_Enemies.ReachedHeight(456);
        }

        private bool isLevelCompleted()
        {
            return m_Enemies.Enemies.Count == 0;
        }

        public override void Update(GameTime i_GameTime)
        {
            if (isGameOver())
            {
                onGameOver();
            }
            else if (isLevelCompleted())
            {
                onLevelCompleted();
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
                //todo:: what to do on esc now? Game over or welcome screen?
                this.Game.Exit();
            }
            if (InputManager.KeyPressed(Keys.P))
            {
                ScreensManager.SetCurrentScreen(new PauseScreen(this.Game));
            }
            //DEBUG - change level by pressing T
            if (InputManager.KeyPressed(Keys.T))
            {
                ExitScreen();
                m_GameSettings.GoToNextLevel();
                ScreensManager.SetCurrentScreen(new PlayScreen(Game, m_GameState));
                ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game, m_GameSettings.CurrentLevel));
            }
        }

        private void onLevelCompleted()
        {
            ExitScreen();
            m_GameSettings.GoToNextLevel();
            ScreensManager.SetCurrentScreen(new PlayScreen(Game, m_GameState));
            ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game, m_GameSettings.CurrentLevel));
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
            if (PreviousScreen as GameOverScreen != null)
            {
                (PreviousScreen as GameOverScreen).SetScore(m_GameState.ScoreManagers);
            }
            ExitScreen();
        }

        private void player_Disposed(object i_Sender, EventArgs i_EventArgs)
        {
            if(i_Sender.Equals(m_Player1))
            {
                m_GameState.Player1Active = false;
            }
            else if(i_Sender.Equals(m_Player2))
            {
                m_GameState.Player2Active = false;
            }
        }
    }
}
