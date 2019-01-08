﻿using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using System;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class Enemy : Sprite, ICollidable2D //, IShooter
    {
        private const int k_TextureWidthDivider = 2;
        private const int k_TextureHeightDivider = 3;
        private const int k_NumOfFrames = 2;

        private const int k_Velocity = 120;
        private const int k_MaxAmmo = 1;
        private RandomActionComponent m_RandomShootingNotifier;

        private Gun Gun;
        public int Seed { get; set; } // TODO: not initialized

        public Enemy(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
        {
            m_TintColor = Color.Red;
            Gun = new Gun(k_MaxAmmo, i_Game, this.GetType());
        }

        public override void Initialize()
        {
            base.Initialize();
            Velocity = new Vector2(k_Velocity, 0);

            Gun.Initialize(Color.Blue);

            m_RandomShootingNotifier = new RandomActionComponent(1, 30, Seed);
            m_RandomShootingNotifier.RandomTimeAchieved += Shoot;

            initAnimations();
        }
        // TODO: set compositeAnimator for dyingAnimaions etc
        private void initAnimations()
        {
            TimeSpan animationLength = TimeSpan.FromSeconds(0.5);

           CellAnimator celAnimation = new CellAnimator(animationLength, k_NumOfFrames, (int)CellIdx.Y, TimeSpan.Zero);
           // BlinkAnimator blinker = new BlinkAnimator(TimeSpan.FromSeconds(0.5), TimeSpan.Zero);
            //      WaypointsAnimator waypoints = new WaypointsAnimator(this.Velocity.X, TimeSpan.Zero, false, ) 
             JumpAnimator jumper = new JumpAnimator(new Vector2(this.Width / 2, this.Position.Y), GraphicsDevice.Viewport.Bounds, animationLength, TimeSpan.Zero);

            float spinsPerSecond = MathHelper.TwoPi * 6;
            animationLength = TimeSpan.FromSeconds(1.2);

            SpinAnimator spinner = new SpinAnimator("Spinner", spinsPerSecond, animationLength);
            ShrinkAnimator shrinker = new ShrinkAnimator("Shrinker", animationLength);

            spinner.Finished += new EventHandler(onDyingAnimationFinish);

            this.Animations.Add(celAnimation);
            //  this.Animations.Add(blinker);
            this.Animations.Add(jumper);
            this.Animations.Add(spinner);
            this.Animations.Add(shrinker);

            this.Animations.Enabled = true;
            this.Animations["Spinner"].Enabled = false;
            this.Animations["Shrinker"].Enabled = false;
        }

        private void onDyingAnimationFinish(object i_Sender, EventArgs i_EventArgs)
        {
            this.Dispose(true);
            this.Animations.Pause();
            this.Visible = false;
        }

        private Vector2 m_CellIdx = Vector2.Zero;
        public Vector2 CellIdx
        {
            get { return m_CellIdx; }
            set { m_CellIdx = value; }
        }

        protected override void InitBounds()
        {
            m_WidthBeforeScale = Texture.Width / k_TextureWidthDivider;
            m_HeightBeforeScale = Texture.Height / k_TextureHeightDivider;

            InitSourceRectangle();

            InitOrigins();
        }

        protected override void InitSourceRectangle()
        {
            base.InitSourceRectangle();

            this.SourceRectangle = new Rectangle(
                (int)CellIdx.Y * m_SourceRectangle.Height,
                (int)CellIdx.X * m_SourceRectangle.Width,
                (int)(m_WidthBeforeScale),
                (int)(m_HeightBeforeScale));
        }

        protected override void InitOrigins()
        {
            this.RotationOrigin = SourceRectangleCenter;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            //base.Collided(i_Collidable);
            IsCollided = true; // TODO: check this boolean .... what it does

            //if (i_Collidable is ICollidable2D &&
            //    !(i_Collidable as ICollidable2D).PixelsCollidable)
            if (!(i_Collidable is ICllidableByPixels))
            {
                this.Animations["Spinner"].Enabled = true;
                this.Animations["Shrinker"].Enabled = true;
            }

        }

        public override void Update(GameTime gameTime)
        { // TODO: check if i can add as game service (Iupdatable ? )
            m_RandomShootingNotifier.Update(gameTime);

            base.Update(gameTime);
        }

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