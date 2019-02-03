using Infrastructure.MenuInterfaces;
using Infrastructure.ObjectModel.MenuItems;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Infrastructure.ObjectModel.Screens
{
    public abstract class MenuGameScreen : GameScreen
    {
        protected OptionSelectionComponent<IMenuItem> m_MenuItemSelectionComponent = new OptionSelectionComponent<IMenuItem>();
        private SoundEffect m_SwitchItemsSoundEffect;

        public MenuGameScreen(Game i_Game, string i_Headline) : base(i_Game)
        {
            m_Content.Text = i_Headline;
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
            m_Content.Position = new Vector2(CenterOfViewPort.X - m_Content.Bounds.Width / 2, CenterOfViewPort.Y - 50);

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
            Vector2 initialPosition = new Vector2(m_Content.Position.X, m_Content.Position.Y + m_Content.Bounds.Height);

            foreach (IMenuItem item in m_MenuItemSelectionComponent)
            {
                item.SetPosition(initialPosition + new Vector2(0, 25) * i);
                i++;
            }
        }

        protected override void SetScreenActivationKeys()
        {
            //it this ok in coding standards? (function names
            m_ActivationKeys = new Dictionary<Keys, NamedAction>
            {
                { Keys.Enter, new NamedAction("OK", moveToOtherScreen) },
                { Keys.PageUp, new NamedAction("Option up", changeMenuItemOptionsUp) },
                { Keys.PageDown, new NamedAction("Option Down", changeMenuItemOptionsDown) },
                { Keys.Up, new NamedAction("Next", moveToPreviousMenuItem) },
                { Keys.Down, new NamedAction("Previous", moveToNextMenuItem) }
            };
        }

        public override void Update(GameTime i_GameTime)
        {
            mouseUpdatesHandler(i_GameTime);
            base.Update(i_GameTime);
        }

        private void mouseUpdatesHandler(GameTime i_GameTime)
        {
            if (InputManager.ScrollWheelDelta > 0 || InputManager.ButtonPressed(eInputButtons.Right))
            {
                changeMenuItemOptionsDown();
            }

            else if (InputManager.ScrollWheelDelta < 0)
            {
                changeMenuItemOptionsUp();
            }

            foreach (IMenuItem menuItem in m_MenuItemSelectionComponent)
            {
                if (InputManager.MouseBounds().Intersects(menuItem.Bounds()))
                {
                    if (menuItem != m_MenuItemSelectionComponent.ActiveItem)//prevents reactivating an active item
                    {
                        m_MenuItemSelectionComponent.ActiveItem.SetActive(false);
                        m_MenuItemSelectionComponent.SetActiveItem(menuItem);
                        menuItem.SetActive(true);
                        m_SwitchItemsSoundEffect.Play();
                    }
                }
            }

            if (InputManager.ButtonPressed(eInputButtons.Left))
            {
                moveToOtherScreen();
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
        protected override void LoadContent()
        {
            base.LoadContent();
            m_SwitchItemsSoundEffect = this.Game.Content.Load<SoundEffect>("Sounds/MenuMove");
        }
        //***************************************************//
        //          iTEM COLLECTION FUNCTIONS                //
        //***************************************************//
        private void moveToNextMenuItem()
        {
            m_MenuItemSelectionComponent.ActiveItem.SetActive(false);
            m_MenuItemSelectionComponent.MoveToNextOption().SetActive(true);
            m_SwitchItemsSoundEffect.Play();
        }

        private void moveToPreviousMenuItem()
        {
            m_MenuItemSelectionComponent.ActiveItem.SetActive(false);
            m_MenuItemSelectionComponent.MoveToPrevOption().SetActive(true);
            m_SwitchItemsSoundEffect.Play();
        }

        protected void AddItem(IMenuItem i_ItemToAdd, bool i_IsActive)
        {
            m_MenuItemSelectionComponent.AddItem(i_ItemToAdd, i_IsActive);
        }
    }
}
