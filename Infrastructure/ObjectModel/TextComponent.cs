using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectModel
{// TODO: add interface for polymorphizem
    public class TextComponent : Sprite
    {
        private const string k_DefaultFont = @"Fonts\Comic Sans MS";
        private SpriteFont m_SpriteFont;
        private string m_FontName;
        public string Text { get; set; } = string.Empty;

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)m_SpriteFont.MeasureString(TextToString()).X,
                (int)m_SpriteFont.MeasureString(TextToString()).Y);
            }
        }

        public TextComponent(Game i_Game, GameScreen i_Screen)
           : base(String.Empty, i_Game, i_Screen, int.MaxValue)
        {
            m_FontName = k_DefaultFont;
            Position = Vector2.Zero;
            m_TintColor = Color.White;
        }

        public TextComponent(string i_AssetName, Game i_Game, GameScreen i_Screen)
            : base(String.Empty, i_Game, i_Screen,int.MaxValue)
        {
            m_FontName = i_AssetName;
        }

        public TextComponent(string i_AssetName, Game i_Game, GameScreen i_Screen, int i_UpdateOrder, int i_DrawOrder)
            : base(String.Empty, i_Game, i_Screen, i_UpdateOrder, i_DrawOrder)
        {
            m_FontName = i_AssetName;
        }

        public TextComponent(string i_AssetName, string i_Name, Game i_Game, GameScreen i_Screen, int i_CallsOrder)
            : base(String.Empty, i_Game, i_Screen, i_CallsOrder)
        {
            m_FontName = i_AssetName;
        }

        protected override void LoadContent()
        {
            m_SpriteFont = Game.Content.Load<SpriteFont>(m_FontName);
            base.LoadContent();
        }

        public override void Draw(GameTime i_GameTime)
        {
            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.Begin();
            }

            m_SpriteBatch.DrawString(m_SpriteFont, TextToString(), this.Position, this.m_TintColor,0,this.m_PositionOrigin, this.Scales, SpriteEffects.None, 0);

            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.End();
            }

            base.Draw(i_GameTime);
        }

        protected override void InitBounds()
        {}

        protected override void DrawBoundingBox()
        {}

        public virtual string TextToString()
        {
            return Text;
        }
    }
}
