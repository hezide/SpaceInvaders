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
    public class PlayerSpaceship : GameObject, IShooter, IDestryoable
    {
        public int Souls { get; set; }
        private MouseState? m_prevMouseState;
        private KeyboardState m_prevKeyboardState;
        private ShootingLogic m_shootingLogic;
        //public Utilities.eShooterType   ShooterType { get; set; }
        public Action<Bullet> ShotFired { get; set; }

        public PlayerSpaceship(GraphicsDevice i_graphicsDevice) : base(i_graphicsDevice)
        {
        }

        public override void Initialize(ContentManager i_content)
        {
            base.Initialize(i_content);
            m_shootingLogic = new ShootingLogic();
            CurrentDirection = Utilities.eDirection.Right;
            Velocity = Utilities.k_SpaceshipVelocity;
            Color = Color.White;
            Souls = Utilities.k_SpaceshipSouls;
            CurrentPosition = getInitialPosition();
        }

        private Vector2 getInitialPosition()
        {
            Vector2 center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height);

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
                    fire();

                    //ShotFired.Invoke(bullet);
                }

                m_prevKeyboardState = keyboardState;

                CurrentPosition = new Vector2(x, CurrentPosition.Y);
            }
        }

        private void fire()
        {
            if (m_shootingLogic.BulletsList.Count < Utilities.k_Ammo) // TODO: readonly
            {
                Vector2 initialPosition = new Vector2(CurrentPosition.X - Texture.Width / 2, CurrentPosition.Y);
                m_shootingLogic.Fire(GraphicsDevice, Content, initialPosition, TypeOfGameObject);
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
                    fire();
                    //Bullet bullet = m_shootingLogic.Fire(CurrentPosition.X - Texture.Width / 2, CurrentPosition.Y, Utilities.eDirection.Up, TypeOfGameObject);
                    // ShotFired.Invoke(bullet);
                }
            }

            m_prevMouseState = currMouseState;
        }

        public override void Update(GameTime i_gameTime)
        {
            updateByKeyboard(i_gameTime);
            updateByMouse(i_gameTime);
            base.Update(i_gameTime);
            m_shootingLogic.Update(i_gameTime);
        }

        public override void Draw(GameTime i_gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, CurrentPosition, Color);

            SpriteBatch.End();

            m_shootingLogic.Draw(i_gameTime);
        }

        private Vector2 getNewPositionByInput(MouseState i_currMouseState)
        {
            Vector2 newPosition = new Vector2(CurrentPosition.X + getMousePositionDeltaX(i_currMouseState), CurrentPosition.Y);

            return (new Vector2(MathHelper.Clamp(newPosition.X, 0, GraphicsDevice.Viewport.Width - Texture.Width), newPosition.Y));
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

        public List<Bullet> GetBulletsList()
        {
            return m_shootingLogic.BulletsList;
        }

        public void GetHit()
        {
            Souls--;
        }

        public bool IsDead()
        {
            return Souls == 0;
        }
    }
}
