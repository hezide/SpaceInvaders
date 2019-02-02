using Infrastructure.ObjectModel.MenuItems;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Screens
{
    class MainMenuScreen : MenuGameScreen
    {
        public MainMenuScreen(Game i_Game) : base(i_Game, "Main Menu")
        {
        }

        public override void Initialize()
        {
            createNumOfPlayersMenuItem((this.Game.Services.GetService(typeof(IGameSettings)) as SpaceInvadersSettings).NumberOfPlayers);
            createScreenSettingsMenuItem();
            createSoundSettingsMenuItem();
            createPlayMenuItem();
            createQuitMenuItem();
            base.Initialize();
        }

        private void createNumOfPlayersMenuItem(int i_NumberOfPlayers)
        {
            MultipleSelectionMenuItem multipleSelectionCreatedItem;

            multipleSelectionCreatedItem = new MultipleSelectionMenuItem(Game, this, "Number Of Players");
            multipleSelectionCreatedItem.AddOption("One", () => { setNumOfPlayers(1); }, i_NumberOfPlayers == 1);
            multipleSelectionCreatedItem.AddOption("Two", () => { setNumOfPlayers(2); }, i_NumberOfPlayers == 2);
            AddItem(multipleSelectionCreatedItem, true);
        }

        private void setNumOfPlayers(int i_NumOfPlayers)
        {
            (this.Game.Services.GetService(typeof(IGameSettings)) as SpaceInvadersSettings).NumberOfPlayers = i_NumOfPlayers;
        }

        private void createScreenSettingsMenuItem()
        {
            ActionMenuItem actionCreatedItem = new ActionMenuItem(Game, this, "Screen Settings", goToScreenSettings);
            AddItem(actionCreatedItem, false);
        }

        private void goToScreenSettings()
        {
            ScreensManager.SetCurrentScreen(new ScreenSettingsMenuScreen(Game));
        }

        private void createSoundSettingsMenuItem()
        {
            ActionMenuItem actionCreatedItem = new ActionMenuItem(Game, this, "Sound Settings", goToSoundSettings);
            AddItem(actionCreatedItem, false);
        }

        private void goToSoundSettings()
        {
            ScreensManager.SetCurrentScreen(new SoundSettingsMenuScreen(Game));
        }

        private void createPlayMenuItem()
        {
            ActionMenuItem actionCreatedItem = new ActionMenuItem(Game, this, "Play", play);
            AddItem(actionCreatedItem, false);
        }

        private void play()
        {
            ExitScreen();
            ScreensManager.SetCurrentScreen(new PlayScreen(Game));
            ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game, 0));
        }

        private void createQuitMenuItem()
        {
            ActionMenuItem actionCreatedItem = new ActionMenuItem(Game, this, "Quit", Game.Exit);
            AddItem(actionCreatedItem, false);
        }
    }
}
