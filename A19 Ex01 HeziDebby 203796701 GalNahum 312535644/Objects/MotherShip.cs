﻿using Infrastructure.ObjectModel;
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
            // TODO: *** for unshared sprite batch
            //m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            //m_UseSharedBatch = false;

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
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Velocity = Vector2.Zero;

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
    }
}
