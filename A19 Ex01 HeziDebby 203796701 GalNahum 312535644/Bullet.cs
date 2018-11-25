using A19_Ex01_HeziDebby_203796701_GalNahum_312535644.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A19_Ex01_HeziDebby_203796701_GalNahum_312535644
{
    public class Bullet : GameObject, IDestryoable
    {
        public bool IsVisible { get; private set; }

        public int Souls { get; private set; }

        public Bullet(GraphicsDevice i_graphicsDevice) : base(i_graphicsDevice)
        {

        }

        public override void Initialize(ContentManager i_content)
        {
            base.Initialize(i_content);
            Souls = Utilities.k_BulletSouls;
            Velocity = Utilities.k_BulletVelocity;
            IsVisible = true;
        }

        protected override void LoadContent(ContentManager i_content)
        {
            base.LoadContent(i_content);

            Texture = i_content.Load<Texture2D>(@"Sprites\Bullet");
        }

        public void InitPosition(Vector2 i_initialPosition)
        {
            CurrentPosition = i_initialPosition;
        }

        public void Move(GameTime i_gameTime)
        {
            CurrentPosition = new Vector2(CurrentPosition.X, Utilities.CalculateNewCoordinate(CurrentPosition.Y, CurrentDirection, Velocity, i_gameTime));
        }

        public override void Update(GameTime i_gameTime)
        {
            Move(i_gameTime);
            base.Update(i_gameTime);
        }

        public override void Draw(GameTime i_gameTime)
        {
            if (IsVisible)
            {
                SpriteBatch.Begin();

                SpriteBatch.Draw(Texture, CurrentPosition, Color);

                SpriteBatch.End();
            }
        }

        public bool Hits(IDestryoable i_destroyable)
        {
            return Rectangle.Intersects(i_destroyable.Rectangle);
        }

        public void Hide() // TODO: check if should be public
        {
            IsVisible = false;
        }

        public bool IsOutOfSight()
        {
            return (!IsVisible || CurrentPosition.Y <= GraphicsDevice.Viewport.Y);
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
