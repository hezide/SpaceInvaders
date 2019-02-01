using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class MotherShip : Sprite, ICollidable2D
    {
        private const string k_AssetName = @"Sprites\MotherShip_32x120";
        private const float k_Velocity = 110;
        private SoundEffect m_MSSDieSoundEffect;

        public MotherShip(Game i_Game, GameScreen i_Screen) : base(k_AssetName, i_Game, i_Screen)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            m_TintColor = Color.Red;
            Visible = false;

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
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Velocity = Vector2.Zero;
            m_MSSDieSoundEffect.Play();
            this.Animations.Restart();
        }

        public bool IsOutOfSight()
        {
            return (!Visible || Position.X > GraphicsDevice.Viewport.Bounds.Right);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawNonPremultiplied();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            m_MSSDieSoundEffect = this.Game.Content.Load<SoundEffect>("Sounds/MotherShipKill");
        }
    }
}
