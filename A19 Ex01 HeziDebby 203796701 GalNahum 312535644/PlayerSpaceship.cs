using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
//using Infrastructure;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    //  public class PlayerSpaceship : GameObject, IShooter, IDestryoable
    public class PlayerSpaceship : GameObject, IShooter, IDestryoable
    {
        public int Souls { get; set; }
        private MouseState? m_PrevMouseState;
        private KeyboardState m_PrevKeyboardState;
        private ShootingLogic m_ShootingLogic;
        private Texture2D               m_HeartTexture;

        public PlayerSpaceship(GraphicsDevice i_GraphicsDevice) : base(i_GraphicsDevice)
        {
        }

        public override void Initialize(ContentManager i_Content)
        {
            base.Initialize(i_Content);
            m_ShootingLogic = new ShootingLogic();
            CurrentDirection = Utilities.eDirection.Right;
            Velocity = Utilities.k_SpaceshipVelocity;
            Color = Color.White;
            Souls = Utilities.k_SpaceshipSouls;
            CurrentPosition = getInitialPosition();
        }

        private Vector2 getInitialPosition()
        {
            Vector2 initial = new Vector2(0, base.GraphicsDevice.Viewport.Height);

            initial.Y -= (Texture.Height / 2) * 1.5f;

            return initial;
        }

        protected override void LoadContent(ContentManager i_Content)
        {
            base.LoadContent(i_Content);

            Texture = i_Content.Load<Texture2D>(@"Sprites\Ship01_32x32");
            m_HeartTexture = i_Content.Load<Texture2D>(@"Sprites\heart");
        }

        private void updateByKeyboard(GameTime i_GameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float x = CurrentPosition.X;

            {
                if (m_PrevKeyboardState.IsKeyDown(Keys.Right))
                {
          //          CurrentDirection = Utilities.eDirection.Right;
                    x += Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
                }

                if (m_PrevKeyboardState.IsKeyDown(Keys.Left))
                {
          //          CurrentDirection = Utilities.eDirection.Left;
                    x -= Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
                }
                // $G$ SFN-003 (-2) Spaceship should shoot uppon 'Enter'
                if (keyboardState != m_PrevKeyboardState && m_PrevKeyboardState.IsKeyDown(Keys.Space))
                {
                    fire();
                }

                m_PrevKeyboardState = keyboardState;

                CurrentPosition = new Vector2(x, CurrentPosition.Y);
            }
        }

        private void fire()
        {
            if (m_ShootingLogic.BulletsList.Count < Utilities.k_Ammo) 
            {
                Vector2 initialPosition = new Vector2(CurrentPosition.X + Texture.Width / 2, CurrentPosition.Y);

                m_ShootingLogic.Fire(GraphicsDevice, Content, initialPosition, TypeOfGameObject);
            }
        }

        private void updateByMouse(GameTime i_GameTime)
        {
            MouseState currMouseState = Mouse.GetState();
            CurrentPosition = getNewPositionByInput(currMouseState);

            if (m_PrevMouseState != null)
            {
                if (m_PrevMouseState.Value.LeftButton == ButtonState.Pressed && currMouseState.LeftButton == ButtonState.Released)
                {
                    fire();
                }
            }

            m_PrevMouseState = currMouseState;
        }

        public override void Update(GameTime i_GameTime)
        {
            updateByKeyboard(i_GameTime);
            updateByMouse(i_GameTime);
            base.Update(i_GameTime);
            m_ShootingLogic.Update(i_GameTime);
        }

        public override void Draw(GameTime i_GameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, CurrentPosition, Color);
            for(int i = 0; i < Souls; i++)
            {
                SpriteBatch.Draw(m_HeartTexture, new Vector2(Utilities.k_heartStartingLocationX + m_HeartTexture.Width * 2 * i,Utilities.k_heartStartingLocationY), Color.White);
            }
            SpriteBatch.End();

            m_ShootingLogic.Draw(i_GameTime);
        }

        private Vector2 getNewPositionByInput(MouseState i_currMouseState)
        {
            Vector2 newPosition = new Vector2(CurrentPosition.X + getMousePositionDeltaX(i_currMouseState), CurrentPosition.Y);

            return new Vector2(MathHelper.Clamp(newPosition.X, 0, GraphicsDevice.Viewport.Width - Texture.Width), newPosition.Y);
        }

        private float getMousePositionDeltaX(MouseState i_currMouseState)
        {
            float x = 0;

            if (m_PrevMouseState != null)
            {
                x = (i_currMouseState.X - m_PrevMouseState.Value.X);
            }

            return x;
        }

  //      public void IsHit(GameObject i_gameObject)
   //     {
    //        Souls--;
    //    }

        public List<Bullet> GetBulletsList()
        {
            return m_ShootingLogic.BulletsList;
        }

        public void GetHit()
        {
            Souls--;
            CurrentPosition = getInitialPosition();
        }

        public bool IsDead()
        {
            return Souls == 0;
        }
    }
}
