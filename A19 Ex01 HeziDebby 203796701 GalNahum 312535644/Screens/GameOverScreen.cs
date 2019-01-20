using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects;
using Infrastructure.Managers;
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

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Screens
{
    class GameOverScreen : GameScreen
    {
        private IInputManager m_InputManager;
        private TextComponent m_GameOverText;
        private TextComponent m_ScoreText;

        public GameOverScreen(Game i_Game) : base(i_Game)
        {
            Background background = new Background(this.Game, this);
            m_GameOverText = new TextComponent(@"Fonts\Comic Sans MS", i_Game, this);
            m_ScoreText = new TextComponent(@"Fonts\Comic Sans MS", i_Game, this);

        }
        public override void Initialize()
        {
            base.Initialize();
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            m_GameOverText.Text = "GAME OVER";
            m_GameOverText.Position = CenterOfViewPort;

        }
        public override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(i_GameTime);
        }
        public override void Update(GameTime gameTime)
        {
            if (m_InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }
            else if (m_InputManager.KeyPressed(Keys.Home))
            {
                (m_ScreensManager as ScreensMananger).Push(new PlayScreen(Game));
                m_ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game,0));
            }
            else if (m_InputManager.KeyPressed(Keys.T))
            {
                //todo:: show main menu
            }
            base.Update(gameTime);
        }

        public void SetScore(List<ScoreManager> i_ScoreManagers)
        {
            string finalScore = string.Format(@"
            {0}
            {1}
            {2}", i_ScoreManagers[0].TextToString(), i_ScoreManagers[1].TextToString(), getWinner(i_ScoreManagers));
            m_ScoreText.Text = finalScore;
        }

        private string getWinner(List<ScoreManager> i_ScoreManagers)
        {
            string theWinner = "It's a Tie!";
            const string winnerText = "The Winner is: ";
            if (i_ScoreManagers[0].Score > i_ScoreManagers[1].Score)
            {
                theWinner = winnerText + i_ScoreManagers[0].Name;
            }
            else if (i_ScoreManagers[0].Score < i_ScoreManagers[1].Score)
            {
                theWinner = winnerText + i_ScoreManagers[1].Name;
            }

            return theWinner;
        }
    }
}
