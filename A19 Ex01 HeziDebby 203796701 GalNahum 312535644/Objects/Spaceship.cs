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
    public class Spaceship : Sprite, ICollidable2D //, IShooter
    {
        public enum eActivationKey { Left, Right, Shoot }; // TODO: is enum good here ?

        private const int k_MaxAmmo = 3;
        private const float k_Velocity = 145;

        public SoulsComponent SoulsComponent { get; set; }
        public Gun Gun { get; private set; }

        //  public InputActivator m_InputActivator { get; set; }
        // TODO: consider encapsulating the folowing in a class
        IInputManager m_InputManager;
        public List<Keys> ActivationKeysList { private get; set; }
        public bool ActivateByMouse { private get; set; }
        // TODO: this way, i count on who ever activating the player to remember setting a score manager
        // TODO: lets try and see that is works, after it does ->
        // lets create a public method on sprite of add listenres then only scroeManager will know player
        //public ScoreManager ScoreManager { get; set; }

        public Spaceship(string i_AssetName, Game i_Game) : base(i_AssetName, i_Game)
        {
            //  r_Name = i_Name;
            SoulsComponent = new SoulsComponent(i_AssetName, Game);
            Gun = new Gun(k_MaxAmmo, i_Game, this.GetType());
            //    ScoreManager = i_ScoreManager;
        }

        public override void Initialize()
        {
            base.Initialize();
            //  m_UseSharedBatch = false;
          //  m_SpriteBatch = new SpriteBatch(GraphicsDevice);
          //  m_UseSharedBatch = false;

            initInputManager();

            Velocity = new Vector2(k_Velocity, 0);

            int bulletsDirectionMultiplier = -1;
            Gun.Initialize(Color.Red, bulletsDirectionMultiplier);

            setInitialPosition();
            initAnimations();
        }

        // TODO: *** for unshared sprite batch
        //protected override void LoadContent()
        //{
        //    m_SpriteBatch = new SpriteBatch(GraphicsDevice);
        //    m_UseSharedBatch = false;

        //    base.LoadContent();
        //}

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

        private void onHitAnimationFinish(object i_Sender, EventArgs i_EventArgs)
        {
            if (SoulsComponent.NumberOfSouls > 0)
            {
                setInitialPosition();
            }
            else
            { // TODO: check how affect at game over
                this.Dispose(true);
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
        {// TODO: souls reach -1 -> see what is the right way to dispose player
            SoulsComponent.NumberOfSouls--;

            if (SoulsComponent.NumberOfSouls > 0)
            {
                this.Animations["Blinker"].Restart();
            }   
            else
            {// TODO: this is compositeAnimator.. encapsulate
                this.Animations["Spinner"].Enabled = true;
                this.Animations["Fader"].Enabled = true;
                this.Gun.Enable = false;
            }

            OnCollision(this, EventArgs.Empty);
        }
        // TODO: here and on gun(with foreach) - is this the right place ?
        public void AddCollisionListener(EventHandler i_CollisionHandler)
        {
            this.Collision += new EventHandler<EventArgs>(i_CollisionHandler);
            this.Gun.AddCollisionListener(i_CollisionHandler);
        }

        public override void Draw(GameTime gameTime)
        {
            base.DrawNonPremultiplied();
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