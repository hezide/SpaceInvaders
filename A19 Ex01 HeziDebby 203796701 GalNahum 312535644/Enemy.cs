using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class Enemy : GameObject, IDestryoable, IShooter
    {
        public int                                      Souls { get; set; }
        public List<Bullet>                             BulletsList { get; private set; }
        private float                                   m_timeToJump = 0;
        private RandomActionComponent                   m_randomShootingNotifier;
        private ShootingLogic                           m_shootingLogic;
        //public Utilities.eShooterType                   ShooterType { get; set; }
        public Action<Bullet>                           ShotFired { get; set; }

        public Enemy(GraphicsDevice i_graphics,int i_randomSeed) : base(i_graphics)
        {
            BulletsList = new List<Bullet>();
            m_randomShootingNotifier = new RandomActionComponent(1, 30, i_randomSeed);
            m_randomShootingNotifier.RandomTimeAchived += Fire;
        }

        public override void Initialize(ContentManager i_content)
        {
            base.Initialize(i_content);
            m_shootingLogic = new ShootingLogic(base.GraphicsDevice, base.Content);
            CurrentDirection = Utilities.eDirection.Right;
            Velocity = Utilities.k_EnemyVelocity;
            Souls = Utilities.k_EnemySouls;

        }

        public void InitPosition(int i_row, int i_col)
        {
            float x;
            float y;
            float height = Texture.Height;
            float width = Texture.Width;

            x = i_col * width + width * Utilities.k_EnemyGapMultiplier * i_col;
            y = (i_row * height + height * Utilities.k_EnemyGapMultiplier * i_row) + Utilities.k_InitialHightMultiplier * height;

            CurrentPosition = new Vector2(x+1, y+1);
        }

        protected override void LoadContent(ContentManager i_content)
        {
            base.LoadContent(i_content);

          string folder = @"Sprites\";
          string enemy = String.Format("Enemy0{0}01_32x32", (int)Type + 1);
          Texture = Content.Load<Texture2D>(folder + enemy);

        }

        private void Move(GameTime i_gameTime) // need to be jump
        {
            float newX, newY;

            newX = Utilities.CalculateNewCoordinate(CurrentPosition.X,CurrentDirection, Velocity,i_gameTime);
            newY = CurrentPosition.Y;

            CurrentPosition = new Vector2(newX, newY);
        }
        //Can delete if the enemies are not getting away from the screen
        //private Vector2 getScreenBoundriesScaleAmount(float i_xToScale, float i_yToScale)
        //{
        //    float xScale = 0, yScale = 0;

        //    if (i_xToScale + Texture.Width > Utilities.k_ScreenWidth)
        //        xScale = i_xToScale + Texture.Width - Utilities.k_ScreenWidth-1;

        //    else if (i_xToScale < 0)
        //    {
        //        xScale = i_xToScale+1;
        //    }
        //    if (i_yToScale + Texture.Height >= Utilities.k_ScreenHeight)
        //        yScale = i_yToScale + Texture.Height - Utilities.k_ScreenHeight ;

        //    return new Vector2(xScale, yScale);
        //}

        internal void IsWallHit(ref bool isWalHit, ref Utilities.eDirection hitDirection)
        {
            if (CurrentPosition.X >= Utilities.k_ScreenWidth - Texture.Width)
            {
                isWalHit = true;
                hitDirection = Utilities.eDirection.Right;
            }
            else if (CurrentPosition.X <= 0)
            {
                isWalHit = true;
                hitDirection = Utilities.eDirection.Left;
            }
            else if (CurrentPosition.Y >= Utilities.k_ScreenHeight - Texture.Height)
            {
                isWalHit = true;
                hitDirection = Utilities.eDirection.Down;
            }
            else
            {
                isWalHit = false;
                hitDirection = Utilities.eDirection.None;
            }

        }

        public override void Update(GameTime i_gameTime)
        {
            Move(i_gameTime);
            m_randomShootingNotifier.Update(i_gameTime);
            m_shootingLogic.Update(i_gameTime);
            base.Update(i_gameTime);
        }
        public override void Draw(GameTime i_gameTime)
        {
            m_shootingLogic.Draw(i_gameTime);
            base.Draw(i_gameTime);
        }
        //private void updateRectangle()
        //{
        //    Rectangle rectangle = new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, Texture.Width, Texture.Height);

        //    Rectangle = rectangle;
        //}

        //     public override void Draw(GameTime i_gameTime)
        //   {
        //     if (m_timeToJump >= k_jumpIndicator)
        //     {
        //         m_timeToJump -= k_jumpIndicator;

        //SpriteBatch.Begin();

        //SpriteBatch.Draw(Texture, CurrentPosition, Color);

        //SpriteBatch.End();
        //    }

        //    m_timeToJump += (float)i_gameTime.ElapsedGameTime.TotalSeconds;
        //     }

        public void GetHit()
        {
            Souls--;
        }

        public bool IsDead()
        {
            return Souls == 0;
        }

        public void StepDown()
        {
            CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y + ((Texture.Height-1) / 2));
        }

        public void IncreaseVelocity(float i_velocityMultiplier)
        {
            Velocity *= i_velocityMultiplier;
        }

        public void Fire()
        {
            Bullet bullet = m_shootingLogic.Fire(CurrentPosition.X - Texture.Width / 2, CurrentPosition.Y,Utilities.eDirection.Down,Type);
           // ShotFired.Invoke(bullet);
        }

        public List<Bullet> GetBulletsList()
        {
            return m_shootingLogic.BulletsList;
        }
    }
}
