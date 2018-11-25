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
        private double                                  m_secondsFromLastJump;
        private RandomActionComponent                   m_randomShootingNotifier;
        private ShootingLogic                           m_shootingLogic;
        private Vector2                                 m_lastDrawnPosition;

        public Enemy(GraphicsDevice i_graphics,int i_randomSeed) : base(i_graphics)
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
            float height = Texture.Height;
            float width = Texture.Width;

            float x = i_col * width + width * Utilities.k_EnemyGapMultiplier * i_col;
            float y = (i_row * height + height * Utilities.k_EnemyGapMultiplier * i_row) + Utilities.k_InitialHightMultiplier * height;

            CurrentPosition = new Vector2(x + 1, y + 1);
            m_lastDrawnPosition = CurrentPosition;
        }

        protected override void LoadContent(ContentManager i_content)
        {
            base.LoadContent(i_content);

            string folder = @"Sprites\";
            string enemy = String.Format("Enemy0{0}01_32x32", (int)TypeOfGameObject + 1);
            Texture = Content.Load<Texture2D>(folder + enemy);
        }

        private void move(GameTime i_gameTime) 
        {
            float newX = Utilities.CalculateNewCoordinate(CurrentPosition.X,CurrentDirection, Velocity,i_gameTime);
            float newY = CurrentPosition.Y;

            CurrentPosition = new Vector2(newX, newY);
        }

        public bool IsWallHit(ref Utilities.eDirection io_hitDirection)
        {
            bool isWallHit = false;

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
            m_secondsFromLastJump += i_gameTime.ElapsedGameTime.TotalSeconds;

            m_shootingLogic.Draw(i_gameTime);

            SpriteBatch.Begin();
            if (m_secondsFromLastJump > 0.5)
            {
                m_secondsFromLastJump = 0;
                m_lastDrawnPosition = CurrentPosition;
                SpriteBatch.Draw(Texture, CurrentPosition, Color);
            }
            else
            {
                SpriteBatch.Draw(Texture, m_lastDrawnPosition, Color);
            }
            SpriteBatch.End();
        }

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
        }

        public List<Bullet> GetBulletsList()
        {
            return m_shootingLogic.BulletsList;
        }
    }
}
