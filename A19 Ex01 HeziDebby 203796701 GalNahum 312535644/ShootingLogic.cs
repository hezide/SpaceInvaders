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
    public class ShootingLogic
    {
        public List<Bullet>         BulletsList { get; private set; }
     //   private Gun                 m_gun;
      //  private GraphicsDevice      m_graphicsDevice;
      //  private ContentManager      m_content;

        public ShootingLogic()
        {
      //      m_graphicsDevice = i_graphicsDevice;
      //      m_content = i_content;
            BulletsList = new List<Bullet>();
        //    m_gun = new Gun(3); // HEZI: only the player should have max 3 bullets at a time 
        }

        // HEZI: i changed to void because u never user the bullet that returns 
        public void Fire(GraphicsDevice i_graphicsDevice, ContentManager i_content, Vector2 i_initialPosition, Utilities.eGameObjectType i_shooterType)
        {
            //    if (!m_gun.Fire())//cannot fire
            //        return null;

            Bullet bullet = SpaceInvadersFactory.CreateBullet(i_graphicsDevice, i_shooterType);
            bullet.Initialize(i_content);
          //  bullet.CurrentDirection = i_shootingDirection;
            bullet.InitPosition(i_initialPosition);
            BulletsList.Add(bullet);

        //    bullet.InitPosition(new Vector2(i_x, i_y));
        //    return bullet;
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
                if (bullet.IsOutOfSight())
                {
                    bulletsToRemove.Add(bullet);
                }
            }

            foreach (Bullet bullet in bulletsToRemove)
            {
                BulletsList.Remove(bullet);
         //       m_gun.Reload(1);
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
