using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class Enemy : Sprite, ICollidable2D //, IShooter
    {
        // TODO: change to sprite shit .....
       // private const string k_AssetName = @"Sprites\Enemy0101_32x32";
        private int k_NumOfFrames = 2;

        private const int k_Velocity = 120;
        private const int k_MaxAmmo = 1;
        private RandomActionComponent m_RandomShootingNotifier;

        private Gun Gun;
        public int Seed { get; set; } // TODO: not initialized
        // TODO: delete this overload
        //public Enemy(Game i_Game) : base(k_AssetName, i_Game)
        //{
        //    m_TintColor = Color.Pink;
        //    Gun = new Gun(k_MaxAmmo, i_Game, this.GetType());
        //}

        public Enemy(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
        {
            m_TintColor = Color.White;
            Gun = new Gun(k_MaxAmmo, i_Game, this.GetType());
        }

        public override void Initialize()
        {
            base.Initialize();
            Velocity = new Vector2(k_Velocity, 0);

            Gun.Initialize(Color.Blue);

            m_RandomShootingNotifier = new RandomActionComponent(1, 30, Seed);
            m_RandomShootingNotifier.RandomTimeAchieved += Shoot;

         //   initAnimations();
        }

        private void initAnimations()
        {
            CellAnimator celAnimation = new CellAnimator(TimeSpan.FromSeconds(0.5), k_NumOfFrames, TimeSpan.Zero);
            this.Animations.Add(celAnimation);
            this.Animations.Enabled = true;
        }

        private int m_EnemyCellIdx = 0;
        public int EnemyCellIdx
        {
            get { return m_EnemyCellIdx; }
            set { m_EnemyCellIdx = value; }
        }

        protected override void InitSourceRectangle()
        {
            base.InitSourceRectangle();

            this.SourceRectangle = new Rectangle(
                0,
                0,
                (int)(m_SourceRectangle.Width / 6),
                (int)m_HeightBeforeScale);
        } // TODO: need to set x position to width * cell index -> the beginig of the cell i want

        protected override void InitOrigins()
        {
            base.InitOrigins();

            this.RotationOrigin = this.SourceRectangleCenter;
        }

        //protected override void InitBounds()
        //{
        //    base.InitBounds();
        //    m_WidthBeforeScale = Texture.Width / 6;
        //    m_HeightBeforeScale = Texture.Height;
        //    m_Position = Vector2.Zero;

        //    InitSourceRectangle();
        //    m_Width /= 6;
        //    m_Height = Texture.Height;
        //}

        public override void Collided(ICollidable i_Collidable)
        {
            //base.Collided(i_Collidable);
            IsCollided = true;
        }

        public override void Update(GameTime gameTime)
        {
            //if (IsCollided)
            //{
                //   Velocity = Vector2.Zero;
                // Shrink(0.9f);
          //  }
         //   else
          //  {
                m_RandomShootingNotifier.Update(gameTime);
          //  }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
                base.Draw(gameTime);
        }
        // TODO: implement jump on blink
        //public override bool Blink(GameTime gameTime)
        //{
        //    return base.Blink(gameTime);
        //}

        public void Shoot()
        {
            Gun.Shoot(new Vector2(Position.X + Width / 2, Position.Y));
        }

        public override bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;

            if (i_Source is Bullet && (i_Source as Bullet).OwnerType != this.GetType())
            {
                collided = base.CheckCollision(i_Source);
            }

            return collided;
        }
    }
}
//protected override bool HitBoundary()
//{
//    // return base.HitBoundary();
//    return this.Visible ?
//       Bounds.Right >= GraphicsDevice.Viewport.Bounds.Right ||
//       Bounds.Left <= GraphicsDevice.Viewport.Bounds.Left : false;
//}

//    public class Enemy : GameObject, IDestryoable, IShooter
//    {
//        public int Souls { get; set; }
//        public List<Bullet> BulletsList { get; private set; }
//        private double m_SecondsFromLastJump = 0;
//        private RandomActionComponent m_RandomShootingNotifier;
//        private ShootingLogic m_ShootingLogic;
//        private readonly float k_EnemyVelocity = 120;

//        // $G$ CSS-013 (0) Bad parameter names (should be in the form of i_PascalCase).
//        public Enemy(GraphicsDevice i_graphics, int i_randomSeed) : base(i_graphics)
//        {
//            BulletsList = new List<Bullet>();
//            m_RandomShootingNotifier = new RandomActionComponent(1, 30, i_randomSeed);
//            m_RandomShootingNotifier.RandomTimeAchieved += Fire;
//        }

//        // $G$ CSS-013 (0) Bad parameter names (should be in the form of i_PascalCase).
//        public override void Initialize(ContentManager i_content)
//        {
//            base.Initialize(i_content);
//            m_ShootingLogic = new ShootingLogic();
//            CurrentDirection = Utilities.eDirection.Right;
//            Velocity = k_EnemyVelocity;
//            Souls = Utilities.k_EnemySouls;
//        }

//        // $G$ CSS-013 (0) Bad parameter names (should be in the form of i_PascalCase).
//        public void InitPosition(int i_row, int i_col)
//        {
//            float height = Texture.Height;
//            float width = Texture.Width;

//            float x = i_col * width + width * Utilities.k_EnemyGapMultiplier * i_col;
//            float y = (i_row * height + height * Utilities.k_EnemyGapMultiplier * i_row) + Utilities.k_InitialHightMultiplier * height;

//            CurrentPosition = new Vector2(x + 1, y + 1);
//        }

//        // $G$ CSS-013 (0) Bad parameter names (should be in the form of i_PascalCase).
//        protected override void LoadContent(ContentManager i_content)
//        {
//            base.LoadContent(i_content);

//            string folder = @"Sprites\";
//            string enemy = String.Format("Enemy0{0}01_32x32", (int)TypeOfGameObject + 1);
//            // $G$ XNA-002 (-5) Instead of concatanating folder and asset name, you should have used Content.RootFolder
//            Texture = Content.Load<Texture2D>(folder + enemy);
//        }

//        private void move(GameTime i_GameTime)
//        {
//            m_SecondsFromLastJump += i_GameTime.ElapsedGameTime.TotalSeconds;

//            if (m_SecondsFromLastJump > 0.5)
//            {
//                // float xPosition = CurrentPosition.X + (Velocity * (float)m_SecondsFromLastJump);
//                float xPosition = Utilities.CalculateNewCoordinate(CurrentPosition.X, CurrentDirection, Velocity, m_SecondsFromLastJump);
//                CurrentPosition = new Vector2(xPosition, CurrentPosition.Y);
//                m_SecondsFromLastJump = 0;
//            }

//        }

//        //public bool IsHittingBoundris()
//        //{
//        //    return ((CurrentPosition.X >= GraphicsDevice.Viewport.Width - Texture.Width) ||
//        //            (CurrentPosition.X <= 0) ||
//        //            (CurrentPosition.Y >= GraphicsDevice.Viewport.Height - Texture.Height));
//        //}

//        //public float CalcOffset()
//        //{
//        //    float offset = 0;

//        ////    Velocity *= -1;

//        //    if (CurrentPosition.X >= GraphicsDevice.Viewport.Width - Texture.Width)
//        //    {
//        //        offset = GraphicsDevice.Viewport.Width - Texture.Width - CurrentPosition.X - 1;
//        //    }
//        //    else if (CurrentPosition.X <= 0)
//        //    {
//        //        offset = -1 * (CurrentPosition.X - 1);
//        //    }

//        //    return offset;
//        //}

//        // $G$ CSS-015 (-2) Bad parameter names (should be in the form of io_PascalCase).
//        public bool IsWallHit(ref Utilities.eDirection io_hitDirection, ref float fixOffset)
//        {
//            bool isWallHit = false;

//            if (CurrentPosition.X >= GraphicsDevice.Viewport.Width - Texture.Width)
//            {
//                isWallHit = true;
//                io_hitDirection = Utilities.eDirection.Right;
//                fixOffset = GraphicsDevice.Viewport.Width - Texture.Width - CurrentPosition.X - 1;
//            }
//            else if (CurrentPosition.X <= 0)
//            {
//                isWallHit = true;
//                io_hitDirection = Utilities.eDirection.Left;
//                fixOffset = -1 * (CurrentPosition.X - 1);
//            }
//            else if (CurrentPosition.Y >= GraphicsDevice.Viewport.Height - Texture.Height)
//            {
//                isWallHit = true;
//                io_hitDirection = Utilities.eDirection.Down;
//            }
//            else
//            {
//                io_hitDirection = Utilities.eDirection.None;
//            }

//            return isWallHit;
//        }

//        // $G$ CSS-013 (0) Bad parameter names (should be in the form of i_PascalCase).
//        public override void Update(GameTime i_gameTime)
//        {
//            move(i_gameTime);
//            m_RandomShootingNotifier.Update(i_gameTime);
//            m_ShootingLogic.Update(i_gameTime);
//            base.Update(i_gameTime);
//        }

//        // $G$ CSS-013 (0) Bad parameter names (should be in the form of i_PascalCase).
//        public override void Draw(GameTime i_gameTime)
//        {
//            m_ShootingLogic.Draw(i_gameTime);

//            SpriteBatch.Begin();
//            SpriteBatch.Draw(Texture, CurrentPosition, Color);
//            SpriteBatch.End();
//        }

//        public void GetHit()
//        {
//            Souls--;
//        }

//        public bool IsDead()
//        {
//            return Souls == 0;
//        }

//        public void StepDown()
//        {
//            CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y + ((Texture.Height - 1) / 2));
//        }

//        // $G$ CSS-013 (0) Bad parameter names (should be in the form of i_PascalCase).
//        public void IncreaseVelocity(float i_velocityMultiplier)
//        {
//            Velocity *= i_velocityMultiplier;
//        }

//        public void Fire()
//        {
//            Vector2 initialPosition = new Vector2(CurrentPosition.X + Texture.Width / 2, CurrentPosition.Y);
//            m_ShootingLogic.Fire(GraphicsDevice, Content, initialPosition, TypeOfGameObject);
//        }

//        public List<Bullet> GetBulletsList()
//        {
//            return m_ShootingLogic.BulletsList;
//        }
//    }
//}
