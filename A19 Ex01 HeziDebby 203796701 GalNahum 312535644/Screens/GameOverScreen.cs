using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects;
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
        private TextComponent m_GameOverText;

        public GameOverScreen(Game i_Game) : base(i_Game)
        {
            Background background = new Background(this.Game, this);
            m_GameOverText = new TextComponent(@"Fonts\Comic Sans MS", i_Game, this);
        }
        public override void Initialize()
        {
            base.Initialize();

            m_GameOverText.Text = "GAME OVER";
            m_GameOverText.Position = CenterOfViewPort;

        }
        public override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(i_GameTime);
        }
    }
}
