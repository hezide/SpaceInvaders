//*** Guy Ronen (c) 2008-2011 ***//
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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

        // TODO: check that works well, change every toglle of velocity in the code to direction toggle
        protected Vector2 m_Direction = Vector2.Zero;
        public Vector2 Direction
        {
            get
            {
                return Velocity == Vector2.Zero ?
                        m_Direction
                        : Velocity / new Vector2(Math.Abs(Velocity.X), Math.Abs(Velocity.Y));
            }
            set
            {
                Velocity *= value;
                m_Direction = value;
            }
        }

        public event EventHandler<EventArgs> Collision;
        protected virtual void OnCollision(object sender, EventArgs args)
        {
            if (Collision != null)
            {
                Collision.Invoke(sender, args);
            }
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

        public PixelBasedCollisionComponent PixelBasedCollisionComponent { get; set; }
        // TODO 14: Implement a basic collision detection between two ICollidable2D objects:
        public virtual bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;

            ICollidable2D source = i_Source as ICollidable2D;

            Rectangle intersection = boundsIntersection(source);
            collided = intersection != Rectangle.Empty;

            if (collided && this is ICllidableByPixels)
            //PixelsCollidable)

            {
                collided = (this as ICllidableByPixels).PixelBasedCollisionComponent.PixelsIntersects(source, intersection);
                // collided = pixelsIntersects(source, intersection);
            }
            else if (collided && (source is ICllidableByPixels))
            //source.PixelsCollidable)
            {
                collided = false;
            }

            return collided;
        }

        //  private List<Point> m_IntersectionPoints;
        public List<Point> IntersectionPoints { get; set; }

        //private bool pixelsIntersects(ICollidable2D i_Source, Rectangle i_Intersection)
        //{
        //    Color[] sourcePixels = getPixelsData(i_Source); // TODO: this method is a helper of sprite. got nothing to do with sprite
        //    List<Point> intersections = new List<Point>();

        //    for (int y = i_Intersection.Top; y < i_Intersection.Bottom; y++)
        //    {
        //        for (int x = i_Intersection.Left; x < i_Intersection.Right; x++)
        //        {
        //            if (this.Pixels[getPixel(x, y, this.Bounds)].A != 0 &&
        //                 sourcePixels[getPixel(x, y, i_Source.Bounds)].A != 0)
        //            {
        //                intersections.Add(new Point(x, y));
        //            }
        //        }
        //    }

        //    m_IntersectionPoints = intersections;

        //    return m_IntersectionPoints.Count > 0;
        //}
    
        public virtual void OnPixelsCollision(ICollidable i_Collidable)
        {
            foreach (Point point in IntersectionPoints)
            {
                int pixelIdx = point.X - this.Bounds.Left + ((point.Y - this.Bounds.Top) * this.Bounds.Width);
                this.Pixels[pixelIdx].A = 0;
            }

            this.Texture.SetData<Color>(this.Pixels);
        }

        protected virtual void Explode(int i_ExplosionRange)
        {
            foreach (Point point in IntersectionPoints)
            {
                   transperantPixelsInRange(point, i_ExplosionRange);
            }

            this.Texture.SetData<Color>(this.Pixels);
        }

        private void transperantPixelsInRange(Point i_Point, int i_ExplosionRange)
        {
            int direction = i_ExplosionRange > 0 ? 1 : -1;
            int destinateY = i_Point.Y + i_ExplosionRange;
            int currentY = i_Point.Y;

            while (currentY != destinateY)
            {
                int pixelIdx = getPixel(i_Point.X, currentY, this.Bounds);

                if (pixelIdx < Pixels.Length && pixelIdx >= 0)
                {
                    transparentPixel(pixelIdx);
                    //this.Pixels[pixelIdx].A = 0;
                }

                currentY += destinateY < currentY ? -1 : 1;
            }
        }

        /*
        public void Explode(int i_XToExplode, int i_YToExplode, Sprite i_SpriteToExplode)
{
   int direction = this.Velocity.Y < 0 ? -1 : 1;
   int destinateY = (i_YToExplode + direction * (int)(Height * 0.7));
   int currentY = i_YToExplode;

   while (currentY != destinateY)
   {
       //  base.OnPixelsCollision(i_X, currentY, i_Source);
       if (i_SpriteToExplode.GetPixel(i_XToExplode, i_YToExplode, i_SpriteToExplode.Bounds) >= 0)
       {
           i_SpriteToExplode.OnPixelsCollision(i_XToExplode, currentY);

       }

       currentY += destinateY < currentY ? -1 : 1;
   }
}
    */
        //   return (i_X - i_Bounds.Left) + ((i_Y - i_Bounds.Top) * i_Bounds.Width);
        // 

        //private bool isPixelsIntersection(ICollidable2D i_Source)
        //{
        //    bool isPixelIntersect = false;

        //    while (!isPixelIntersect)
        //    {

        //    }

        //    return false;
        //}

        //{
        //    bool collided = false;
        //    ICollidable2D source = i_Source as ICollidable2D;

        //    if (source != null)
        //    {
        //        collided = checkBoundsCollision(source);

        //        if (collided && this.CollisionByPixels)
        //        {
        //            collided = checkPixelBasedCollision(source);
        //        }
        //        else if (collided && source.CollisionByPixels)
        //        {
        //            collided = false;
        //        }
        //    }

        //    /*
        //     pseudo
        //     if ( source is ipixelCollidable)
        //     collided = iPixelCollidable.checkPixelBased(i_source)
        //     */

        //    return collided;
        //}

        //public virtual bool CheckCollision(ICollidable i_Source)
        //{
        //    bool collided = false;
        //    ICollidable2D source = i_Source as ICollidable2D;

        //    if (source != null)
        //    {
        //        collided = checkCollisionByBounds(source);
        //        // TODO: change the name CollisionByPixels
        //        if (collided && this is IPixelCollidable) //CollisionByPixels) // || source.CollisionByPixels))
        //        // TODO: checks twise the pixels (for both objects) - try to optimize !
        //        {
        //            //  collided = pixelBasedCollisionDetection(source);
        //            collided = (this as IPixelCollidable).Collided()
        //        }
        //        else if (collided && source is IPixelCollidable) //.CollisionByPixels)
        //        {
        //            collided = false;
        //        }
        //    }

        //    return collided;
        //}

        private Rectangle boundsIntersection(ICollidable2D i_Source)
        {
            //  return i_Source.Bounds.Intersects(this.Bounds) || i_Source.Bounds.Contains(this.Bounds);
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

        //private bool m_CollisionByPixels = false;
        //public bool PixelsCollidable
        //{
        //    get { return m_CollisionByPixels; }
        //    set { m_CollisionByPixels = value; }
        //}

    

        private void transparentPixel(int i_PixelIdx)
        {
           // int pixelIndex = getPixel(i_X, i_Y, this.Bounds);

            //if (pixelIndex >= 0 && pixelIndex < Pixels.Length) // TODO: throws exception out of bounds
            //{
            this.Pixels[i_PixelIdx].A = 0;
            //}
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
            if (this is ICllidableByPixels)
            {
                OnPixelsCollision(i_Collidable);
                //this.Texture.SetData<Color>(Pixels);
            }
            else
            {
                this.Visible = false;
            }

            OnCollision(i_Collidable, EventArgs.Empty);
        }


        #endregion //Collision Handlers

        protected Texture2D GetTextureClone()
        {
            Texture2D texture = new Texture2D(this.GraphicsDevice, Texture.Width, Texture.Height);

            texture.SetData<Color>(this.Pixels);

            return texture;
        }
        // TODO: should be on Sprite ? maybe should encapsulate in a different class ...
        // *** sprites hit boundaries behavior ***
        private bool m_BoundaryHitAffects = false;
        public bool BoundaryHitAffects
        {
            get { return m_BoundaryHitAffects; }
            set { m_BoundaryHitAffects = value; }
        }

        public event EventHandler<OffsetEventArgs> HitBoundaryEvent;

        public virtual bool HitBoundary()
        {
            bool ret = false;
            //return this.Visible ?
            ////Bounds.Right >= GraphicsDevice.Viewport.Bounds.Right ||
            ////Bounds.Left <= GraphicsDevice.Viewport.Bounds.Left : false;

            if (this.Visible) // TODO: this is for debugging only
            {
                if (Bounds.Right >= GraphicsDevice.Viewport.Bounds.Right ||
                    Bounds.Left <= GraphicsDevice.Viewport.Bounds.Left)
                {
                    ret = true;
                }
            }

            return ret;
        }

        protected virtual void OnBoundaryHit(object i_Sender, OffsetEventArgs i_EventArgs)
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
            //    float offset = Position.X - MathHelper.Clamp(Position.X, 0, GraphicsDevice.Viewport.Width - Width);
                float offset = Position.X - MathHelper.Clamp(Position.X, GraphicsDevice.Viewport.Bounds.Left, GraphicsDevice.Viewport.Bounds.Right - Width);

             //   Vector2 newPosition = new Vector2(Position.X - offset, Position.Y);

            //    offset += Bounds.X - MathHelper.Clamp(Bounds.X, GraphicsDevice.Viewport.Bounds.Left, GraphicsDevice.Viewport.Bounds.Right - Width);
                //    float offset2 = Position.X - Bounds.Right;
                //    float offset3 = Position.X - Bounds.Left; ;
                //Position = new Vector2()
                // Position = new Vector2(MathHelper.Clamp(Position.X, GraphicsDevice.Viewport.Bounds.Left, GraphicsDevice.Viewport.Bounds.Right - Width), Position.Y);
             //   this.Position = newPosition;

                OnBoundaryHit(this, new OffsetEventArgs(offset));
            }
        }
        //    Position = new Vector2(MathHelper.Clamp(Position.X, 0, GraphicsDevice.Viewport.Width - Texture.Width), Position.Y);


        public Sprite ShallowClone()
        {
            return this.MemberwiseClone() as Sprite;
        }
    }

    public class OffsetEventArgs : EventArgs
    {
        public float Offset { get; set; }
        //public static readonly float Empty = 0;

        public OffsetEventArgs() : base()
        {
        }

        public OffsetEventArgs(float i_Offset) : base()
        {
            Offset = i_Offset;
        }
    }
}