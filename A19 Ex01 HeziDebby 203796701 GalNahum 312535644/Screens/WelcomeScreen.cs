using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects;
using Infrastructure.ObjectModel.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework.Input;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Screens
{
    class WelcomeScreen : GameScreen
    {
        public WelcomeScreen(Game i_Game) : base(i_Game)
        {
            Background background = new Background(this.Game, this);
        }

        public override void Initialize()
        {
            base.Initialize();

            m_Content.Text = "Welcome to Space Invaders !";
            m_Content.Position = new Vector2(CenterOfViewPort.X - m_Content.Bounds.Width / 2, CenterOfViewPort.Y - 20);
            m_Instructions.Position = new Vector2(m_Content.Bounds.X, m_Content.Bounds.Y + m_Content.Bounds.Height);
        }

        protected override void SetScreenActivationKeys()
        {
            m_ActivationKeys.Add(Keys.Enter, new NamedAction("Play", goToPlayScreen));
            m_ActivationKeys.Add(Keys.Escape, new NamedAction("Exit", Game.Exit));
            m_ActivationKeys.Add(Keys.T, new NamedAction("Go to Main Menu", goToMainMenuScreen));
        }

        private void goToMainMenuScreen()
        {
            ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
        }

        private void goToPlayScreen()
        {
            ExitScreen();
            ScreensManager.SetCurrentScreen(new PlayScreen(Game));
            ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game, 0));
        }
    }
}
