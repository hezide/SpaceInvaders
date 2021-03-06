﻿using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class Bullet : Sprite, ICollidable2D, IExplodable
    {
        private const string k_AssetName = @"Sprites\Bullet";
        private const float k_Velocity = 155;
        private const float k_ExplosionRangeMultiplier = 0.7f;
        public Type OwnerType;

        public Bullet(Game i_Game, GameScreen i_Screen) : base(k_AssetName, i_Game, i_Screen)
        {
            Velocity = new Vector2(0, k_Velocity);
        }

        public float ExplosionRange
        {
            get
            {
                int direction = Velocity.Y > 0 ? 1 : -1;

                return (Height * k_ExplosionRangeMultiplier * direction);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            setVisibleFalseIfOutOfSight();
        }

        private void setVisibleFalseIfOutOfSight()
        {
            if (Position.Y <= GraphicsDevice.Viewport.Y)
            {
                Visible = false;
            }
        }

        public override void Draw(GameTime i_GameTime)
        {
            if (Visible)
            {
                base.Draw(i_GameTime);
            }
        }

        public override bool CheckCollision(ICollidable i_Source)
        {
            bool collided = false;

            if (OwnerType != i_Source.GetType())
            {
                collided = base.CheckCollision(i_Source);
            }

            return collided;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is Bullet && this.OwnerType == typeof(Enemy))
            {
                if (isRandomCollision())
                {
                    base.Collided(i_Collidable);
                }
            }
            else
            {
                base.Collided(i_Collidable);
            }
        }

        private bool isRandomCollision()
        {
            return (new Random()).Next(1, 10) == 1;
        }
    }
}
