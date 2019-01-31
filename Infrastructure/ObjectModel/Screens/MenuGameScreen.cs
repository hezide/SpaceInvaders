using Infrastructure.MenuInterfaces;
using Infrastructure.ObjectModel.MenuItems;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectModel.Screens
{
    public abstract class MenuGameScreen : GameScreen
    {
        private TextComponent m_HeadlineMessage;
        private Dictionary<Keys, Action> m_ActivationKeys;
        protected OptionSelectionComponent<IMenuItem> m_MenuItemSelectionComponent = new OptionSelectionComponent<IMenuItem>();
        private MouseState m_PreviousMouseState;

        private readonly Vector2 r_FirstItemPosition = new Vector2(0, 50);//change when doing visual adjustments
        
        public MenuGameScreen(Game i_Game,string i_Headline) : base(i_Game)
        {
            m_HeadlineMessage = new TextComponent(@"Fonts\Comic Sans MS", i_Game, this);
            m_HeadlineMessage.Text = i_Headline;
            initKeys();
        }
        protected void createDoneMenuItem()
        {
            ActionMenuItem actionCreatedItem = new ActionMenuItem(Game, this, "Done", backToPreviousScreen);
            AddItem(actionCreatedItem, false);
        }
        private void backToPreviousScreen()
        {
            ExitScreen();
        }
        public override void Initialize()        
        {
            base.Initialize();
            foreach (IMenuItem menuItem in m_MenuItemSelectionComponent)
            {
                menuItem.Initialize();
            }
            //setting the first menu item to be active on initialization
            if (m_MenuItemSelectionComponent.ActiveItem != null)
            {
                m_MenuItemSelectionComponent.ActiveItem.SetActive(true);
            }
            
            //init positions
            int i = 0;
            foreach (IMenuItem item in m_MenuItemSelectionComponent)
            {
                item.SetPosition(r_FirstItemPosition + new Vector2(0, 20) * i);
                i++;
            }
        }

        private void initKeys()
        {
            //it this ok in coding standards? (function names
            m_ActivationKeys = new Dictionary<Keys, Action>();
            m_ActivationKeys.Add(Keys.Enter, moveToOtherScreen);
            m_ActivationKeys.Add(Keys.PageUp, changeMenuItemOptionsUp);
            m_ActivationKeys.Add(Keys.PageDown, changeMenuItemOptionsDown);
            m_ActivationKeys.Add(Keys.Up, moveToPreviousMenuItem);
            m_ActivationKeys.Add(Keys.Down, moveToNextMenuItem);
        }

        public override void Update(GameTime i_GameTime)
        {
            mouseUpdatesHandler(i_GameTime);

            foreach (KeyValuePair<Keys, Action> keyAndAction in m_ActivationKeys)
            {
                if(InputManager.KeyPressed(keyAndAction.Key))
                {
                    keyAndAction.Value.Invoke();
                }
            }

            base.Update(i_GameTime);
        }

        private void mouseUpdatesHandler(GameTime i_GameTime)
        {            
            if (InputManager.ScrollWheelDelta > 0)
            {
                changeMenuItemOptionsDown();
            }
            else if (InputManager.ScrollWheelDelta < 0)
            {
                changeMenuItemOptionsUp();
            }
        }

        private void moveToOtherScreen()
        {
            if (m_MenuItemSelectionComponent.ActiveItem is IActionMenuItem)
            {
                (m_MenuItemSelectionComponent.ActiveItem as IActionMenuItem).InvokeAction();
            }
        }

        private void changeMenuItemOptionsUp()
        {
            if (m_MenuItemSelectionComponent.ActiveItem is IMultipleSelectionMenuItem)
            {
                (m_MenuItemSelectionComponent.ActiveItem as IMultipleSelectionMenuItem).MoveUp();
            }
        }

        private void changeMenuItemOptionsDown()
        {
            if (m_MenuItemSelectionComponent.ActiveItem is IMultipleSelectionMenuItem)
            {
                (m_MenuItemSelectionComponent.ActiveItem as IMultipleSelectionMenuItem).MoveDown();
            }
        }

        //***************************************************//
        //          iTEM COLLECTION FUNCTIONS                //
        //***************************************************//
        private void moveToNextMenuItem()
        {
            m_MenuItemSelectionComponent.ActiveItem.SetActive(false);
            m_MenuItemSelectionComponent.MoveToNextOption().SetActive(true);
        }

        private void moveToPreviousMenuItem()
        {
            m_MenuItemSelectionComponent.ActiveItem.SetActive(false);
            m_MenuItemSelectionComponent.MoveToPrevOption().SetActive(true);
        }

        protected void AddItem(IMenuItem i_ItemToAdd,bool i_IsActive)
        {
            m_MenuItemSelectionComponent.AddItem(i_ItemToAdd, i_IsActive);
        }
    }
}
