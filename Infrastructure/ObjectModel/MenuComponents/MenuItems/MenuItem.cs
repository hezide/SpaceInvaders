using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectModel
{
    public abstract class MenuItem : IMenuItem
    {
        public bool Active { get; private set ;}
        protected TextComponent m_ItemTitle;
        protected TextComponent m_ItemText;

        private readonly int r_distanceBetweenTitleAndText = 10;
        private readonly Color r_ActiveColor = Color.Blue;
        private readonly Color r_InActiveColor = Color.DimGray;

        public MenuItem(Game i_Game, GameScreen i_GameScreen, string i_ItemTitle)
        {
            m_ItemText = new TextComponent(i_Game, i_GameScreen);
            m_ItemTitle = new TextComponent(i_Game, i_GameScreen)
            {
                Text = i_ItemTitle
            };
        }

        public void Initialize()
        {
            m_ItemText.Initialize();
            m_ItemTitle.Initialize();
            initAnimations();
            SetActive(false);

        }
        private void initAnimations()
        {
            m_ItemText.Animations.Add(new PulseAnimator("Pulse", TimeSpan.Zero, 1.1f, 0.4f));
            m_ItemTitle.Animations.Add(new PulseAnimator("Pulse", TimeSpan.Zero, 1.1f, 0.4f));
        }
        public Rectangle Bounds()
        {
            return new Rectangle(m_ItemTitle.Bounds.X, m_ItemTitle.Bounds.Y, (int)m_ItemTitle.Bounds.Width + (int)m_ItemText.Bounds.Width + r_distanceBetweenTitleAndText, (int)m_ItemTitle.Bounds.Height);
        }
        public void SetActive(bool i_Active)
        {
            if(i_Active == true)
            {
                m_ItemText.TintColor = r_ActiveColor;
                m_ItemTitle.TintColor = r_ActiveColor;
                m_ItemText.Animations.Enabled = true;
                m_ItemTitle.Animations.Enabled = true;
            }
            else
            {
                m_ItemText.TintColor = r_InActiveColor;
                m_ItemTitle.TintColor = r_InActiveColor;
                m_ItemText.Animations.Reset();
                m_ItemText.Animations.Enabled = false;
                m_ItemTitle.Animations.Reset();
                m_ItemTitle.Animations.Enabled = false;
            }
        }

        public void SetPosition(Vector2 i_ItemPosition)
        {
            m_ItemTitle.Position = i_ItemPosition;
            
            m_ItemText.Position = new Vector2(m_ItemTitle.Bounds.Right + r_distanceBetweenTitleAndText, m_ItemTitle.Position.Y);
        }
    }
}
