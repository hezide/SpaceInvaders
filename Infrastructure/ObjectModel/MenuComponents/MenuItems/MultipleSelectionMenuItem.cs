using Infrastructure.MenuInterfaces;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectModel.MenuItems
{
    public class MultipleSelectionMenuItem : MenuItem, IMultipleSelectionMenuItem
    {
        private OptionSelectionComponent<KeyValuePair<string, Action>> m_MenuItemSelectionComponent = new OptionSelectionComponent<KeyValuePair<string, Action>>();

        public MultipleSelectionMenuItem(Game i_Game, GameScreen i_Screen, string i_ItemTitle) : base(i_Game, i_Screen, i_ItemTitle)
        {
        }

        public void MoveDown()
        {
            KeyValuePair<string, Action> nextOption = m_MenuItemSelectionComponent.MoveToNextOption();
            activateOption(nextOption);
        }

        public void MoveUp()
        {
            KeyValuePair<string, Action> prevOption = m_MenuItemSelectionComponent.MoveToPrevOption();
            activateOption(prevOption);
        }

        public void AddOption(string i_OptionTitle, Action i_OptionAction,bool i_IsActive)
        {
            KeyValuePair<string, Action> itemToAdd = new KeyValuePair<string, Action>(i_OptionTitle, i_OptionAction);

            if (i_IsActive)
            {
                m_ItemText.Text = itemToAdd.Key;
            }

            m_MenuItemSelectionComponent.AddItem(itemToAdd, i_IsActive);
        } 

        private void activateOption(KeyValuePair<string, Action> i_Option)
        {
            m_ItemText.Text = i_Option.Key;
            i_Option.Value.Invoke();
        }
    }
}
