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
        private TextComponent m_WelcomeMessage;
        public WelcomeScreen(Game i_Game):base(i_Game)
        {
            Background background = new Background(this.Game, this);
            m_WelcomeMessage = new TextComponent(@"Fonts\Comic Sans MS", i_Game, this);
        }
        public override void Initialize()
        {
            base.Initialize();
            
            m_WelcomeMessage.Text = "Welcome";
            m_WelcomeMessage.Position = CenterOfViewPort;
        }

        public override void Update(GameTime i_GameTime)
        {
            if (InputManager.KeyPressed(Keys.Enter))
            {
                ExitScreen();
                ScreensManager.SetCurrentScreen(new PlayScreen(Game));
                ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game,0));
            }
            else if (InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }
            else if (InputManager.KeyPressed(Keys.T))
            {
                ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
            }
        }
        public override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            base.Draw(i_GameTime);
        }

    }
}
