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
    public class ActionMenuItem : MenuItem, IActionMenuItem
    {
        private Action MenuAction;

        public ActionMenuItem(Game i_Game, GameScreen i_Screen, string i_ItemTitle, Action i_MenuAction) : base(i_Game, i_Screen, i_ItemTitle)
        {
            MenuAction = i_MenuAction;
        }

        public void InvokeAction()
        {
            MenuAction.Invoke();
        }
    }
}