//*** Guy Ronen (c) 2008-2011 ***//
using Infrastructure.ObjectModel.Animators;
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

        // TODO 01: The Bounds property for collision detection
        //public Rectangle Bounds
        //{
        //    get
        //    {
        //        return new Rectangle(
        //            (int)m_Position.X,
        //            (int)m_Position.Y,
        //            m_Width,
        //            m_Height);
        //    }
        //}
        // -- end of TODO 01

        // TODO 13: Notify about  change:
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
        // -- end of TODO 13
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

        public Sprite(string i_AssetName, Game i_Game, int i_UpdateOrder, int i_DrawOrder)
            : base(i_AssetName, i_Game, i_UpdateOrder, i_DrawOrder)
        { }

        public Sprite(string i_AssetName, Game i_Game, int i_CallsOrder)
            : base(i_AssetName, i_Game, i_CallsOrder)
        { }

        public Sprite(string i_AssetName, Game i_Game)
            : base(i_AssetName, i_Game, int.MaxValue)
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

        private bool m_UseSharedBatch = true;

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
            m_Texture = Game.Content.Load<Texture2D>(m_AssetName);

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

            if (BoundaryHitAffects)
            {
                BoundaryCheckAndInvoke();
            }

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

            //           m_SpriteBatch.Draw(m_Texture, m_Position, m_TintColor);
            DrawWithParameters();

            if (!m_UseSharedBatch)
            {
                m_SpriteBatch.End();
            }

            base.Draw(gameTime);
        }

        protected void DrawWithParameters()
        {
            m_SpriteBatch.Draw(m_Texture, this.PositionForDraw,
                    this.SourceRectangle, this.TintColor,
                    this.Rotation, this.RotationOrigin, this.Scales,
                    SpriteEffects.None, this.LayerDepth);
        }

        protected void DrawNonPremultiplied()
        {
            m_SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            DrawWithParameters();

            m_SpriteBatch.End();
        }

        // TODO 04:
        protected override void DrawBoundingBox()
        {
            // not implemented yet
        }
        // -- end of TODO 04

        #region Collision Handlers 
        private bool m_IsCollided = false;
        public bool IsCollided
        {
            get { return m_IsCollided; }
            set { m_IsCollided = value; }
        }

        // TODO 14: Implement a basic collision detection between two ICollidable2D objects:
        public virtual bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;
            ICollidable2D source = i_Source as ICollidable2D;

            if (source != null)
            {
                collided = checkCollisionByBounds(source);
                // TODO: change the name CollisionByPixels
                if (collided && CollisionByPixels) // || source.CollisionByPixels))
                // TODO: checks twise the pixels (for both objects) - try to optimize !
                {
                    collided = pixelBasedCollisionDetection(source);
                }
                else if (collided && source.CollisionByPixels)
                {
                    collided = false;
                }
            }

            return collided;
        }

        private bool checkCollisionByBounds(ICollidable2D i_Source)
        {
            return i_Source.Bounds.Intersects(this.Bounds) || i_Source.Bounds.Contains(this.Bounds);
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

        private bool m_CollisionByPixels = false;
        public bool CollisionByPixels
        {
            get { return m_CollisionByPixels; }
            set { m_CollisionByPixels = value; }
        }

        private bool pixelBasedCollisionDetection(ICollidable2D i_Source)
        {
            bool collided = false;
            Color[] sourcePixels;

            sourcePixels = getPixelsData(i_Source);

            // *** overlapping bounds ***
            int top = Math.Max(this.Bounds.Top, i_Source.Bounds.Top);
            int bottom = Math.Min(this.Bounds.Bottom, i_Source.Bounds.Bottom);
            int left = Math.Max(this.Bounds.Left, i_Source.Bounds.Left);
            int right = Math.Min(this.Bounds.Right, i_Source.Bounds.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    if (!isTransparent(this.Pixels, x, y, this.Bounds) &&
                        !isTransparent(sourcePixels, x, y, i_Source.Bounds))
                    //if (this.Pixels[getPixel(x, y, this.Bounds)].A != 0 &&
                    //    sourcePixels[(x - i_Source.Bounds.Left) + ((y - i_Source.Bounds.Top) * i_Source.Bounds.Width)].A != 0)
                    {
                        collided = true;

                    //    i_Source.PixelsBasedCollision(x, y, this as ICollidable2D);
                        OnPixelsCollision(x, y, i_Source);

                        //if (i_Source is Sprite)
                        //{
                        //    (i_Source as Sprite).OnPixelsCollision(x, y, i_Source);
                        //}

                        //this.transparentPixel(x, y);
                        // this.Pixels[(x - this.Bounds.Left) + ((y - this.Bounds.Top) * this.Bounds.Width)].A = 0;
                    }
                }
            }

            return collided;

            //for (int row = 0; row < i_Source.Texture.Width; row++)
            //{
            //    for (int col = 0; col < i_Source.Texture.Height; col++)
            //    { // *** if non transparent pixels collided ***
            //if (this.Pixels[row + col * this.Texture.Width].A != 0 &&
            //    i_Source.Pixels[row + col * i_Source.Texture.Width].A != 0)
            //{ // *** change the collided i_Source pixels to transparent ***
            //    Pixels[row + col * this.Texture.Width] = new Color(0, 0, 0, 0);
            //  //  Pixels[row + col * this.Texture.Width].A = 0;
            //    collided = true;
            //}


            //   }
            // }

            //            return collided;
        }
        // TODO: barriers -> override
        protected virtual void OnPixelsCollision(int i_X, int i_Y, ICollidable2D i_Source)
        {
            // *** default behavior ***
            transparentPixel(i_X, i_Y);
        }

        private void transparentPixel(int i_X, int i_Y)
        {
            this.Pixels[getPixel(i_X, i_Y, this.Bounds)].A = 0;
        }

        private bool isTransparent(Color[] i_Pixels, int i_X, int i_Y, Rectangle i_Bounds)
        {
            return i_Pixels[getPixel(i_X, i_Y, i_Bounds)].A == 0;
        }

        private int getPixel(int i_X, int i_Y, Rectangle i_Bounds)
        {
            return (i_X - i_Bounds.Left) + ((i_Y - i_Bounds.Top) * i_Bounds.Width);
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

        // -- end of TODO 14

        // TODO 15: Implement a basic collision reaction between two ICollidable2D objects
        public virtual void Collided(ICollidable i_Collidable)
        {
            // defualt behavior
            if (CollisionByPixels)
            {
                this.Texture.SetData<Color>(Pixels);
            }
            else
            {
                this.Visible = false;
            }
        }

        protected Texture2D GetTextureClone()
        {
            Texture2D texture = new Texture2D(this.GraphicsDevice, Texture.Width, Texture.Height);

            texture.SetData<Color>(this.Pixels);

            return texture;
        }
        #endregion //Collision Handlers
        // TODO: should be on Sprite ? maybe should encapsulate in a different class ...
        // *** sprites hit boundaries behavior ***
        private bool m_BoundaryHitAffects = false;
        public bool BoundaryHitAffects
        {
            get { return m_BoundaryHitAffects; }
            set { m_BoundaryHitAffects = value; }
        }

        public event EventHandler<EventArgs> HitBoundaryEvent;

        protected virtual bool HitBoundary()
        {
            return this.Visible ?
                Bounds.Right >= GraphicsDevice.Viewport.Bounds.Right ||
                Bounds.Left <= GraphicsDevice.Viewport.Bounds.Left : false;
        }

        protected virtual void OnBoundaryHit(object i_Sender, EventArgs i_EventArgs)
        {
            if (HitBoundaryEvent != null)
            {
                HitBoundaryEvent.Invoke(i_Sender, i_EventArgs);
            }
        }

        protected virtual void BoundaryCheckAndInvoke()
        {
            if (HitBoundary())
            {
                OnBoundaryHit(this, EventArgs.Empty);
            }
        }

        public Sprite ShallowClone()
        {
            return this.MemberwiseClone() as Sprite;
        }

    }
}