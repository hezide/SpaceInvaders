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

        public PauseScreen(Game i_Game):base(i_Game)
        {
            this.IsModal = true;
            this.IsOverlayed = true;
            this.UseGradientBackground = false;
            this.BlackTintAlpha = 0.6f;
            this.UseFadeTransition = true;

            this.ActivationLength = TimeSpan.FromSeconds(0.5f);
            this.DeactivationLength = TimeSpan.FromSeconds(0.5f);
        }
        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (InputManager.KeyPressed(Keys.R))
            {
                ExitScreen();
            }
        }
        public override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(i_GameTime);
        }

    }
}
