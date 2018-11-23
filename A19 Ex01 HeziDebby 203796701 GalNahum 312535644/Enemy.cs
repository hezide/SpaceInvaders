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
    class Enemy : IMoveable, IDestryoable, Interfaces.IDrawable, IShooter
    {
        public Vector2                                  CurrentPosition { get; set; }
        public float                                    Velocity { get; set; }
        public Texture2D                                Texture { get; set; }
        public Utilities.eDirection                     CurrentDirection { get; set; }
        public Utilities.eDrawableType                  Type { get; set; }
        private int                                     m_ShootingFrequency;
        public SpriteBatch                              SpriteBatch { private get; set; }
        public Color                                    Color { private get; set; }
        public Rectangle                                ScreenBoundries { private get; set; }

        public event EventHandler<Utilities.eDirection> WallWasHit;

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
            SpriteBatch i_SpriteBatch,
            Rectangle i_ScreenBoundries)
        {
            Type = i_EnemyType;
            Texture = i_EnemyTexture;
            Color = i_Color;
            CurrentPosition = i_Position;
            Velocity = i_Velocity;
            CurrentDirection = i_Direction;
            SpriteBatch = i_SpriteBatch;
            ScreenBoundries = i_ScreenBoundries;

            Random rnd = new Random();
            m_ShootingFrequency = rnd.Next(1, 10);//todo make sure its ok
        }
        public void Move(GameTime i_GameTime)
        {
            if (CurrentPosition.X >= ScreenBoundries.Width - Texture.Width)
                WallWasHit.Invoke(this, Utilities.eDirection.Right);
            if (CurrentPosition.X <= 0)
                WallWasHit.Invoke(this, Utilities.eDirection.Left);
            if (CurrentPosition.Y >= ScreenBoundries.Height - Texture.Height)
                WallWasHit.Invoke(this, Utilities.eDirection.Down);

            //Direction left is -1 and right is +1, this makes the enemies jump e
            CurrentPosition = new Vector2(CurrentPosition.X + ((float)CurrentDirection) * (Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds), CurrentPosition.Y);
        }

        public void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, CurrentPosition, Color);
            SpriteBatch.End();
        }

        public void StepDown()
        {
            CurrentPosition = new Vector2(CurrentPosition.X, CurrentPosition.Y + (Texture.Height / 2));
        }

        public void IncreaseVelocity(float i_VelocityMultiplier)
        {
            Velocity *= i_VelocityMultiplier;
        }

    }
}
