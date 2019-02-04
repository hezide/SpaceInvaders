//*** Guy Ronen (c) 2008-2011 ***//
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Infrastructure.ObjectModel
{
    public class Sprite : LoadableDrawableComponent
    {
        protected CompositeAnimator m_Animations;
        public CompositeAnimator Animations
        {
            get { return m_Animations; }
            set { m_Animations = value; }
        }

        private Texture2D m_Texture;
        public Texture2D Texture
        {
            get { return m_Texture; }
            set { m_Texture = value; }
        }

        protected float m_Width;
        public float Width
        {
            get { return m_WidthBeforeScale * m_Scales.X; }
            set
            {
                if (m_Width != value)
                {
                    m_WidthBeforeScale = value / m_Scales.X;
                    OnSizeChanged();
                }
            }
        }

        protected float m_Height;
        public float Height
        {
            get { return m_HeightBeforeScale * m_Scales.Y; }
            set
            {
                if (m_Height != value)
                {
                    m_HeightBeforeScale = value / m_Scales.Y;
                    OnSizeChanged();
                }
            }
        }

        protected float m_WidthBeforeScale;
        public float WidthBeforeScale
        {
            get { return m_WidthBeforeScale; }
            set { m_WidthBeforeScale = value; }
        }

        protected float m_HeightBeforeScale;
        public float HeightBeforeScale
        {
            get { return m_HeightBeforeScale; }
            set { m_HeightBeforeScale = value; }
        }

        protected Vector2 m_Position;
        public Vector2 Position
        {
            get { return m_Position; }
            set
            {
                if (m_Position != value)
                {
                    m_Position = value;
                    OnPositionChanged();
                }
            }
        }

        public Vector2 m_PositionOrigin;
        public Vector2 PositionOrigin
        {
            get { return m_PositionOrigin; }
            set { m_PositionOrigin = value; }
        }

        public Vector2 m_RotationOrigin = Vector2.Zero;
        public Vector2 RotationOrigin
        {
            get { return m_RotationOrigin; }
            set { m_RotationOrigin = value; }
        }

        public Vector2 PositionForDraw
        {
            get { return this.Position - this.PositionOrigin + this.RotationOrigin; }
        }

        public Vector2 TopLeftPosition
        {
            get { return this.Position - this.PositionOrigin; }
            set { this.Position = value + this.PositionOrigin; }
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)TopLeftPosition.X,
                    (int)TopLeftPosition.Y,
                    (int)this.Width,
                    (int)this.Height);
            }
        }

        public Rectangle BoundsBeforeScale
        {
            get
            {
                return new Rectangle(
                    (int)TopLeftPosition.X,
                    (int)TopLeftPosition.Y,
                    (int)this.WidthBeforeScale,
                    (int)this.HeightBeforeScale);
            }
        }

        protected Rectangle m_SourceRectangle;
        public Rectangle SourceRectangle
        {
            get { return m_SourceRectangle; }
            set { m_SourceRectangle = value; }
        }

        public Vector2 TextureCenter
        {
            get
            {
                return new Vector2(m_Texture.Width / 2, m_Texture.Height / 2);
            }
        }

        public Vector2 SourceRectangleCenter
        {
            get { return new Vector2(m_SourceRectangle.Width / 2, m_SourceRectangle.Height / 2); }
        }

        protected float m_Rotation = 0;
        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }

        protected Vector2 m_Scales = Vector2.One;
        public Vector2 Scales
        {
            get { return m_Scales; }
            set
            {
                if (m_Scales != value)
                {
                    m_Scales = value;
                    // OnSizeChanged();
                    // Notify the Collision Detection mechanism:
                    OnPositionChanged();
                }
            }
        }

        protected Color m_TintColor = Color.White;
        public Color TintColor
        {
            get { return m_TintColor; }
            set { m_TintColor = value; }
        }

        public float Opacity
        {
            get { return m_TintColor.A / (float)byte.MaxValue; }
            set { m_TintColor.A = (byte)(value * byte.MaxValue); }
        }

        protected float m_LayerDepth = 0;
        public float LayerDepth
        {
            get { return m_LayerDepth; }
            set { m_LayerDepth = value; }
        }

        protected SpriteEffects m_SpriteEffects = SpriteEffects.None;
        public SpriteEffects SpriteEffects
        {
            get { return m_SpriteEffects; }
            set { m_SpriteEffects = value; }
        }

        private float m_AngularVelocity = 0;
        /// <summary>
        /// Radians per Second on X Axis
        /// </summary>
        public float AngularVelocity
        {
            get { return m_AngularVelocity; }
            set { m_AngularVelocity = value; }
        }

        protected Vector2 m_Velocity = Vector2.Zero;
        public Vector2 Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }

        public event EventHandler<EventArgs> Collision;
        protected virtual void OnCollision(object i_Sender, EventArgs i_Args)
        {
            if (Collision != null)
            {
                Collision.Invoke(i_Sender, i_Args);
            }
        }

        public Sprite(string i_AssetName, Game i_Game, GameScreen i_Screen, int i_UpdateOrder, int i_DrawOrder)
            : base(i_AssetName, i_Game, i_Screen, i_UpdateOrder, i_DrawOrder)
        { }

        public Sprite(string i_AssetName, Game i_Game, GameScreen i_Screen, int i_CallsOrder)
            : base(i_AssetName, i_Game, i_Screen, i_CallsOrder)
        { }

        public Sprite(string i_AssetName, Game i_Game, GameScreen i_Screen)
            : base(i_AssetName, i_Game, i_Screen, int.MaxValue)
        { }

        /// <summary>
        /// Default initialization of bounds
        /// </summary>
        /// <remarks>
        /// Derived classes are welcome to override this to implement their specific boudns initialization
        /// </remarks>
        protected override void InitBounds()
        {
            // default initialization of bounds
            m_WidthBeforeScale = m_Texture.Width;
            m_HeightBeforeScale = m_Texture.Height;
            m_Position = Vector2.Zero;

            InitSourceRectangle();

            InitOrigins();
        }

        protected virtual void InitOrigins()
        {
        }

        protected virtual void InitSourceRectangle()
        {
            m_SourceRectangle = new Rectangle(0, 0, (int)m_WidthBeforeScale, (int)m_HeightBeforeScale);
        }

        protected bool m_UseSharedBatch = true;

        protected SpriteBatch m_SpriteBatch;
        public SpriteBatch SpriteBatch
        {
            set
            {
                m_SpriteBatch = value;
                m_UseSharedBatch = true;
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            m_Animations = new CompositeAnimator(this);
        }

        protected override void LoadContent()
        {
            if(m_AssetName != String.Empty)
            {
                m_Texture = Game.Content.Load<Texture2D>(m_AssetName);
            }
            else
            {
                m_Texture = new Texture2D(this.Game.GraphicsDevice, 1, 1);
            }

            if (m_SpriteBatch == null)
            {
                m_SpriteBatch =
                    Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;

                if (m_SpriteBatch == null)
                {
                    m_SpriteBatch = new SpriteBatch(Game.GraphicsDevice);
                    m_UseSharedBatch = false;
                }
            }

            base.LoadContent();
        }

        /// <summary>
        /// Basic movement logic (position += velocity * totalSeconds)
        /// </summary>
        /// <param name="gameTime"></param>
        /// <remarks>
        /// Derived classes are welcome to extend this logic.
        /// </remarks>
        public override void Update(GameTime gameTime)
        {
            float totalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.Position += this.Velocity * totalSeconds;
            this.Rotation += this.AngularVelocity * totalSeconds;

            base.Update(gameTime);

            this.Animations.Update(gameTime);

        }

        /// <summary>
        /// Basic texture draw behavior, using a shared/own sprite batch
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.Begin();
            }

            DrawWithParameters();

            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.End();
            }

            base.Draw(gameTime);
        }

        protected void DrawWithParameters()
        {
            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            }

                m_SpriteBatch.Draw(m_Texture, this.PositionForDraw,
                    this.SourceRectangle, this.TintColor,
                    this.Rotation, this.RotationOrigin, this.Scales,
                    SpriteEffects.None, this.LayerDepth);

            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.End();
            }
        }

        protected void DrawNonPremultiplied()
        {
            m_SpriteBatch.End();
            m_SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            DrawWithParameters();

            m_SpriteBatch.End();
            m_SpriteBatch.Begin(
                SpriteSortMode.Deferred, BlendState.AlphaBlend, null,
                null, null, null, Matrix.Identity);
        }

        protected override void DrawBoundingBox()
        {
            // not implemented yet
        }

        #region Collision Handlers 
        public PixelBasedCollisionComponent PixelBasedCollisionComponent { get; set; }

        public virtual bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;

            ICollidable2D source = i_Source as ICollidable2D;

            // *** first checking rectangle intersections ***
            Rectangle intersection = boundsIntersection(source);
            collided = intersection != Rectangle.Empty;
            // *** pixel based collision detection if this || i_Source are ICollidableByPixels ***
            if (collided && this is ICollidableByPixels)
            {
                collided = (this as ICollidableByPixels).PixelBasedCollisionComponent.CheckCollision(source, intersection);
            }
            else if (collided && (source is ICollidableByPixels))
            {
                collided = false;
            }

            return collided;
        }

        private Rectangle boundsIntersection(ICollidable2D i_Source)
        {
            return Rectangle.Intersect(this.Bounds, i_Source.Bounds);
        }

        private Color[] m_Pixels;
        public Color[] Pixels
        {
            get
            {
                if (m_Pixels == null)
                {
                    m_Pixels = new Color[Texture.Width * Texture.Height];
                    Texture.GetData<Color>(m_Pixels);
                    // TODO: OptimizePixels();
                }

                return m_Pixels;
            }
            set // TODO: should be public ?
            {
                m_Pixels = value;
            }
        }

        private Color[] getPixelsData(ICollidable2D i_Source)
        {
            Color[] sourcePixels;

            if (i_Source is Sprite)
            {
                sourcePixels = (i_Source as Sprite).Pixels;
            }
            else
            {
                sourcePixels = new Color[i_Source.Bounds.Width * i_Source.Bounds.Height];
                i_Source.Texture.GetData<Color>(sourcePixels);
            }

            return sourcePixels;
        }

        public virtual void Collided(ICollidable i_Collidable)
        {
            // defualt behavior
            if (this is ICollidableByPixels)
            {
                PixelBasedCollisionComponent.Collided(i_Collidable);
            }
            else
            {
                this.Visible = false;
            }

            OnCollision(i_Collidable, EventArgs.Empty); // TODO: isnt the sender is this ? or both ?
        }
        
        #endregion //Collision Handlers

        protected Texture2D GetTextureClone()
        {
            Texture2D texture = new Texture2D(this.GraphicsDevice, Texture.Width, Texture.Height);

            texture.SetData<Color>(this.Pixels);

            return texture;
        }
  
        public virtual bool HitBoundary()
        {
            return this.Visible ?
            Bounds.Right >= GraphicsDevice.Viewport.Bounds.Right ||
            Bounds.Left <= GraphicsDevice.Viewport.Bounds.Left : false;
        }

        public Sprite ShallowClone()
        {
            return this.MemberwiseClone() as Sprite;
        }

        protected override void Dispose(bool i_Disposing)
        {
            base.Dispose(i_Disposing);
            this.Visible = false;
        }
    }
}