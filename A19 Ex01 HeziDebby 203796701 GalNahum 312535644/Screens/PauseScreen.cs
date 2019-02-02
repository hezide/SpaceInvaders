using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Screens
{
    class PauseScreen : GameScreen
    {
        private TextComponent m_PausedMessage;

        public PauseScreen(Game i_Game):base(i_Game)
        {
            this.IsModal = true;
            this.IsOverlayed = true;
            this.UseGradientBackground = false;
            //todo:: is this really 40%?
            this.BlackTintAlpha = 0.6f;
            this.UseFadeTransition = true;

            this.ActivationLength = TimeSpan.FromSeconds(0.5f);
            this.DeactivationLength = TimeSpan.FromSeconds(0.5f);

            m_PausedMessage = new TextComponent(@"Fonts\Comic Sans MS", i_Game, this);
        }
        public override void Initialize()
        {
            m_PausedMessage.Text = @"
Game is paused.
Press 'R' to continue.";
            m_PausedMessage.Position = CenterOfViewPort;
            base.Initialize();
        }
        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (InputManager.KeyPressed(Keys.R))
            {
                ExitScreen();
            }
        }
    }
}
