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
}
//    public class ShootingLogic
//    {
//        public List<Bullet>         BulletsList { get; private set; }

//        public ShootingLogic()
//        {
//            BulletsList = new List<Bullet>();
//        }

//        public void Fire(GraphicsDevice i_GraphicsDevice, ContentManager i_Content, Vector2 i_initialPosition, Utilities.eGameObjectType i_shooterType)
//        {
//            // $G$ XNA-001 (-5) Its better to "recycle" the mothership instead of creating one every for shot..
//            Bullet bullet = SpaceInvadersFactory.CreateBullet(i_GraphicsDevice, i_shooterType);
//            bullet.Initialize(i_Content);
//            bullet.InitPosition(i_initialPosition);
//            BulletsList.Add(bullet);
//        }

//        private void updateBullets(GameTime i_GameTime)
//        {
//            foreach (Bullet bullet in BulletsList)
//            {
//                bullet.Update(i_GameTime);
//            }

//            updateBulletsList();
//        }

//        private void updateBulletsList()
//        {
//            List<Bullet> bulletsToRemove = new List<Bullet>();

//            foreach (Bullet bullet in BulletsList)
//            {
//                if (bullet.IsOutOfSight())
//                {
//                    bulletsToRemove.Add(bullet);
//                }
//            }

//            foreach (Bullet bullet in bulletsToRemove)
//            {
//                BulletsList.Remove(bullet);
//            }
//        }

//        public void Draw(GameTime i_GameTime)
//        {
//            foreach (Bullet bullet in BulletsList)
//            {
//                bullet.Draw(i_GameTime);
//            }
//        }

//        public void Update(GameTime i_GameTime)
//        {
//            updateBullets(i_GameTime);
//        }
//    }
//}
