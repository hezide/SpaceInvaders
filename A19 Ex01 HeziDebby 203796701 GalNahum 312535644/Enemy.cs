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
        public int Souls { get; set; }
        public List<Bullet> BulletsList { get; private set; }
        private float m_timeToJump = 0;
        private RandomActionComponent m_randomShootingNotifier;
        private ShootingLogic m_shootingLogic;
        //public Utilities.eShooterType                   ShooterType { get; set; }
        public Action<Bullet> ShotFired { get; set; }

        public Enemy(GraphicsDevice i_graphics, int i_randomSeed) : base(i_graphics)
        {
            BulletsList = new List<Bullet>();
            m_randomShootingNotifier = new RandomActionComponent(1, 30, i_randomSeed);
            m_randomShootingNotifier.RandomTimeAchieved += Fire;
        }

        public override void Initialize(ContentManager i_content)
        {
            base.Initialize(i_content);
            m_shootingLogic = new ShootingLogic();
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

            CurrentPosition = new Vector2(x + 1, y + 1);
        }

        protected override void LoadContent(ContentManager i_content)
        {
            base.LoadContent(i_content);

            string folder = @"Sprites\";
            string enemy = String.Format("Enemy0{0}01_32x32", (int)TypeOfGameObject + 1);
            Texture = Content.Load<Texture2D>(folder + enemy);
        }

        private void move(GameTime i_gameTime) // need to be jump
        {
            float newX, newY;

            newX = Utilities.CalculateXToMove(CurrentPosition.X, CurrentDirection, Velocity, i_gameTime);
            newY = CurrentPosition.Y;

            CurrentPosition = new Vector2(newX, newY);
        }

        public bool IsWallHit(ref Utilities.eDirection io_hitDirection)
        {
            bool isWallHit = false;

            //  if (CurrentPosition.X >= Utilities.k_ScreenWidth - Texture.Width)\
            if (CurrentPosition.X >= GraphicsDevice.Viewport.Width - Texture.Width)
            {
                isWallHit = true;
                io_hitDirection = Utilities.eDirection.Right;
            }
            else if (CurrentPosition.X <= 0)
            {
                isWallHit = true;
                io_hitDirection = Utilities.eDirection.Left;
            }
            else if (CurrentPosition.Y >= GraphicsDevice.Viewport.Height - Texture.Height)
            {
                isWallHit = true;
                io_hitDirection = Utilities.eDirection.Down;
            }
            else
            {
                io_hitDirection = Utilities.eDirection.None;
            }

            return isWallHit;
        }

        public override void Update(GameTime i_gameTime)
        {
            move(i_gameTime);
            m_randomShootingNotifier.Update(i_gameTime);
            m_shootingLogic.Update(i_gameTime);
            base.Update(i_gameTime);
        }
        public override void Draw(GameTime i_gameTime)
        {
            m_shootingLogic.Draw(i_gameTime);
            base.Draw(i_gameTime);
        }

        //public override void Draw(GameTime i_gameTime)
        //{
        //    if (m_timeToJump >= k_jumpIndicator)
        //    {
        //        m_timeToJump -= k_jumpIndicator;

        //        SpriteBatch.Begin();

        //        SpriteBatch.Draw(Texture, CurrentPosition, Color);

        //        SpriteBatch.End();
        //    }

        //    m_timeToJump += (float)i_gameTime.ElapsedGameTime.TotalSeconds;
        //}

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
            CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y + ((Texture.Height - 1) / 2));
        }

        public void IncreaseVelocity(float i_velocityMultiplier)
        {
            Velocity *= i_velocityMultiplier;
        }

        public void Fire()
        {
            Vector2 initialPosition = new Vector2(CurrentPosition.X - Texture.Width / 2, CurrentPosition.Y);
            m_shootingLogic.Fire(GraphicsDevice, Content, initialPosition, TypeOfGameObject);
            // ShotFired.Invoke(bullet);
        }

        public List<Bullet> GetBulletsList()
        {
            return m_shootingLogic.BulletsList;
        }
    }
}
