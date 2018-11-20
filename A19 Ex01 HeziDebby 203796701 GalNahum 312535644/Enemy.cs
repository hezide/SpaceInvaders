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
    public class Enemy : IMoveable, IDestryoable
    {
        public Vector2 CurrentPosition { get; set; }
        public int Velocity { get; set; }
        public Texture2D Texture { get; set; }
        public Utilities.eDirection CurrentDirection { get; set; }
        public Utilities.eDrawableType Type { get; set; }
        private int m_ShootingFrequency;
        public SpriteBatch SpriteBatch { get; set; }
        public Color Color { get; private set; }
        public int Souls { get; set; }

        public Enemy(Texture2D i_texture, SpriteBatch i_spriteBatch, Color i_color)
        {
            Texture = i_texture;
            SpriteBatch = i_spriteBatch;
          //  CurrentPosition = i_initialPosition;
            Color = i_color;
        }

        public void Init(Vector2 i_initialPosition)
        {
            CurrentDirection = Utilities.eDirection.Right;
            Velocity = Utilities.k_EnemyVelocity;
            Souls = Utilities.k_EnemySouls;
            CurrentPosition = i_initialPosition;
        }

        public void Move(GameTime i_gameTime) // need to be jump
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
