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
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Content.Text = "Game Paused.";

            m_Content.Position = new Vector2(CenterOfViewPort.X - m_Content.Bounds.Width / 2, CenterOfViewPort.Y - 50); 
            m_Instructions.Position = new Vector2(m_Content.Bounds.X, m_Content.Bounds.Y + m_Content.Bounds.Height);
        }

        protected override void SetScreenActivationKeys()
        {
            m_ActivationKeys.Add(Keys.R, new NamedAction("Continue", ExitScreen));
        }
    }
}
