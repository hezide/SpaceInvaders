using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    class Enemy : IMoveable, IDestryoable
    {
        public Vector2              CurrentPosition { get; set; }
        public int                  Velocity { get; set; }
        public Texture2D            Texture { get; set; }
        public Utilities.eDirection CurrentDirection { get; set; }
        public Utilities.eType      Type { get; set; }
        private int                 m_ShootingFrequency;

        public void Init()
        {
            throw new NotImplementedException();
            //TODO: update shooting frequency
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
