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
        private readonly float k_BulletVelocity = 155;
        // $G$ DSN-001 (-2) this property belongs to GameObject
        public bool IsVisible { get; private set; }
        // $G$ DSN-999 (-2) why does a bullet have souls?
        public int Souls { get; private set; }

        public Bullet(GraphicsDevice i_GraphicsDevice) : base(i_GraphicsDevice)
        {

        }

        public override void Initialize(ContentManager i_Content)
        {
            base.Initialize(i_Content);
            //      Souls = Utilities.k_BulletSouls;
            Velocity = k_BulletVelocity;
            IsVisible = true;
        }

        protected override void LoadContent(ContentManager i_Content)
        {
            base.LoadContent(i_Content);

            Texture = i_Content.Load<Texture2D>(@"Sprites\Bullet");
        }

        public void InitPosition(Vector2 i_InitialPosition)
        {
            CurrentPosition = i_InitialPosition;
        }

        public void Move(GameTime i_GameTime)
        {
            CurrentPosition = new Vector2(CurrentPosition.X, Utilities.CalculateNewCoordinate(CurrentPosition.Y, CurrentDirection, Velocity, i_GameTime.ElapsedGameTime.TotalSeconds));
            //    return i_oldCoord + directionMultiplier * (i_velocity * (float)i_elaspedSeconds);
            //   float yPosition = CurrentPosition.Y + (Velocity * (float)i_GameTime.ElapsedGameTime.TotalSeconds);
            //   CurrentPosition = new Vector2(CurrentPosition.X, yPosition);
        }

        public override void Update(GameTime i_GameTime)
        {
            Move(i_GameTime);
            base.Update(i_GameTime);
        }

        public override void Draw(GameTime i_GameTime)
        {
            if (IsVisible)
            {
                SpriteBatch.Begin();

                SpriteBatch.Draw(Texture, CurrentPosition, Color);

                SpriteBatch.End();
            }
        }

        public bool Hits(IDestryoable i_Destroyable)
        {
            return (Rectangle.Intersects(i_Destroyable.Rectangle) && pixelBasedCollisionDetect(i_Destroyable));
        
        }

        private bool pixelBasedCollisionDetect(IDestryoable i_Destroyable)
        {
           
            return true;
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
