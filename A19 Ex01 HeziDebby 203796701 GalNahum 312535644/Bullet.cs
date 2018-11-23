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
    class Bullet : IMoveable
    {
        public Vector2 InitialPosition { get; set; }
        public Vector2 CurrentPosition { get; set; }
        public float Velocity { get ; set ; }

        public Texture2D Texture { get; private set; }

        public Utilities.eDirection CurrentDirection { get; set; }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Move(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
