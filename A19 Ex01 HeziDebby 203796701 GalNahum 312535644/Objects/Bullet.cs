using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class Bullet : Sprite, ICollidable2D
    {
        private const string k_AssetName = @"Sprites\Bullet";
        private const float k_Velocity = 155;
        public Type OwnerType; // TODO: should i put default value ?

        public Bullet(Game i_Game) : base(k_AssetName, i_Game)
        {
            Velocity = new Vector2(0, k_Velocity);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            SetVisibleFalseIfOutOfSight();
        }

        private void SetVisibleFalseIfOutOfSight()
        {
            if (Position.Y <= GraphicsDevice.Viewport.Y)// || Position.Y <= 0)
            {
                Visible = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            //base.Draw(gameTime);
            if (Visible)
            {
                base.Draw(gameTime);
            }
        }

        public override bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;

            if (OwnerType != i_Source.GetType())
            {
                collided = base.CheckCollision(i_Source);
            }

            return collided;
        }

        public void Explode(int i_XToExplode, int i_YToExplode, Sprite i_SpriteToExplode)
        {
            int direction = this.Velocity.Y < 0 ? -1 : 1;
            int destinateY = (i_YToExplode + (int)(i_YToExplode * 0.7)) * direction;
            int currentY = i_YToExplode;

            while (currentY != destinateY)
            {
                //  base.OnPixelsCollision(i_X, currentY, i_Source);

                currentY += destinateY < currentY ? -1 : 1;
            }
        }
    }

    //public void PixelsBasedCollision(int i_X, int i_Y, ICollidable2D i_Source)
    //{
    //    int direction = this.Velocity.Y < 0 ? -1 : 1;
    //    int destinateY = (i_Y + (int)(i_Y * 0.7)) * direction;
    //    int currentY = i_Y;

    //    while (currentY != destinateY)
    //    {
    //        //  base.OnPixelsCollision(i_X, currentY, i_Source);

    //        currentY += destinateY < currentY ? -1 : 1;
    //    }
    //}

    //protected override void OnPixelsCollision(int i_X, int i_Y, ICollidable2D i_Source)
    //{
    //    int direction = this.Velocity.Y < 0 ? -1 : 1;
    //    int destinateY = (i_Y + (int)(i_Y * 0.7)) * direction;
    //    int currentY = i_Y;

    //    while (currentY != destinateY)
    //    {
    //      //  base.OnPixelsCollision(i_X, currentY, i_Source);

    //        currentY += destinateY < currentY ? -1 : 1;
    //    }

    //   // base.OnPixelsCollision(i_X, i_Y, i_Source);
    //}


    //protected override void OnPositionChanged()
    //{
    //    m_Velocity.Y = Position.Y;
    //}


    //    public void Move(GameTime i_GameTime)
    //    {
    //        CurrentPosition = new Vector2(CurrentPosition.X, Utilities.CalculateNewCoordinate(CurrentPosition.Y, CurrentDirection, Velocity, i_GameTime.ElapsedGameTime.TotalSeconds));
    //        //    return i_oldCoord + directionMultiplier * (i_velocity * (float)i_elaspedSeconds);
    //        //   float yPosition = CurrentPosition.Y + (Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds);
    //        //   CurrentPosition = new Vector2(CurrentPosition.X, yPosition);
    //    }

    //    public bool Hits(IDestryoable i_Destroyable)
    //    {
    //        return (Rectangle.Intersects(i_Destroyable.Rectangle) && pixelBasedCollisionDetect(i_Destroyable));

    //    }

    //    private bool pixelBasedCollisionDetect(IDestryoable i_Destroyable)
    //    {

    //        return true;
    //    }

    //    public void Hide() // TODO: check if should be public
    //    {
    //        IsVisible = false;
    //    }



}
//}
//public class Bullet : GameObject, IDestryoable
//{
//    private readonly float k_BulletVelocity = 155;
//    // $G$ DSN-001 (-2) this property belongs to GameObject
//    public bool IsVisible { get; private set; }
//    // $G$ DSN-999 (-2) why does a bullet have souls?
//    public int Souls { get; private set; }

//    public Bullet(GraphicsDevice i_GraphicsDevice) : base(i_GraphicsDevice)
//    {

//    }

//    public override void Initialize(ContentManager i_Content)
//    {
//        base.Initialize(i_Content);
//        //      Souls = Utilities.k_BulletSouls;
//        Velocity = k_BulletVelocity;
//        IsVisible = true;
//    }

//    protected override void LoadContent(ContentManager i_Content)
//    {
//        base.LoadContent(i_Content);

//        Texture = i_Content.Load<Texture2D>(@"Sprites\Bullet");
//    }

//    public void InitPosition(Vector2 i_InitialPosition)
//    {
//        CurrentPosition = i_InitialPosition;
//    }

//    public void Move(GameTime i_GameTime)
//    {
//        CurrentPosition = new Vector2(CurrentPosition.X, Utilities.CalculateNewCoordinate(CurrentPosition.Y, CurrentDirection, Velocity, i_GameTime.ElapsedGameTime.TotalSeconds));
//        //    return i_oldCoord + directionMultiplier * (i_velocity * (float)i_elaspedSeconds);
//        //   float yPosition = CurrentPosition.Y + (Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds);
//        //   CurrentPosition = new Vector2(CurrentPosition.X, yPosition);
//    }

//    public override void Update(GameTime i_GameTime)
//    {
//        Move(i_GameTime);
//        base.Update(i_GameTime);
//    }

//    public override void Draw(GameTime i_GameTime)
//    {
//        if (IsVisible)
//        {
//            SpriteBatch.Begin();

//            SpriteBatch.Draw(Texture, CurrentPosition, Color);

//            SpriteBatch.End();
//        }
//    }

//    public bool Hits(IDestryoable i_Destroyable)
//    {
//        return (Rectangle.Intersects(i_Destroyable.Rectangle) && pixelBasedCollisionDetect(i_Destroyable));

//    }

//    private bool pixelBasedCollisionDetect(IDestryoable i_Destroyable)
//    {

//        return true;
//    }

//    public void Hide() // TODO: check if should be public
//    {
//        IsVisible = false;
//    }

//    public bool IsOutOfSight()
//    {
//        return (!IsVisible || CurrentPosition.Y <= GraphicsDevice.Viewport.Y);
//    }

//    public void GetHit()
//    {
//        Souls--;
//    }

//    public bool IsDead()
//    {
//        return Souls == 0;
//    }
//}
//}
