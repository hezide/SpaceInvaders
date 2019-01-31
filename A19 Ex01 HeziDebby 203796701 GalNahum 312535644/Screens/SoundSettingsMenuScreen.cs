using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Screens
{
    class SoundSettingsMenuScreen : MenuGameScreen
    {
        public SoundSettingsMenuScreen(Game i_Game) : base(i_Game, "Sound Settings"){}

        public override void Initialize()
        {
            //createMouseVisibilityMenuItem(this.Game.IsMouseVisible);
            //createWindowResizingMenuItem(this.Game.Window.AllowUserResizing);
            //createFullScreenMenuItem((this.ScreensManager as ScreensMananger).GraphicsDeviceManager.IsFullScreen);
            createDoneMenuItem();

            base.Initialize();
        }

        public override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(i_GameTime);
        }
    }
}
