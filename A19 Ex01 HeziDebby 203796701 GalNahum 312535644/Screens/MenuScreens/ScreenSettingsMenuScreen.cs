using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects;
using Infrastructure.Managers;
using Infrastructure.ObjectModel;
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
            Background background = new Background(i_Game, this);
        }

        public override void Initialize()
        {
            createMouseVisibilityMenuItem(this.Game.IsMouseVisible);
            createWindowResizingMenuItem(this.Game.Window.AllowUserResizing);
            createFullScreenMenuItem((this.ScreensManager as ScreensMananger).GraphicsDeviceManager.IsFullScreen);
            createDoneMenuItem();

            base.Initialize();
        }

        private void createMouseVisibilityMenuItem(bool i_IsMouseVisible)
        {
            MultipleSelectionMenuItem multipleSelectionCreatedItem;
            multipleSelectionCreatedItem = new MultipleSelectionMenuItem(Game, this, "Mouse Visibility");
            multipleSelectionCreatedItem.AddOption("Visible", () => { this.Game.IsMouseVisible = true; }, i_IsMouseVisible);
            multipleSelectionCreatedItem.AddOption("Not Visible", () => { this.Game.IsMouseVisible = false; }, !i_IsMouseVisible);

            AddItem(multipleSelectionCreatedItem, true);
        }

        private void createWindowResizingMenuItem(bool i_AllowUserResizing)
        {
            MultipleSelectionMenuItem multipleSelectionCreatedItem;

            multipleSelectionCreatedItem = new MultipleSelectionMenuItem(Game, this, "Allow Window Resizing");
            multipleSelectionCreatedItem.AddOption("Allow", () => { this.Game.Window.AllowUserResizing = true; }, i_AllowUserResizing);
            multipleSelectionCreatedItem.AddOption("Do not allow", () => { this.Game.Window.AllowUserResizing = false; }, !i_AllowUserResizing);
            AddItem(multipleSelectionCreatedItem, false);
        }

        private void createFullScreenMenuItem(bool i_IsFullScreen)
        {
            MultipleSelectionMenuItem multipleSelectionCreatedItem;

            multipleSelectionCreatedItem = new MultipleSelectionMenuItem(Game, this, "Full Screen Mode");
            multipleSelectionCreatedItem.AddOption("Off", toggleFullScreen, !i_IsFullScreen);
            multipleSelectionCreatedItem.AddOption("On", toggleFullScreen, i_IsFullScreen);
            AddItem(multipleSelectionCreatedItem, false);
        }

        private void toggleFullScreen()
        {
            (this.ScreensManager as ScreensMananger).GraphicsDeviceManager.ToggleFullScreen();
        }
    }
}
