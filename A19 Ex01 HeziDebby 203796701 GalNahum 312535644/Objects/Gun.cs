using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{// TODO: should this be in infrastructure ?
    // TODO: is this sprite collection ?
    public class Gun
    {
        public List<Bullet> BulletsList { get; private set; }
        public int Ammo { get; private set; }

        public Gun(int i_Ammo, Game i_Game, Type i_OwnerType)
        {
            Ammo = i_Ammo;
            BulletsList = new List<Bullet>();

            allocateBullets(i_Game, i_OwnerType);
        }

        private void allocateBullets(Game i_Game, Type i_OwnerType)
        {
           // Bullet bulletToAdd;

            for (int i = 0; i < Ammo; i++)
            {
                BulletsList.Add(new Bullet(i_Game) { OwnerType = i_OwnerType });

                //bulletToAdd = new Bullet(i_Game) { OwnerType = i_OwnerType };
                //BulletsList.Add(bulletToAdd);
            }
        }

        public void Initialize(Color i_TintColor, int i_DirectionMultiplier = 1)
        {
            foreach (Bullet bullet in BulletsList)
            {
                bullet.Velocity *= i_DirectionMultiplier;
                bullet.TintColor = i_TintColor;
            }
        }

        private bool m_Enable = true;
        public bool Enable
        {
            get { return m_Enable; }
            set { m_Enable = value; }
        }

        public void Shoot(Vector2 i_InitialPosition)
        {
            bool shot = false;

            if (this.Enable)
            {
                foreach (Bullet bullet in BulletsList)
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
            foreach(Bullet bullet in BulletsList)
            {
                bullet.Collision += new EventHandler<EventArgs>(i_CollisionHandler);
            }
        }
    }
}
