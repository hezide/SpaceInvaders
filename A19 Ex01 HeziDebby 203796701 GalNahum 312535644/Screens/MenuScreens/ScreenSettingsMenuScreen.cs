using Infrastructure.Managers;
using Infrastructure.ObjectModel.MenuItems;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Screens
{
    class ScreenSettingsMenuScreen : MenuGameScreen
    {
        public ScreenSettingsMenuScreen(Game i_Game) : base(i_Game, "Screen Settings")
        {
        }
        public override void Initialize()
        {
            createMouseVisibilityMenuItem(this.Game.IsMouseVisible);
            createWindowResizingMenuItem(this.Game.Window.AllowUserResizing);
            createFullScreenMenuItem((this.ScreensManager as ScreensMananger).GraphicsDeviceManager.IsFullScreen);
            createDoneMenuItem();

            base.Initialize();
        }

        private void createFullScreenMenuItem(bool i_IsFullScreen)
        {
            MultipleSelectionMenuItem multipleSelectionCreatedItem;

            multipleSelectionCreatedItem = new MultipleSelectionMenuItem(Game, this, "Full Screen Mode");
            multipleSelectionCreatedItem.AddOption("Off", fullScreenOff, !i_IsFullScreen);
            multipleSelectionCreatedItem.AddOption("On", fullScreenOn, i_IsFullScreen);
            AddItem(multipleSelectionCreatedItem, false);
        }

        private void createWindowResizingMenuItem(bool i_AllowUserResizing)
        {
            MultipleSelectionMenuItem multipleSelectionCreatedItem;

            multipleSelectionCreatedItem = new MultipleSelectionMenuItem(Game, this, "Allow Window Resizing");
            multipleSelectionCreatedItem.AddOption("Allow", allowWindowResizing, i_AllowUserResizing);
            multipleSelectionCreatedItem.AddOption("Do not allow", disallowWindowResizing, !i_AllowUserResizing);
            AddItem(multipleSelectionCreatedItem, false);
        }

        private void createMouseVisibilityMenuItem(bool i_IsMouseVisible)
        {
            MultipleSelectionMenuItem multipleSelectionCreatedItem;
            multipleSelectionCreatedItem = new MultipleSelectionMenuItem(Game, this, "Mouse Visibility");
            multipleSelectionCreatedItem.AddOption("Visible", mouseVisibleOn, i_IsMouseVisible);
            multipleSelectionCreatedItem.AddOption("Not Visible", mouseVisibleOff, !i_IsMouseVisible);

            AddItem(multipleSelectionCreatedItem, true);
        }

        private void fullScreenOff()
        {
            if((this.ScreensManager as ScreensMananger).GraphicsDeviceManager.IsFullScreen == true)
            {
                (this.ScreensManager as ScreensMananger).GraphicsDeviceManager.ToggleFullScreen();
            }
        }

        private void fullScreenOn()
        {
            if ((this.ScreensManager as ScreensMananger).GraphicsDeviceManager.IsFullScreen == false)
            {
                (this.ScreensManager as ScreensMananger).GraphicsDeviceManager.ToggleFullScreen();
            }
        }

        private void disallowWindowResizing()
        {
            this.Game.Window.AllowUserResizing = false;
        }

        private void allowWindowResizing()
        {
            this.Game.Window.AllowUserResizing = true;
        }

        private void mouseVisibleOn()
        {
            this.Game.IsMouseVisible = true;
        }
        private void mouseVisibleOff()
        {
            this.Game.IsMouseVisible = false;
        }

        public override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(i_GameTime);
        }
    }
}
