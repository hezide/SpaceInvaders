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
    class Enemy : IMoveable, IDestryoable, Interfaces.IDrawable
    {
        public Vector2                          CurrentPosition { get; set; }
        public int                              Velocity { get; set; }
        public Texture2D                        Texture { get; set; }
        public Utilities.eDirection             CurrentDirection { get; set; }
        public Utilities.eDrawableType          Type { get; set; }
        private int                             m_ShootingFrequency;
        public SpriteBatch                      SpriteBatch { private get; set; }
        public Color                            Color { private get; set; }

        public void Init()
        {
            throw new NotImplementedException();
            //TODO: update shooting frequency
        }
        public void Init(
            Utilities.eDrawableType i_EnemyType,
            Texture2D i_EnemyTexture,
            Color i_Color,
            Vector2 i_Position,
            Utilities.eDirection i_Direction,
            int i_Velocity,
            SpriteBatch i_SpriteBatch)
        {
            Type = i_EnemyType;
            Texture = i_EnemyTexture;
            Color = i_Color;
            CurrentPosition = i_Position;
            Velocity = i_Velocity;
            CurrentDirection = i_Direction;
            SpriteBatch = i_SpriteBatch;
            
        }
        public void Update(GameTime i_GameTime)
        {
            CurrentPosition = new Vector2(CurrentPosition.X + (float)Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds,CurrentPosition.Y);
        }

        public void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, CurrentPosition, Color);
            SpriteBatch.End();

        }
    }
}
