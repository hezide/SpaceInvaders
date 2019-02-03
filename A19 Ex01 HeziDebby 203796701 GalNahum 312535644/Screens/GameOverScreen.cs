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
        private TextComponent m_ScoreText;

        public GameOverScreen(Game i_Game) : base(i_Game)
        {
            Background background = new Background(this.Game, this);
            m_ScoreText = new TextComponent(i_Game, this);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Content.Text = "GAME OVER";
            m_Content.Position = CenterOfViewPort;
            m_ScoreText.Position = new Vector2(m_Content.Bounds.X, m_Content.Bounds.Y + m_Content.Bounds.Height);
        }

        protected override void SetScreenActivationKeys()
        {
            m_ActivationKeys.Add(Keys.Escape, new NamedAction("Exit", Game.Exit));
            m_ActivationKeys.Add(Keys.Home, new NamedAction("New Game", newGame));
            m_ActivationKeys.Add(Keys.T, new NamedAction("Go to Main Menu", goToMainMenu));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        private void goToMainMenu()
        {
            m_ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
        }

        private void newGame()
        {
            (m_ScreensManager as ScreensMananger).Push(new PlayScreen(Game));
            m_ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game, 0));
        }

        public void SetScore(List<ScoreManager> i_ScoreManagers)
        {
            string finalScore = string.Format(@"{0}
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
