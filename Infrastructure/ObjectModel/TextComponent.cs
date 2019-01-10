using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ObjectModel
{// TODO: add interface for polymorphizem
    public class TextComponent : LoadableDrawableComponent
    {
        protected SpriteFont m_SpriteFont;
        protected SpriteBatch m_SpriteBatch;
        protected string m_StringToDraw = string.Empty;

        protected Color m_TintColor = Color.White;
        public Color TintColor
        {
            get { return m_TintColor; }
            set { m_TintColor = value; }
        }

        private Vector2 m_Position = Vector2.Zero;
        public Vector2 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)m_StringToDraw.Length,
                (int)m_SpriteFont.MeasureString(TextToString()).Y);
            }
        }

        public TextComponent(string i_AssetName, Game i_Game)
            : base(i_AssetName, i_Game, int.MaxValue)
        { }

        public TextComponent(string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
            : base(i_AssetName, i_Game, i_UpdateOrder, i_DrawOrder)
        { }

        public TextComponent(string i_AssetName, string i_Name, Game i_Game, int i_CallsOrder)
            : base(i_AssetName, i_Game, i_CallsOrder)
        { }

        protected override void LoadContent()
        {
            base.LoadContent();

            m_SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
            m_SpriteFont = Game.Content.Load<SpriteFont>(AssetName);
        }

        public override void Draw(GameTime i_GameTime)
        {
            m_SpriteBatch.Begin();

            m_SpriteBatch.DrawString(m_SpriteFont, TextToString(), Position, TintColor);

            m_SpriteBatch.End();
        }

        protected override void InitBounds()
        {}

        protected override void DrawBoundingBox()
        {}

        public virtual string TextToString()
        {
            return m_StringToDraw;
        }
    }
}
