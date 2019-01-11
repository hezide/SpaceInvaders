using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    // TODO: is this sprite collection ?
    public class Gun
    {
        private List<Bullet> m_BulletsList;
        private int m_Ammo;
        public bool Enable { get; set; } = true;

        public Gun(int i_Ammo, Game i_Game, Type i_OwnerType)
        {
            m_Ammo = i_Ammo;
            m_BulletsList = new List<Bullet>();

            allocateBullets(i_Game, i_OwnerType);
        }

        private void allocateBullets(Game i_Game, Type i_OwnerType)
        {
            for (int i = 0; i < m_Ammo; i++)
            {
                m_BulletsList.Add(new Bullet(i_Game) { OwnerType = i_OwnerType });
            }
        }

        public void Initialize(Color i_TintColor, int i_DirectionMultiplier = 1)
        {
            foreach (Bullet bullet in m_BulletsList)
            {
                bullet.Velocity *= i_DirectionMultiplier;
                bullet.TintColor = i_TintColor;
            }
        }

        public void Shoot(Vector2 i_InitialPosition)
        {
            bool shot = false;

            if (this.Enable)
            {
                foreach (Bullet bullet in m_BulletsList)
                {
                    if (!bullet.Visible && !shot)
                    {
                        shot = true;
                        bullet.Position = i_InitialPosition;
                        bullet.Visible = true;
                    }
                }
            }
        }

        public void AddCollisionListener(EventHandler i_CollisionHandler)
        {
            foreach(Bullet bullet in m_BulletsList)
            {
                bullet.Collision += new EventHandler<EventArgs>(i_CollisionHandler);
            }
        }
    }
}
