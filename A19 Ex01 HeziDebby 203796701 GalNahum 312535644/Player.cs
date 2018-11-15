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
    class Player : IMoveable
    {
        public Vector2 InitialPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 CurrentPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Texture2D Texture { get; set; }

        public Utilities.eDirection CurrentDirection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int Souls { get; set; }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
