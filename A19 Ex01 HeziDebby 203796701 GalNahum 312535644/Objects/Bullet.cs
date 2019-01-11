using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Objects
{
    public class Bullet : Sprite, ICollidable2D, IExplodable // TODO: try to avoid iExplodable
    {
        private const string k_AssetName = @"Sprites\Bullet";
        private const float k_Velocity = 155;
        public Type OwnerType; // TODO: should i put default value ?

        public float ExplosionRange
        {
            get
            {
                int direction = Velocity.Y > 0 ? 1 : -1;

                return (Height * 0.7f * (float)direction);
            }
        }

        public Bullet(Game i_Game) : base(k_AssetName, i_Game)
        {
            Velocity = new Vector2(0, k_Velocity);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            SetVisibleFalseIfOutOfSight();
        }

        private void SetVisibleFalseIfOutOfSight() // TODO: can be on sprite ? have something like this on motherShip .. check
        {
            if (Position.Y <= GraphicsDevice.Viewport.Y)
            {
                Visible = false;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                base.Draw(gameTime);
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
            if (i_Collidable is Bullet && this.OwnerType == typeof(Enemy)) // TODO: do i have to have type checking ?
            {
                if (isRandomCollision())
                {
                    doCollision(i_Collidable); //TODO: is this the same as calling the base class ? check
                }
            }
            else
            {
                doCollision(i_Collidable);
            }
        }

        private void doCollision(ICollidable i_Collidable)
        {
            this.Visible = false;

            OnCollision(i_Collidable, EventArgs.Empty);
        }

        private bool isRandomCollision()
        {
            return (new Random()).Next(1, 10) == 1;
        }
    }
}
