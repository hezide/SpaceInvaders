using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces
{
    class ShootingLogic
    {
        public List<Bullet>         BulletsList { get; private set; }
        private Gun                 m_gun;
        private GraphicsDevice      m_graphicsDevice;
        private ContentManager      m_content;

        public ShootingLogic(GraphicsDevice i_graphicsDevice,ContentManager i_content)
        {
            m_graphicsDevice = i_graphicsDevice;
            m_content = i_content;
            BulletsList = new List<Bullet>();
            m_gun = new Gun(3);
        }

        public void Fire(float i_x, float i_y, Utilities.eDirection i_shootingDirection,Utilities.eShooterType i_shooterType)
        {
            if (!m_gun.Fire())//cannot fire
                return;

            Bullet bullet = SpaceInvadersFactory.CreateBullet(m_graphicsDevice, i_shooterType);
            bullet.Initialize(m_content);
            bullet.CurrentDirection = i_shootingDirection;
            BulletsList.Add(bullet);

            bullet.InitPosition(new Vector2(i_x, i_y));
            
        }

        private void updateBullets(GameTime i_gameTime)
        {
            foreach (Bullet bullet in BulletsList)
            {
                bullet.Update(i_gameTime);
            }

            updateBulletsList();
        }

        private void updateBulletsList()
        {
            List<Bullet> bulletsToRemove = new List<Bullet>();

            foreach (Bullet bullet in BulletsList)
            {
                if (bullet.CurrentPosition.Y <= m_graphicsDevice.Viewport.Y || !bullet.IsVisible)
                {
                    bulletsToRemove.Add(bullet);
                }
            }

            foreach (Bullet bullet in bulletsToRemove)
            {
                BulletsList.Remove(bullet);
                m_gun.Reload(1);
            }
        }

        public void Draw(GameTime i_gameTime)
        {
            foreach (Bullet bullet in BulletsList)
            {
                bullet.Draw(i_gameTime);
            }
        }

        public void Update(GameTime i_gameTime)
        {
            updateBullets(i_gameTime);
        }
    }
}
