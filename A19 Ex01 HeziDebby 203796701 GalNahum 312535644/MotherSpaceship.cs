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
    public class MotherSpaceship : GameObject, IDestryoable
    {
  //      public Vector2 CurrentPosition { get; set; }
  //      public int Velocity { get; set; }
  //      public Utilities.eDirection CurrentDirection { get; set; }
  //      public Texture2D Texture { get; private set; }
  //      public Color Color { get; private set; }
        public int Souls { get; set; }

   //     public Rectangle Rectangle { get; private set; }

        public MotherSpaceship(GraphicsDevice i_graphicsDevice) : base(i_graphicsDevice)
        {

        }

        public override void Initialize(ContentManager i_content)
        {
            base.Initialize(i_content);

            CurrentDirection = Utilities.eDirection.Right;
            Velocity = Utilities.k_MotherSpaceshipVelocity;
            Color = Color.Red;
            Souls = Utilities.k_MotherSpaceshipSouls;
            CurrentPosition = SetToInitialPosition();
        }

        public Vector2 SetToInitialPosition()
        {
            return (new Vector2(base.GraphicsDevice.Viewport.Bounds.Left - Texture.Width, Texture.Height));
        }

        protected override void LoadContent(ContentManager i_content)
        {
            base.LoadContent(i_content);

            Texture = i_content.Load<Texture2D>(@"Sprites\MotherShip_32x120");
        }

        private void move(GameTime i_gameTime)
        {
            CurrentPosition = new Vector2(CurrentPosition.X + (float)Velocity * (float)i_gameTime.ElapsedGameTime.TotalSeconds, CurrentPosition.Y);
        }

        public override void Update(GameTime i_gameTime)
        {
            move(i_gameTime);
            base.Update(i_gameTime);
            //          updateRectangle();
        }

        //private void updateRectangle()
        //{
        //    Rectangle rectangle = new Rectangle((int)CurrentPosition.X, (int)CurrentPosition.Y, Texture.Width, Texture.Height);

        //    Rectangle = rectangle;
        //}

        public override void Draw(GameTime i_gameTime)
        {
            SpriteBatch.Begin();

            SpriteBatch.Draw(Texture, CurrentPosition, Color);

            SpriteBatch.End();
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
