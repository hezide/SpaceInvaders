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
    public class Bullet : IMoveable
    {
        public Vector2 CurrentPosition { get; set; }
        public int Velocity { get ; set ; }
        public Utilities.eDirection CurrentDirection { get; set; }
        public Texture2D Texture { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public Color Color { get; private set; }

        public Bullet(Texture2D i_texture, SpriteBatch i_spriteBatch, Color i_color)
        {
            Texture = i_texture;
            SpriteBatch = i_spriteBatch;
         //   CurrentPosition = i_initialPosition;
            Color = i_color;
        }

        public void Init(Vector2 i_initialPosition)
        {
            CurrentDirection = Utilities.eDirection.Up;
            Velocity = Utilities.k_BulletVelocity;
            CurrentPosition = i_initialPosition;
        }

        public void Move(GameTime i_gameTime)
        {
            CurrentPosition = new Vector2(CurrentPosition.X + (float)Velocity * (float)i_gameTime.ElapsedGameTime.TotalSeconds, CurrentPosition.Y);
        }

        public void Draw(GameTime i_gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, CurrentPosition, Color);

            SpriteBatch.End();
        }
    }
}
