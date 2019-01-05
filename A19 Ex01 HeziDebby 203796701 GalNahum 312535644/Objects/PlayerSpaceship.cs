using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class PlayerSpaceship : Sprite, ICollidable2D //, IShooter
    {
        public enum eActivationKey { Left, Right, Shoot };

        private const string k_AssetName = @"Sprites\Ship01_32x32";
        private const int k_MaxAmmo = 3;
        private const float k_Velocity = 145;

        public SoulsComponent SoulsComponent { get; set; }

        //  public InputActivator m_InputActivator { get; set; }
        // TODO: consider encapsulating the folowing in a class
        IInputManager m_InputManager;
        public List<Keys> ActivationKeysList { private get; set; }
        public bool ActivateByMouse { private get; set; }

        private Gun Gun;

        public PlayerSpaceship(Game i_Game) : base(k_AssetName, i_Game)
        {
            SoulsComponent = new SoulsComponent(k_AssetName, Game);
            Gun = new Gun(k_MaxAmmo, i_Game, this.GetType());
        }

        public override void Initialize()
        {
            base.Initialize();
            initInputManager();

            Velocity = new Vector2(k_Velocity, 0);

            int bulletsDirectionMultiplier = -1;
            Gun.Initialize(Color.Red, bulletsDirectionMultiplier);

            setInitialPosition();
            initAnimations();
            
        }

        private void initAnimations()
        {
            TimeSpan animationLength = TimeSpan.FromSeconds(2.5);
            TimeSpan blinkLength = TimeSpan.FromSeconds((double)1 / 15);

            BlinkAnimator blinker = new BlinkAnimator("Blinker", blinkLength, animationLength);

            float spinsPerSecond = MathHelper.TwoPi * 4;

            SpinAnimator spinner = new SpinAnimator("Spinner", spinsPerSecond, animationLength);
            FadeOutAnimator fader = new FadeOutAnimator("Fader", animationLength);

            blinker.Finished += new EventHandler(onHitAnimationFinish);
            spinner.Finished += new EventHandler(onHitAnimationFinish);

            this.Animations.Add(blinker);
            this.Animations.Add(spinner);
            this.Animations.Add(fader);

            this.Animations.Enabled = true;
            this.Animations["Blinker"].Pause();
            this.Animations["Spinner"].Enabled = false;
            this.Animations["Fader"].Enabled = false;
            // this.Animations.Pause();
        }

        private void onHitAnimationFinish(object sender, EventArgs e)
        {
            if (SoulsComponent.NumberOfSouls > 0)
            {
                setInitialPosition();
            }
            else
            {// TODO: implement general disposel on sprite
                this.Dispose(true);
                this.Visible = false;
            }
        }

        private void setInitialPosition()
        {
            float x = 0;
            float y = base.GraphicsDevice.Viewport.Height - (Texture.Height / 2) * 1.5f;

            Position = new Vector2(x, y);
        }

        private void initInputManager()
        {
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;

            // *** default values for activation by input ***
            ActivationKeysList = new List<Keys> { Keys.Left, Keys.Right, Keys.Enter };
            ActivateByMouse = false;
        }

        protected override void InitSourceRectangle()
        {
            base.InitSourceRectangle();
            RotationOrigin = SourceRectangleCenter;
        }

        public override void Update(GameTime gameTime)
        {
            updateByInput(gameTime);

            base.Update(gameTime);
        }

        private void updateByInput(GameTime i_GameTime)
        {
            updateByKeyboard();

            if (ActivateByMouse)
            {
                updateByMouse(i_GameTime);
            }

            Position = new Vector2(MathHelper.Clamp(Position.X, 0, GraphicsDevice.Viewport.Width - Texture.Width), Position.Y);
        }

        private void updateByKeyboard()
        {
            if (m_InputManager.KeyboardState.IsKeyDown(ActivationKeysList[(int)eActivationKey.Left]))
            {
                m_Velocity.X = k_Velocity * -1;
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(ActivationKeysList[(int)eActivationKey.Right]))
            {
                m_Velocity.X = k_Velocity;
            }
            else
            {
                m_Velocity.X = 0;
            }

            if (m_InputManager.KeyPressed(ActivationKeysList[(int)eActivationKey.Shoot]))
            {
                shoot();
            }
        }

        private void updateByMouse(GameTime i_GameTime)
        {
            Position = new Vector2(Position.X + m_InputManager.MousePositionDelta.X, Position.Y);

            if (m_InputManager.ButtonReleased(eInputButtons.Left))
            {
                shoot();
            }
        }

        private void shoot()
        {
            Gun.Shoot(new Vector2(Position.X + Width / 2, Position.Y));
        }

        public override bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;

            if (i_Source is Bullet && (i_Source as Bullet).OwnerType != this.GetType())
            {
                collided = base.CheckCollision(i_Source);
            }

            return collided;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            // TODO: the souls need to begin in the upper right corner - fix after a soul is dead
            SoulsComponent.NumberOfSouls--;

            if (SoulsComponent.NumberOfSouls > 0)
            {
                //this.Animations.Restart();
                this.Animations["Blinker"].Restart();
            }   //
            else//
            {
                this.Animations["Spinner"].Enabled = true;
                this.Animations["Fader"].Enabled = true;
                this.Gun.Enable = false;
                //this.Animations["Spinner"].Restart();
                //this.Animations["Fader"].Restart();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}

/*

     */
//public class InputActivated
//{ // TODO: check accesibilty level required
//    public enum eActivationKey { Left, Right, Shoot };
//    // public Dictionary<eActivationKey, Func>
//    private IInputManager m_InputManager;
//    public List<Keys> ActivationKeysList { get; set; }

//    public InputActivated()
//    {

//    }
//    public void UpdateByKeyboard(Sprite i_Sprite, float i_Velocity)
//    {
//        if (InputManager.KeyboardState.IsKeyDown(ActivationKeysList[(int)eActivationKey.Left]))
//        {
//            float x = i_Velocity * -1;
//            i_Sprite.Velocity = new Vector2(x, i_Sprite.Velocity.Y);
//        }
//        else if (m_InputManager.KeyboardState.IsKeyDown(ActivationKeysList[(int)eActivationKey.Right]))
//        {
//            m_Velocity.X = k_Velocity;
//        }
//        else
//        {
//            m_Velocity.X = 0;
//        }

//        if (m_InputManager.KeyPressed(ActivationKeysList[(int)eActivationKey.Shoot]))
//        {
//            //Shoot();
//        }
//    }
//    public void UpdateByMouse()
//    {

//    }
//   }

//    public class PlayerSpaceship : GameObject, IShooter, IDestryoable
//    {
//        public int Souls { get; set; }
//        private MouseState? m_PrevMouseState;
//        private KeyboardState m_PrevKeyboardState;
//        private ShootingLogic m_ShootingLogic;
//        private Texture2D               m_HeartTexture;

//        public PlayerSpaceship(GraphicsDevice i_GraphicsDevice) : base(i_GraphicsDevice)
//        {
//        }

//        public override void Initialize(ContentManager i_Content)
//        {
//            base.Initialize(i_Content);
//            m_ShootingLogic = new ShootingLogic();
//            CurrentDirection = Utilities.eDirection.Right;
//            Velocity = Utilities.k_SpaceshipVelocity;
//            Color = Color.White;
//            Souls = Utilities.k_SpaceshipSouls;
//            CurrentPosition = getInitialPosition();
//        }

//        private Vector2 getInitialPosition()
//        {
//            Vector2 initial = new Vector2(0, base.GraphicsDevice.Viewport.Height);

//            initial.Y -= (Texture.Height / 2) * 1.5f;

//            return initial;
//        }

//        protected override void LoadContent(ContentManager i_Content)
//        {
//            base.LoadContent(i_Content);

//            Texture = i_Content.Load<Texture2D>(@"Sprites\Ship01_32x32");
//            m_HeartTexture = i_Content.Load<Texture2D>(@"Sprites\heart");
//        }

//        private void updateByKeyboard(GameTime i_GameTime)
//        {
//            KeyboardState keyboardState = Keyboard.GetState();
//            float x = CurrentPosition.X;

//            {
//                if (m_PrevKeyboardState.IsKeyDown(Keys.Right))
//                {
//          //          CurrentDirection = Utilities.eDirection.Right;
//                    x += Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
//                }

//                if (m_PrevKeyboardState.IsKeyDown(Keys.Left))
//                {
//          //          CurrentDirection = Utilities.eDirection.Left;
//                    x -= Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds;
//                }
//                // $G$ SFN-003 (-2) Spaceship should shoot uppon 'Enter'
//                if (keyboardState != m_PrevKeyboardState && m_PrevKeyboardState.IsKeyDown(Keys.Space))
//                {
//                    fire();
//                }

//                m_PrevKeyboardState = keyboardState;

//                CurrentPosition = new Vector2(x, CurrentPosition.Y);
//            }
//        }

//        private void fire()
//        {
//            if (m_ShootingLogic.BulletsList.Count < Utilities.k_Ammo) 
//            {
//                Vector2 initialPosition = new Vector2(CurrentPosition.X + Texture.Width / 2, CurrentPosition.Y);

//                m_ShootingLogic.Fire(GraphicsDevice, Content, initialPosition, TypeOfGameObject);
//            }
//        }

//        private void updateByMouse(GameTime i_GameTime)
//        {
//            MouseState currMouseState = Mouse.GetState();
//            CurrentPosition = getNewPositionByInput(currMouseState);

//            if (m_PrevMouseState != null)
//            {
//                if (m_PrevMouseState.Value.LeftButton == ButtonState.Pressed && currMouseState.LeftButton == ButtonState.Released)
//                {
//                    fire();
//                }
//            }

//            m_PrevMouseState = currMouseState;
//        }

//        public override void Update(GameTime i_GameTime)
//        {
//            updateByKeyboard(i_GameTime);
//            updateByMouse(i_GameTime);
//            base.Update(i_GameTime);
//            m_ShootingLogic.Update(i_GameTime);
//        }

//        public override void Draw(GameTime i_GameTime)
//        {
//            SpriteBatch.Begin();

//            SpriteBatch.Draw(Texture, CurrentPosition, Color);
//            for(int i = 0; i < Souls; i++)
//            {
//                SpriteBatch.Draw(m_HeartTexture, new Vector2(Utilities.k_heartStartingLocationX + m_HeartTexture.Width * 2 * i,Utilities.k_heartStartingLocationY), Color.White);
//            }
//            SpriteBatch.End();

//            m_ShootingLogic.Draw(i_GameTime);
//        }

//        private Vector2 getNewPositionByInput(MouseState i_currMouseState)
//        {
//            Vector2 newPosition = new Vector2(CurrentPosition.X + getMousePositionDeltaX(i_currMouseState), CurrentPosition.Y);

//            return new Vector2(MathHelper.Clamp(newPosition.X, 0, GraphicsDevice.Viewport.Width - Texture.Width), newPosition.Y);
//        }

//        private float getMousePositionDeltaX(MouseState i_currMouseState)
//        {
//            float x = 0;

//            if (m_PrevMouseState != null)
//            {
//                x = (i_currMouseState.X - m_PrevMouseState.Value.X);
//            }

//            return x;
//        }

//  //      public void IsHit(GameObject i_gameObject)
//   //     {
//    //        Souls--;
//    //    }

//        public List<Bullet> GetBulletsList()
//        {
//            return m_ShootingLogic.BulletsList;
//        }

//        public void GetHit()
//        {
//            Souls--;
//            CurrentPosition = getInitialPosition();
//        }

//        public bool IsDead()
//        {
//            return Souls == 0;
//        }
//    }
//}
