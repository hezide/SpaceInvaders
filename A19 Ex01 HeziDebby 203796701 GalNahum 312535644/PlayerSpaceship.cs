using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class PlayerSpaceship : GameObject, IShooter
    {
     //   public Vector2 CurrentPosition { get; set; }
      //  public int Velocity { get; set; }
     //   public Utilities.eDirection CurrentDirection { get; set; }
     //   public Texture2D Texture { get; private set; }
     //   public Color Color { get; private set; }
        public int Souls { get; set; }
        private MouseState? m_prevMouseState;
        private KeyboardState m_prevKeyboardState;
        public List<Bullet> BulletsList { get; private set; }

        public PlayerSpaceship(GraphicsDevice i_graphicsDevice) : base(i_graphicsDevice)
        {
            BulletsList = new List<Bullet>();
        }

        public override void Initialize(ContentManager i_content)
        {
            base.Initialize(i_content);

            CurrentDirection = Utilities.eDirection.Right;
            Velocity = Utilities.k_SpaceshipVelocity;
            Color = Color.White;
            Souls = Utilities.k_SpaceshipSouls;
            CurrentPosition = getInitialPosition();
        }

        private Vector2 getInitialPosition()
        {
            Vector2 center = new Vector2(base.GraphicsDevice.Viewport.Width / 2, base.GraphicsDevice.Viewport.Height);

            center.X -= Texture.Width / 2;
            center.Y -= (Texture.Height / 2) * 1.5f;

            return center;
        }

        protected override void LoadContent(ContentManager i_content)
        {
            base.LoadContent(i_content);

            Texture = i_content.Load<Texture2D>(@"Sprites\Ship01_32x32");
        }

        private void updateByKeyboard(GameTime i_gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            float x = CurrentPosition.X;

            {
                if (m_prevKeyboardState.IsKeyDown(Keys.Right))
                {
                    CurrentDirection = Utilities.eDirection.Right;
                    x += (float)Velocity * (float)i_gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (m_prevKeyboardState.IsKeyDown(Keys.Left))
                {
                    CurrentDirection = Utilities.eDirection.Left;
                    x -= (float)Velocity * (float)i_gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (keyboardState != m_prevKeyboardState && m_prevKeyboardState.IsKeyDown(Keys.Space))
                {
                    if (BulletsList.Count < 3) // TODO: duplication1
                    {
                        Fire();
                    }

                }

                m_prevKeyboardState = keyboardState;

                CurrentPosition = new Vector2(x, CurrentPosition.Y);
            }
        }

        private void updateByMouse(GameTime i_gameTime)
        {
            MouseState currMouseState = Mouse.GetState();

            CurrentPosition = getNewPositionByInput(currMouseState);

            if (m_prevMouseState != null)
            {
                if (m_prevMouseState.Value.LeftButton == ButtonState.Pressed && currMouseState.LeftButton == ButtonState.Released)
                {
                    if (BulletsList.Count < 3) // TODO: duplication1
                    {
                        Fire();
                    }
                }
            }

            m_prevMouseState = currMouseState;
        }

        public override void Update(GameTime i_gameTime)
        {
            updateByKeyboard(i_gameTime);
            updateByMouse(i_gameTime);
            base.Update(i_gameTime);
            updateBullets(i_gameTime);
        }

        private void updateBullets(GameTime i_gameTime)
        {
            foreach (Bullet bullet in BulletsList)
            {
                bullet.Update(i_gameTime);
            }

            updateBulletsList();
        }

        private void updateBulletsList()
        {
            List<Bullet> bulletsToRemove = new List<Bullet>();

            foreach (Bullet bullet in BulletsList)
            {
                if (bullet.CurrentPosition.Y <= base.GraphicsDevice.Viewport.Y || !bullet.IsVisible)
                {
                    bulletsToRemove.Add(bullet);
                }

            }

            foreach (Bullet bullet in bulletsToRemove)
            {
                BulletsList.Remove(bullet);
            }
        }

        public override void Draw(GameTime i_gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, CurrentPosition, Color);

            SpriteBatch.End();

            foreach (Bullet bullet in BulletsList)
            {
                bullet.Draw(i_gameTime);
            }
        }

        public void Fire()
        {
            Bullet bullet = SpaceInvadersFactory.CreateBullet(base.GraphicsDevice);

            bullet.Initialize(Content);
            BulletsList.Add(bullet);

            Vector2 bulletPosition = new Vector2(CurrentPosition.X - Texture.Width / 2, CurrentPosition.Y);

            bullet.InitPosition(CurrentPosition);
        }

        private Vector2 getNewPositionByInput(MouseState i_currMouseState)
        {
            Vector2 newPosition = new Vector2(CurrentPosition.X + getMousePositionDeltaX(i_currMouseState), CurrentPosition.Y);

            return (new Vector2(MathHelper.Clamp(newPosition.X, 0, base.GraphicsDevice.Viewport.Width - Texture.Width), newPosition.Y));
        }

        private float getMousePositionDeltaX(MouseState i_currMouseState)
        {
            float x = 0;

            if (m_prevMouseState != null)
            {
                x = (i_currMouseState.X - m_prevMouseState.Value.X);
            }

            return x;
        }

        public void IsHit(GameObject i_gameObject)
        {
            Souls--;
        }
    }
}
