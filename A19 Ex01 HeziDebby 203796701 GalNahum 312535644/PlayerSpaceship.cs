using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class PlayerSpaceship : IMoveable, IShooter, IDestryoable
    {
        public Vector2 CurrentPosition { get; set; }
        public int Velocity { get; set; }
        public Utilities.eDirection CurrentDirection { get; set; }
        public Texture2D Texture { get; private set; }
        public SpriteBatch SpriteBatch { get; set; }
        public Color Color { get; private set; }
        public int Souls { get; set; }
 
        public PlayerSpaceship(Texture2D i_texture, SpriteBatch i_spriteBatch, Color i_color)
        {
            Texture = i_texture;
            SpriteBatch = i_spriteBatch;
        //    CurrentPosition = i_initialPosition;
            Color = i_color;
        }

        public void Init(Vector2 i_initialPosition)
        {
            CurrentDirection = Utilities.eDirection.Right;
            Velocity = Utilities.k_SpaceshipVelocity;
            Souls = Utilities.k_SpaceshipSouls;
            CurrentPosition = i_initialPosition;
        }

        public void Move(GameTime i_gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float x = CurrentPosition.X;

            // Move our sprite based on arrow keys being pressed:
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                CurrentDirection = Utilities.eDirection.Right;
                x += (float)Velocity * (float)i_gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                CurrentDirection = Utilities.eDirection.Left;
                x -= (float)Velocity * (float)i_gameTime.ElapsedGameTime.TotalSeconds;
            }
    
            CurrentPosition = new Vector2(x, CurrentPosition.Y);
        }

        public void Draw(GameTime i_gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, CurrentPosition, Color);

            SpriteBatch.End();
        }

        public void Fire(Bullet i_bullet)
        {
  
        }
    }
}
