using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class MotherShip : Sprite, ICollidable2D
    {
        private const string k_AssetName = @"Sprites\MotherShip_32x120";
        private const float k_Velocity = 110;

        public MotherShip(Game i_Game) : base(k_AssetName, i_Game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            m_TintColor = Color.Red;
            Visible = false;
            //  SetInitialValues();

            initAnimations();
        }

        private void initAnimations()
        {
            this.Animations.Add(new ShrinkAnimator(TimeSpan.FromSeconds(2.2)));
            this.Animations.Add(new BlinkAnimator(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(2.2)));
            this.Animations.Add(new FadeOutAnimator(TimeSpan.FromSeconds(2.2)));

            this.Animations.Enabled = true;
            this.Animations.Pause();
        }

        protected override void InitSourceRectangle()
        {
            base.InitSourceRectangle();

            this.RotationOrigin = SourceRectangleCenter;
        }

        public void SetInitialValues()
        {
            Position = new Vector2(GraphicsDevice.Viewport.Bounds.Left - Texture.Width, Texture.Height);
            Velocity = new Vector2(k_Velocity, 0);
            Visible = true;
            IsCollided = false;
        }

        public override void Collided(ICollidable i_Collidable)
        {// TODO: check if isCollided does something 
            IsCollided = true;
            Velocity = Vector2.Zero;

            this.Animations.Restart();
        }

        public bool IsOutOfSight()
        {
            return (!Visible || Position.X > GraphicsDevice.Viewport.Bounds.Right);
        }

        public override void Draw(GameTime gameTime)
        { // TODO: code duplication on fade out draw 
            DrawNonPremultiplied();
        }
    }
}
//    {
//        public int Souls { get; set; }

//        public MotherSpaceship(GraphicsDevice i_GraphicsDevice) : base(i_GraphicsDevice)
//        {

//        }

//        public override void Initialize(ContentManager i_Content)
//        {
//            base.Initialize(i_Content);

//       //     CurrentDirection = Utilities.eDirection.Right;
//            Velocity = Utilities.k_MotherSpaceshipVelocity;
//            Color = Color.Red;
//            Souls = Utilities.k_MotherSpaceshipSouls;
//            CurrentPosition = SetToInitialPosition();
//        }

//        public Vector2 SetToInitialPosition()
//        {
//            return new Vector2(GraphicsDevice.Viewport.Bounds.Left - Texture.Width, Texture.Height);
//        }

//        protected override void LoadContent(ContentManager i_Content)
//        {
//            base.LoadContent(i_Content);

//            Texture = i_Content.Load<Texture2D>(@"Sprites\MotherShip_32x120");
//        }

//        private void move(GameTime i_GameTime)
//        {
//            CurrentPosition = new Vector2(CurrentPosition.X + Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds, CurrentPosition.Y);
//        }

//        public override void Update(GameTime i_GameTime)
//        {
//            move(i_GameTime);
//            base.Update(i_GameTime);
//        }

//        public override void Draw(GameTime i_GameTime)
//        {
//            SpriteBatch.Begin();

//            SpriteBatch.Draw(Texture, CurrentPosition, Color);

//            SpriteBatch.End();
//        }

//        // TODO: Ex2 code duplication
//        public void GetHit()
//        {
//            Souls--;
//        }
//        // TODO: Ex2 code duplication
//        public bool IsDead()
//        {
//            return Souls == 0;
//        }
//    }
//}
