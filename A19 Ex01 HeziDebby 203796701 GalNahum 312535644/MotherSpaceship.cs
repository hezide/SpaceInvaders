﻿using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    class MotherSpaceship : IMoveable, IDestryoable
    {
        public Vector2 InitialPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 CurrentPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Velocity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Texture2D Texture { get; set; }

        public Utilities.eDirection CurrentDirection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Souls { get; set; }
        public SpriteBatch SpriteBatch { set => throw new NotImplementedException(); }

        public Color Color => throw new NotImplementedException();

        SpriteBatch Interfaces.IDrawable.SpriteBatch => throw new NotImplementedException();

        public void Draw(GameTime i_gameTime)
        {
            throw new NotImplementedException();
        }
        
        public void Init(Vector2 i_initialPosition)
        {
            throw new NotImplementedException();
        }

        public void Move(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
