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
        private IInputManager m_InputManager;
        public WelcomeScreen(Game i_Game):base(i_Game)
        {
            Background background = new Background(this.Game, this);
            m_WelcomeMessage = new TextComponent(@"Fonts\Comic Sans MS", i_Game, this);
        }
        public override void Initialize()
        {
            base.Initialize();
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;

            m_WelcomeMessage.Text = "Welcome";
            m_WelcomeMessage.Position = CenterOfViewPort;
        }

        public override void Update(GameTime i_GameTime)
        {
            if (m_InputManager.KeyPressed(Keys.Enter))
            {
                ExitScreen();
            }
            else if (m_InputManager.KeyPressed(Keys.Escape))
            {
                Game.Exit();
            }
            else if (m_InputManager.KeyPressed(Keys.T))
            {
                //todo:: show main menu
            }
        }
        public override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(i_GameTime);
        }

    }
}
