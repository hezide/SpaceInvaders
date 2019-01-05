//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.ObjectModel
//{
//    public class AnimatableSprite : Sprite
//    {
//        private Vector2 m_PosOfOriginInScreen = Vector2.Zero;
//        public Vector2 PosOfOriginInScreen
//        {
//            get { return m_PosOfOriginInScreen; }
//            set { m_PosOfOriginInScreen = value; }
//        }

//        private Vector2 m_OriginInTexture;
//        public Vector2 OriginInTexture
//        {
//            get { return m_OriginInTexture; }
//            set { m_OriginInTexture = value; }
//        }

//        private Rectangle? m_SourceRectangle = null;
//        public Rectangle? SourceRectangle
//        {
//            get { return m_SourceRectangle; }
//            set { m_SourceRectangle = value; }
//        }

//        private Rectangle m_DestinationRectangle;
//        public Rectangle DestinationRectangle
//        {
//            get { return m_DestinationRectangle; }
//            set { m_DestinationRectangle = value; }
//        }

//        private Vector4 m_TintVector = Color.White.ToVector4();
//        public Vector4 TintVector
//        {
//            get { return m_TintVector; }
//            set { m_TintColor = new Color(value); } // TODO: is this properly ?
//        }

//        private float m_Rotation = 0;
//        public float Rotation
//        {
//            get { return m_Rotation; }
//            set { m_Rotation = value; }
//        }

//        private float m_ScaleFactor = 1f;
//        public float ScaleFactor
//        {
//            get { return m_ScaleFactor; }
//            set { m_ScaleFactor = value; }
//        }

//        private SpriteEffects m_SpriteEffects = SpriteEffects.None;
//        public SpriteEffects SpriteEffects
//        {
//            get { return m_SpriteEffects; }
//            set { m_SpriteEffects = value; }
//        }

//        private float m_LayerDepth = 0;
//        public float LayerDepth
//        {
//            get { return m_LayerDepth; }
//            set { m_LayerDepth = value; }
//        }

//        public AnimatableSprite(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
//        {
//        }

//        public override void Initialize()
//        {
//            base.Initialize();

//            OriginInTexture = new Vector2(((float)Width) / 2, ((float)Height) / 2);
//            DestinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
//        }

//        protected override void OnPositionChanged()
//        {
//            PosOfOriginInScreen = Position;
//            m_DestinationRectangle.X = (int)Position.X;
//            m_DestinationRectangle.Y = (int)Position.Y;
//        }
//        // TODO: what else here ?
//        protected override void OnSizeChanged()
//        {
//            base.OnSizeChanged();
//            m_DestinationRectangle.Width = Width;
//            m_DestinationRectangle.Height = Height;
//        }
//        // TODO: check that the tint is as defined at player and not overrided
//        public override void Draw(GameTime gameTime)
//        {
//            m_SpriteBatch.Begin();

//            if (m_ScaleFactorRequired)
//            {
//                m_SpriteBatch.Draw(
//                    Texture,
//                    PosOfOriginInScreen,
//                    SourceRectangle,
//                    m_TintColor,
//                    Rotation,
//                    OriginInTexture,
//                    ScaleFactor,
//                    SpriteEffects.None,
//                    LayerDepth);
//            }
//            else
//            {
//                m_SpriteBatch.Draw(
//                    Texture,
//                    DestinationRectangle,
//                    SourceRectangle,
//                    m_TintColor,
//                    Rotation,
//                    OriginInTexture,
//                    SpriteEffects,
//                    LayerDepth);
//            }
//            //m_SpriteBatch.Draw(
//            //    Texture,
//            //    null,
//            //    DestinationRectangle,
//            //    SourceRectangle,
//            //    OriginInTexture,
//            //    Rotation,
//            //    ScaleFactor,
//            //    color: m_TintColor,
//            //    effects: SpriteEffects,
//            //    layerDepth: LayerDepth);
//            //m_SpriteBatch.Draw(
//            //    Texture,
//            //    PosOfOriginInScreen,
//            //    SourceRectangle,
//            //    m_TintColor,
//            //    Rotation,
//            //    OriginInTexture,
//            //    ScaleFactor,
//            //    SpriteEffects.None,
//            //    LayerDepth);

//            m_SpriteBatch.End();
//        }

//        protected bool m_ScaleFactorRequired = false;

//        public virtual void Shrink(float i_ScaleFactor)
//        {
//            ScaleFactor *= i_ScaleFactor;
//            m_ScaleFactorRequired = true;
//        }

//        protected TimeSpan m_FadeOutTime = TimeSpan.FromSeconds(2.2);
//        protected TimeSpan m_TimeLeftToFadeOut = TimeSpan.FromSeconds(2.2);

//        public virtual void FadeOut(GameTime gameTime)
//        {
//            m_TimeLeftToFadeOut -= gameTime.ElapsedGameTime;

//            if (m_TimeLeftToFadeOut.TotalSeconds <= 0)
//            {
//                Visible = false;
//                m_TimeLeftToFadeOut = m_FadeOutTime;
//            }
//        }

//        protected TimeSpan m_BlinkingTime = TimeSpan.FromSeconds(0.2);
//        protected TimeSpan m_TimeToBlink = TimeSpan.FromSeconds(0.2);

//        public virtual bool Blink(GameTime gameTime)
//        {
//            bool blinking = false;

//            m_TimeToBlink -= gameTime.ElapsedGameTime;

//            if (m_TimeToBlink.TotalSeconds <= 0)
//            {
//                blinking = true;
//                m_TimeToBlink = m_BlinkingTime;
//            }

//            return blinking;
//        }
//    }
//}
